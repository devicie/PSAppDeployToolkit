using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using PSADT.Interop.Extensions;
using Windows.Win32;
using Windows.Win32.System.WindowsProgramming;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Provides shared utility methods for PE file parsing.
    /// </summary>
    internal static class PortableExecutableUtilities
    {
        /// <summary>
        /// Reads a structure from the binary reader.
        /// </summary>
        /// <typeparam name="T">The unmanaged structure type to read.</typeparam>
        /// <param name="reader">The binary reader to read from.</param>
        /// <returns>A reference to the read structure.</returns>
        internal static ref readonly T ReadStruct<T>(BinaryReader reader) where T : unmanaged
        {
            return ref reader.ReadBytes(Marshal.SizeOf<T>()).AsSpan().AsReadOnlyStructure<T>();
        }

        /// <summary>
        /// Reads a fixed-size array of structures.
        /// </summary>
        /// <typeparam name="T">The unmanaged structure type to read.</typeparam>
        /// <param name="reader">The binary reader to read from.</param>
        /// <param name="count">The number of structures to read.</param>
        /// <returns>An array of the read structures.</returns>
        internal static ReadOnlyCollection<T> ReadStructArray<T>(BinaryReader reader, int count) where T : unmanaged
        {
            T[] array = new T[count];
            for (int i = 0; i < count; i++)
            {
                array[i] = ReadStruct<T>(reader);
            }
            return new(array);
        }

        /// <summary>
        /// Attempts to seek to a data directory's location in the file.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="dataDir">The data directory to seek to.</param>
        /// <param name="sections">The section headers.</param>
        /// <param name="basePosition">The base position of the PE file in the stream.</param>
        /// <returns>True if the directory exists and seek was successful; false otherwise.</returns>
        internal static bool TrySeekToRvaDirectory(BinaryReader reader, ImageDataDirectory dataDir, IReadOnlyList<ImageSectionHeader> sections, long basePosition)
        {
            if (dataDir.VirtualAddress == 0 || dataDir.Size == 0)
            {
                return false;
            }
            long offset = RvaToFileOffset(dataDir.VirtualAddress, sections);
            if (offset < 0)
            {
                return false;
            }
            reader.BaseStream.Position = basePosition + offset;
            return true;
        }

        /// <summary>
        /// Converts an RVA (Relative Virtual Address) to a file offset using the section headers.
        /// </summary>
        /// <param name="rva">The RVA to convert.</param>
        /// <param name="sections">The section headers.</param>
        /// <returns>The file offset, or -1 if the RVA cannot be mapped.</returns>
        internal static long RvaToFileOffset(uint rva, IReadOnlyList<ImageSectionHeader> sections)
        {
            foreach (ImageSectionHeader section in sections)
            {
                if (rva >= section.VirtualAddress && rva < section.VirtualAddress + section.SizeOfRawData)
                {
                    return section.PointerToRawData + (rva - section.VirtualAddress);
                }
            }
            return -1;
        }

        /// <summary>
        /// Reads a null-terminated array of structures.
        /// </summary>
        /// <typeparam name="T">The unmanaged structure type to read.</typeparam>
        /// <param name="reader">The binary reader to read from.</param>
        /// <param name="isTerminator">A function that determines if an element is the null terminator.</param>
        /// <returns>A list of the read structures (excluding the terminator).</returns>
        internal static ReadOnlyCollection<T> ReadNullTerminatedArray<T>(BinaryReader reader, Func<T, bool> isTerminator) where T : unmanaged
        {
            List<T> list = [];
            while (true)
            {
                T item = ReadStruct<T>(reader);
                if (isTerminator(item))
                {
                    break;
                }
                list.Add(item);
            }
            return new(list);
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the current position.
        /// </summary>
        /// <param name="reader">The binary reader to read from.</param>
        /// <returns>The read string.</returns>
        internal static string ReadNullTerminatedAsciiString(BinaryReader reader)
        {
            List<byte> bytes = []; byte b;
            while ((b = reader.ReadByte()) != 0)
            {
                bytes.Add(b);
            }
            return Encoding.ASCII.GetString([.. bytes]);
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the specified RVA.
        /// </summary>
        /// <param name="reader">The binary reader to read from.</param>
        /// <param name="rva">The RVA of the string.</param>
        /// <param name="sections">The section headers.</param>
        /// <param name="basePosition">The base position of the PE file in the stream.</param>
        /// <returns>The read string, or empty if the RVA cannot be mapped.</returns>
        internal static string ReadNullTerminatedStringAtRva(BinaryReader reader, uint rva, IReadOnlyList<ImageSectionHeader> sections, long basePosition)
        {
            long offset = RvaToFileOffset(rva, sections);
            if (offset < 0)
            {
                return string.Empty;
            }

            long savedPosition = reader.BaseStream.Position;
            reader.BaseStream.Position = basePosition + offset;
            string result = ReadNullTerminatedAsciiString(reader);
            reader.BaseStream.Position = savedPosition;
            return result;
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the specified absolute position.
        /// </summary>
        /// <param name="reader">The binary reader to read from.</param>
        /// <param name="position">The absolute position of the string in the stream.</param>
        /// <returns>The read string.</returns>
        internal static string ReadNullTerminatedStringAtPosition(BinaryReader reader, long position)
        {
            long savedPosition = reader.BaseStream.Position;
            reader.BaseStream.Position = position;
            string result = ReadNullTerminatedAsciiString(reader);
            reader.BaseStream.Position = savedPosition;
            return result;
        }

        /// <summary>
        /// Parses a null-terminated array of IMAGE_THUNK_DATA structures into resolved import entries.
        /// </summary>
        /// <remarks>
        /// This method consolidates thunk parsing for Import Lookup Tables (ILT), Import Name Tables (INT),
        /// and similar structures that use IMAGE_THUNK_DATA to reference imports by name or ordinal.
        /// Returns <see cref="ImageImportByName"/> for named imports and <see cref="ImageThunkData"/> for ordinal imports.
        /// </remarks>
        /// <param name="reader">The binary reader positioned at the start of the thunk array.</param>
        /// <param name="sections">The section headers for RVA translation.</param>
        /// <param name="basePosition">The base position of the PE file in the stream.</param>
        /// <param name="is64Bit">Whether this is a 64-bit PE file.</param>
        /// <returns>A list of parsed import entries, or an empty list if none were found.</returns>
        internal static List<ImageThunkData> ParseImportThunkArray(BinaryReader reader, IReadOnlyList<ImageSectionHeader> sections, long basePosition, bool is64Bit)
        {
            List<ImageThunkData> entries = [];
            if (is64Bit)
            {
                ParseImportThunks64(reader, entries, sections, basePosition);
            }
            else
            {
                ParseImportThunks32(reader, entries, sections, basePosition);
            }
            return entries;
        }

        /// <summary>
        /// Parses 32-bit thunk entries into import entries.
        /// </summary>
        private static void ParseImportThunks32(BinaryReader reader, List<ImageThunkData> entries, IReadOnlyList<ImageSectionHeader> sections, long basePosition)
        {
            while (true)
            {
                ref readonly IMAGE_THUNK_DATA32 thunk = ref ReadStruct<IMAGE_THUNK_DATA32>(reader);
                if (thunk.u1.AddressOfData == 0)
                {
                    break;
                }
                if ((thunk.u1.Ordinal & PInvoke.IMAGE_ORDINAL_FLAG32) != 0)
                {
                    // Ordinal import - use base ImageThunkData
                    entries.Add(new(in thunk));
                }
                else if (ReadImageImport(reader, thunk.u1.Ordinal, false, sections, basePosition) is ImageImportByName entry)
                {
                    // Named import - use derived ImageImport
                    entries.Add(entry);
                }
            }
        }

        /// <summary>
        /// Parses 64-bit thunk entries into import entries.
        /// </summary>
        private static void ParseImportThunks64(BinaryReader reader, List<ImageThunkData> entries, IReadOnlyList<ImageSectionHeader> sections, long basePosition)
        {
            while (true)
            {
                ref readonly IMAGE_THUNK_DATA64 thunk = ref ReadStruct<IMAGE_THUNK_DATA64>(reader);
                if (thunk.u1.AddressOfData == 0)
                {
                    break;
                }
                if ((thunk.u1.Ordinal & PInvoke.IMAGE_ORDINAL_FLAG64) != 0)
                {
                    // Ordinal import - use base ImageThunkData
                    entries.Add(new(in thunk));
                }
                else if (ReadImageImport(reader, thunk.u1.Ordinal, true, sections, basePosition) is ImageImportByName entry)
                {
                    // Named import - use derived ImageImport
                    entries.Add(entry);
                }
            }
        }

        /// <summary>
        /// Reads an IMAGE_IMPORT_BY_NAME structure at the specified RVA and creates an ImageImport.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="rawValue">The raw thunk value.</param>
        /// <param name="is64Bit">Whether this is from a 64-bit PE file.</param>
        /// <param name="sections">The section headers for RVA translation.</param>
        /// <param name="basePosition">The base position of the PE file in the stream.</param>
        /// <returns>An ImageImport instance, or null if the RVA is invalid.</returns>
        internal static ImageImportByName? ReadImageImport(BinaryReader reader, ulong rawValue, bool is64Bit, IReadOnlyList<ImageSectionHeader> sections, long basePosition)
        {
            uint rva = (uint)(rawValue & 0x7FFFFFFF);
            long offset = RvaToFileOffset(rva, sections);
            if (offset < 0)
            {
                return null;
            }

            long savedPosition = reader.BaseStream.Position;
            reader.BaseStream.Position = basePosition + offset;
            ushort hint = reader.ReadUInt16();
            string name = ReadNullTerminatedAsciiString(reader);
            reader.BaseStream.Position = savedPosition;
            return new(rawValue, is64Bit, hint, name);
        }

        /// <summary>
        /// Parses a null-terminated array of raw IMAGE_THUNK_DATA structures.
        /// </summary>
        /// <remarks>
        /// Use this method when you need the raw thunk data (e.g., for IAT entries that contain
        /// bound addresses). For import name resolution, use <see cref="ParseImportThunkArray"/> instead.
        /// </remarks>
        /// <param name="reader">The binary reader positioned at the start of the thunk array.</param>
        /// <param name="is64Bit">Whether this is a 64-bit PE file.</param>
        /// <returns>A list of raw thunk data entries.</returns>
        internal static List<ImageThunkData> ParseRawThunkArray(BinaryReader reader, bool is64Bit)
        {
            List<ImageThunkData> entries = [];
            if (is64Bit)
            {
                while (true)
                {
                    ref readonly IMAGE_THUNK_DATA64 thunk = ref ReadStruct<IMAGE_THUNK_DATA64>(reader);
                    if (thunk.u1.AddressOfData == 0)
                    {
                        break;
                    }
                    entries.Add(new(in thunk));
                }
            }
            else
            {
                while (true)
                {
                    ref readonly IMAGE_THUNK_DATA32 thunk = ref ReadStruct<IMAGE_THUNK_DATA32>(reader);
                    if (thunk.u1.AddressOfData == 0)
                    {
                        break;
                    }
                    entries.Add(new(in thunk));
                }
            }
            return entries;
        }
    }
}
