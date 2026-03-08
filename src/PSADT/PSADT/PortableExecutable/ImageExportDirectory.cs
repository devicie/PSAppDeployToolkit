using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Provides parsed export information from a PE file's export directory.
    /// </summary>
    public sealed record ImageExportDirectory
    {
        /// <summary>
        /// Parses export information from the given reader and export directory.
        /// </summary>
        /// <param name="reader">The binary reader positioned in the PE file.</param>
        /// <param name="exportDir">The IMAGE_EXPORT_DIRECTORY structure.</param>
        /// <param name="exportDataDir">The data directory entry for exports (used for forwarder detection).</param>
        /// <param name="sections">The section headers for RVA translation.</param>
        /// <param name="basePosition">The base position of the PE file in the stream.</param>
        internal static ImageExportDirectory Parse(BinaryReader reader, in IMAGE_EXPORT_DIRECTORY exportDir, ImageDataDirectory exportDataDir, IReadOnlyList<ImageSectionHeader> sections, long basePosition)
        {
            // Read the DLL name.
            string dllName = ReadNullTerminatedString(reader, exportDir.Name, sections, basePosition);

            // Read the export address table (array of function RVAs).
            uint[] functionRvas = ReadRvaArray(reader, exportDir.AddressOfFunctions, (int)exportDir.NumberOfFunctions, sections, basePosition);

            // Read the name pointer table (array of RVAs to null-terminated strings).
            uint[] nameRvas = ReadRvaArray(reader, exportDir.AddressOfNames, (int)exportDir.NumberOfNames, sections, basePosition);

            // Read the ordinal table (array of indices into the function table).
            ushort[] nameOrdinals = ReadOrdinalArray(reader, exportDir.AddressOfNameOrdinals, (int)exportDir.NumberOfNames, sections, basePosition);

            // Build the Export Address Table entries (functions by ordinal).
            List<ImageExportFunction> functions = [];
            uint exportDirStart = exportDataDir.VirtualAddress;
            uint exportDirEnd = exportDataDir.VirtualAddress + exportDataDir.Size;
            for (int i = 0; i < functionRvas.Length; i++)
            {
                // Confirm the RVA is valid.
                uint functionRva = functionRvas[i];
                if (functionRva == 0)
                {
                    continue; // Unused slot.
                }

                // Calculate ordinal and check if this is a forwarder.
                uint ordinal = exportDir.Base + (uint)i;
                bool isForwarder = functionRva >= exportDirStart && functionRva < exportDirEnd;
                string? forwarderName = isForwarder ? ReadNullTerminatedString(reader, functionRva, sections, basePosition) : null;
                functions.Add(new(ordinal, functionRva, forwarderName));
            }

            // Build the Export Name Table entries (names to ordinals).
            List<ImageExportName> names = [];
            for (int i = 0; i < nameRvas.Length; i++)
            {
                string name = ReadNullTerminatedString(reader, nameRvas[i], sections, basePosition);
                ushort ordinal = nameOrdinals[i];
                names.Add(new(name, ordinal));
            }
            return new(in exportDir, dllName, new(functions), new(names));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageExportDirectory"/> class.
        /// </summary>
        /// <param name="directory">The IMAGE_EXPORT_DIRECTORY structure.</param>
        /// <param name="name">The name of the DLL.</param>
        /// <param name="functions">The exported functions from the Export Address Table.</param>
        /// <param name="names">The exported names from the Export Name Table.</param>
        private ImageExportDirectory(in IMAGE_EXPORT_DIRECTORY directory, string name, ReadOnlyCollection<ImageExportFunction> functions, ReadOnlyCollection<ImageExportName> names)
        {
            Directory = directory;
            Name = name;
            Functions = functions;
            Names = names;
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from the specified RVA.
        /// </summary>
        private static string ReadNullTerminatedString(BinaryReader reader, uint rva, IReadOnlyList<ImageSectionHeader> sections, long basePosition)
        {
            return PortableExecutableUtilities.ReadNullTerminatedStringAtRva(reader, rva, sections, basePosition);
        }

        /// <summary>
        /// Reads an array of RVAs (DWORDs) from the specified location.
        /// </summary>
        private static uint[] ReadRvaArray(BinaryReader reader, uint rva, int count, IReadOnlyList<ImageSectionHeader> sections, long basePosition)
        {
            // Confirm the RVA is valid.
            if (count <= 0 || rva == 0)
            {
                return [];
            }

            // Confirm the offset is valid.
            long offset = PortableExecutableUtilities.RvaToFileOffset(rva, sections);
            if (offset < 0)
            {
                return [];
            }

            // Read from the valid position.
            reader.BaseStream.Position = basePosition + offset;
            uint[] result = new uint[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = reader.ReadUInt32();
            }
            return result;
        }

        /// <summary>
        /// Reads an array of ordinals (WORDs) from the specified location.
        /// </summary>
        private static ushort[] ReadOrdinalArray(BinaryReader reader, uint rva, int count, IReadOnlyList<ImageSectionHeader> sections, long basePosition)
        {
            // Confirm the RVA is valid.
            if (count <= 0 || rva == 0)
            {
                return [];
            }

            // Confirm the offset is valid.
            long offset = PortableExecutableUtilities.RvaToFileOffset(rva, sections);
            if (offset < 0)
            {
                return [];
            }

            // Read from the valid position.
            reader.BaseStream.Position = basePosition + offset;
            ushort[] result = new ushort[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = reader.ReadUInt16();
            }
            return result;
        }

        /// <summary>
        /// Gets the UTC date and time represented by the Unix timestamp from the directory, or null if the timestamp is
        /// not valid.
        /// </summary>
        /// <remarks>The timestamp is considered valid if it is greater than zero. If the timestamp is
        /// valid, it is converted from Unix time to a DateTime object in UTC format.</remarks>
        public DateTime? TimeDateStamp => Directory.TimeDateStamp > 0
            ? DateTimeOffset.FromUnixTimeSeconds(Directory.TimeDateStamp).UtcDateTime
            : null;

        /// <summary>
        /// Gets the version of the directory, represented as a Version object containing the major and minor version
        /// numbers.
        /// </summary>
        public Version Version => new(Directory.MajorVersion, Directory.MinorVersion);

        /// <summary>
        /// Gets the name of the DLL as specified in the export directory.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the base ordinal number for exports.
        /// </summary>
        public uint Base => Directory.Base;

        /// <summary>
        /// Gets the collection of exported functions from the Export Address Table (EAT).
        /// </summary>
        public IReadOnlyList<ImageExportFunction> Functions { get; }

        /// <summary>
        /// Gets the collection of exported names from the Export Name Table (ENT).
        /// </summary>
        public IReadOnlyList<ImageExportName> Names { get; }

        /// <summary>
        /// Gets the raw IMAGE_EXPORT_DIRECTORY structure.
        /// </summary>
        private readonly IMAGE_EXPORT_DIRECTORY Directory;
    }
}
