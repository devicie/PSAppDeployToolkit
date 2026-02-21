using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Win32;
using Windows.Win32.System.Diagnostics.Debug;
using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Provides comprehensive information about a PE (Portable Executable) file.
    /// This class preserves full fidelity of the PE header structure in processing order.
    /// </summary>
    public sealed record PortableExecutableInfo
    {
        /// <summary>
        /// Parses the specified PE file and returns comprehensive header information.
        /// </summary>
        /// <param name="filePath">The path to the PE file.</param>
        /// <returns>A <see cref="PortableExecutableInfo"/> instance containing the parsed PE header information.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="filePath"/> is null or whitespace.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the specified file does not exist.</exception>
        /// <exception cref="InvalidDataException">Thrown when the file is not a valid PE file.</exception>
        public static PortableExecutableInfo Get(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath), "File path cannot be null or empty.");
            }
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified PE file does not exist.", filePath);
            }
            using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            return Get(fs);
        }

        /// <summary>
        /// Parses a PE file from a stream.
        /// </summary>
        /// <param name="stream">The stream containing the PE data.</param>
        /// <returns>A <see cref="PortableExecutableInfo"/> instance containing the parsed PE header information.</returns>
        internal static PortableExecutableInfo Get(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);
            if (!stream.CanRead)
            {
                throw new ArgumentException("Stream must be readable.", nameof(stream));
            }
            if (!stream.CanSeek)
            {
                throw new ArgumentException("Stream must be seekable.", nameof(stream));
            }
            using BinaryReader reader = new(stream, System.Text.Encoding.Default, leaveOpen: true);
            return new(reader);
        }

        /// <summary>
        /// Gets the IMAGE_DOS_HEADER structure (MZ header).
        /// </summary>
        public ImageDosHeader ImageDosHeader { get; }

        /// <summary>
        /// Gets the IMAGE_NT_HEADERS structure containing the PE signature, file header, and optional header.
        /// </summary>
        public ImageNtHeaders ImageNtHeaders { get; }

        /// <summary>
        /// Gets the array of IMAGE_SECTION_HEADER structures.
        /// </summary>
        public IReadOnlyList<ImageSectionHeader> ImageSectionHeaders { get; }

        /// <summary>
        /// Parses PE headers from the provided binary reader.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0045:Convert to conditional expression", Justification = "Implementing this in this method will just make for worse code.")]
        private PortableExecutableInfo(BinaryReader reader)
        {
            // Read and validate the DOS header.
            long basePosition = reader.BaseStream.Position;
            ImageDosHeader = new(in PortableExecutableUtilities.ReadStruct<IMAGE_DOS_HEADER>(reader));
            if (ImageDosHeader.e_magic != PInvoke.IMAGE_DOS_SIGNATURE)
            {
                throw new InvalidDataException("The specified file does not have a valid DOS header (missing MZ signature).");
            }

            // Seek to the NT headers and validate PE signature.
            reader.BaseStream.Position = basePosition + ImageDosHeader.e_lfanew;
            uint peSignature = reader.ReadUInt32();
            if (peSignature != PInvoke.IMAGE_NT_SIGNATURE)
            {
                throw new InvalidDataException("The specified file does not have a valid PE signature.");
            }

            // Read the file header and the optional header based on magic number.
            ref readonly IMAGE_FILE_HEADER fileHeader = ref PortableExecutableUtilities.ReadStruct<IMAGE_FILE_HEADER>(reader);
            Interop.IMAGE_OPTIONAL_HEADER_MAGIC magic = (Interop.IMAGE_OPTIONAL_HEADER_MAGIC)reader.ReadUInt16();
            reader.BaseStream.Position -= 2; // Seek back to re-read the full optional header.
            if (magic == Interop.IMAGE_OPTIONAL_HEADER_MAGIC.IMAGE_NT_OPTIONAL_HDR32_MAGIC)
            {
                ImageNtHeaders = new(peSignature, in fileHeader, in PortableExecutableUtilities.ReadStruct<IMAGE_OPTIONAL_HEADER32>(reader));
            }
            else if (magic == Interop.IMAGE_OPTIONAL_HEADER_MAGIC.IMAGE_NT_OPTIONAL_HDR64_MAGIC)
            {
                ImageNtHeaders = new(peSignature, in fileHeader, in PortableExecutableUtilities.ReadStruct<IMAGE_OPTIONAL_HEADER64>(reader));
            }
            else
            {
                throw new InvalidDataException($"The specified file has an invalid optional header magic number: 0x{(ushort)magic:X4}.");
            }

            // Read section headers.
            List<ImageSectionHeader> sectionHeaders = new(fileHeader.NumberOfSections);
            for (int i = 0; i < fileHeader.NumberOfSections; i++)
            {
                sectionHeaders.Add(new(in PortableExecutableUtilities.ReadStruct<IMAGE_SECTION_HEADER>(reader)));
            }
            ImageSectionHeaders = new ReadOnlyCollection<ImageSectionHeader>(sectionHeaders);
        }
    }
}
