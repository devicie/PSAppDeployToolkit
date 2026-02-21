using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using PSADT.Interop.Extensions;
using Windows.Win32;
using Windows.Win32.Security.WinTrust;
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
        /// Gets the parsed data directories.
        /// </summary>
        public DataDirectories DataDirectories { get; }

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

            // Local variables for building DataDirectories.
            ImageExportDirectory? export = null;
            ReadOnlyCollection<ImageImportDescriptor>? import = null;
            ImageResourceIdDirectory? resource = null;
            ReadOnlyCollection<ImageRuntimeFunctionEntry>? exception = null;
            ReadOnlyCollection<WinCertificate>? security = null;
            ReadOnlyCollection<ImageBaseRelocation>? baseReloc = null;
            ReadOnlyCollection<ImageDebugDirectory>? debug = null;
            uint? globalPtr = null;
            ImageTlsDirectory? tls = null;
            ImageLoadConfigDirectory? loadConfig = null;
            ReadOnlyCollection<ImageBoundImportDescriptor>? boundImport = null;
            ReadOnlyCollection<ImageThunkData>? iat = null;
            ReadOnlyCollection<ImageDelayLoadDescriptor>? delayImport = null;
            ImageCor20Header? comDescriptor = null;

            // Parse the export directory if present (IMAGE_DIRECTORY_ENTRY_EXPORT, index 0).
            ImageDataDirectory exportDataDir = ImageNtHeaders.OptionalHeader.DataDirectories[0];
            if (PortableExecutableUtilities.TrySeekToRvaDirectory(reader, exportDataDir, ImageSectionHeaders, basePosition))
            {
                export = ImageExportDirectory.Parse(reader, in PortableExecutableUtilities.ReadStruct<IMAGE_EXPORT_DIRECTORY>(reader), exportDataDir, ImageSectionHeaders, basePosition);
            }

            // Parse the import directory if present (IMAGE_DIRECTORY_ENTRY_IMPORT, index 1).
            if (PortableExecutableUtilities.TrySeekToRvaDirectory(reader, ImageNtHeaders.OptionalHeader.DataDirectories[1], ImageSectionHeaders, basePosition) && PortableExecutableUtilities.ReadNullTerminatedArray<IMAGE_IMPORT_DESCRIPTOR>(reader, static d => d.Anonymous.Characteristics == 0 && d.Name == 0) is { Count: > 0 } importDescriptors)
            {
                import = ImageImportDescriptor.Parse(reader, importDescriptors, ImageSectionHeaders, basePosition, ImageNtHeaders.OptionalHeader.Is64Bit);
            }

            // Parse the resource directory tree if present (IMAGE_DIRECTORY_ENTRY_RESOURCE, index 2).
            ImageDataDirectory resourceDataDir = ImageNtHeaders.OptionalHeader.DataDirectories[2];
            if (PortableExecutableUtilities.TrySeekToRvaDirectory(reader, resourceDataDir, ImageSectionHeaders, basePosition))
            {
                resource = ImageResourceDirectory.Parse(reader, reader.BaseStream.Position);
            }

            // Parse the exception directory if present (IMAGE_DIRECTORY_ENTRY_EXCEPTION, index 3).
            // Only present on x64, ARM, and ARM64 architectures.
            ImageDataDirectory exceptionDataDir = ImageNtHeaders.OptionalHeader.DataDirectories[3];
            if (PortableExecutableUtilities.TrySeekToRvaDirectory(reader, exceptionDataDir, ImageSectionHeaders, basePosition))
            {
                int count = (int)(exceptionDataDir.Size / Marshal.SizeOf<IMAGE_RUNTIME_FUNCTION_ENTRY>());
                List<ImageRuntimeFunctionEntry> parsed = new(count);
                for (int i = 0; i < count; i++)
                {
                    parsed.Add(new(in PortableExecutableUtilities.ReadStruct<IMAGE_RUNTIME_FUNCTION_ENTRY>(reader)));
                }
                exception = new(parsed);
            }

            // Parse the security/certificate directory if present (IMAGE_DIRECTORY_ENTRY_SECURITY, index 4).
            // Note: VirtualAddress is actually a raw file offset, not an RVA.
            ImageDataDirectory securityDataDir = ImageNtHeaders.OptionalHeader.DataDirectories[4];
            if (securityDataDir.VirtualAddress != 0 && securityDataDir.Size != 0)
            {
                reader.BaseStream.Position = securityDataDir.VirtualAddress;
                long endPosition = securityDataDir.VirtualAddress + securityDataDir.Size;
                List<WinCertificate> certificates = [];
                while (reader.BaseStream.Position < endPosition)
                {
                    // Read the length first to know how much to read.
                    long entryStart = reader.BaseStream.Position;
                    uint dwLength = reader.ReadUInt32();
                    if (dwLength < 8)
                    {
                        break; // Invalid entry.
                    }

                    // Seek back and read the full entry into a buffer.
                    reader.BaseStream.Position = entryStart;
                    byte[] entryBuffer = reader.ReadBytes((int)dwLength);

                    // Get a reference to the WIN_CERTIFICATE in the buffer and extract the certificate data.
                    ref readonly WIN_CERTIFICATE raw = ref entryBuffer.AsSpan().AsReadOnlyStructure<WIN_CERTIFICATE>();
                    certificates.Add(new(in raw, raw.bCertificate.AsSpan((int)dwLength - 8).ToArray()));

                    // Certificates are 8-byte aligned.
                    long nextEntry = entryStart + ((dwLength + 7) & ~7U);
                    reader.BaseStream.Position = nextEntry;
                }
                if (certificates.Count > 0)
                {
                    security = new(certificates);
                }
            }

            // Parse the base relocation directory if present (IMAGE_DIRECTORY_ENTRY_BASERELOC, index 5).
            ImageDataDirectory relocDataDir = ImageNtHeaders.OptionalHeader.DataDirectories[5];
            if (relocDataDir.VirtualAddress != 0 && relocDataDir.Size != 0)
            {
                baseReloc = ImageBaseRelocation.Parse(reader, ImageSectionHeaders, basePosition, relocDataDir);
            }

            // Parse the debug directory if present (IMAGE_DIRECTORY_ENTRY_DEBUG, index 6).
            ImageDataDirectory debugDataDir = ImageNtHeaders.OptionalHeader.DataDirectories[6];
            if (PortableExecutableUtilities.TrySeekToRvaDirectory(reader, debugDataDir, ImageSectionHeaders, basePosition))
            {
                int count = (int)(debugDataDir.Size / Marshal.SizeOf<IMAGE_DEBUG_DIRECTORY>());
                List<ImageDebugDirectory> parsed = new(count);
                for (int i = 0; i < count; i++)
                {
                    ref readonly IMAGE_DEBUG_DIRECTORY dir = ref PortableExecutableUtilities.ReadStruct<IMAGE_DEBUG_DIRECTORY>(reader);
                    parsed.Add(ImageDebugDirectory.Parse(reader, in dir, basePosition));
                }
                debug = new(parsed);
            }

            // Parse the global pointer if present (IMAGE_DIRECTORY_ENTRY_GLOBALPTR, index 8).
            ImageDataDirectory globalPtrDataDir = ImageNtHeaders.OptionalHeader.DataDirectories[8];
            if (globalPtrDataDir.VirtualAddress != 0)
            {
                globalPtr = globalPtrDataDir.VirtualAddress;
            }

            // Parse the TLS directory if present (IMAGE_DIRECTORY_ENTRY_TLS, index 9).
            if (PortableExecutableUtilities.TrySeekToRvaDirectory(reader, ImageNtHeaders.OptionalHeader.DataDirectories[9], ImageSectionHeaders, basePosition))
            {
                if (ImageNtHeaders.OptionalHeader.Is64Bit)
                {
                    tls = new(in PortableExecutableUtilities.ReadStruct<IMAGE_TLS_DIRECTORY64>(reader));
                }
                else
                {
                    tls = new(in PortableExecutableUtilities.ReadStruct<IMAGE_TLS_DIRECTORY32>(reader));
                }
            }

            // Parse the load config directory if present (IMAGE_DIRECTORY_ENTRY_LOAD_CONFIG, index 10).
            if (PortableExecutableUtilities.TrySeekToRvaDirectory(reader, ImageNtHeaders.OptionalHeader.DataDirectories[10], ImageSectionHeaders, basePosition))
            {
                if (ImageNtHeaders.OptionalHeader.Is64Bit)
                {
                    loadConfig = new(in PortableExecutableUtilities.ReadStruct<IMAGE_LOAD_CONFIG_DIRECTORY64>(reader));
                }
                else
                {
                    loadConfig = new(in PortableExecutableUtilities.ReadStruct<IMAGE_LOAD_CONFIG_DIRECTORY32>(reader));
                }
            }

            // Parse the bound import directory if present (IMAGE_DIRECTORY_ENTRY_BOUND_IMPORT, index 11).
            // Note: VirtualAddress is a raw file offset, not an RVA.
            boundImport = ImageBoundImportDescriptor.Parse(reader, basePosition, ImageNtHeaders.OptionalHeader.DataDirectories[11]);

            // Parse the IAT if present (IMAGE_DIRECTORY_ENTRY_IAT, index 12).
            ImageDataDirectory iatDataDir = ImageNtHeaders.OptionalHeader.DataDirectories[12];
            if (iatDataDir.VirtualAddress != 0 && iatDataDir.Size != 0)
            {
                long iatOffset = PortableExecutableUtilities.RvaToFileOffset(iatDataDir.VirtualAddress, ImageSectionHeaders);
                if (iatOffset >= 0)
                {
                    reader.BaseStream.Position = basePosition + iatOffset;
                    List<ImageThunkData> iatEntries = PortableExecutableUtilities.ParseRawThunkArray(reader, ImageNtHeaders.OptionalHeader.Is64Bit);
                    if (iatEntries.Count > 0)
                    {
                        iat = new(iatEntries);
                    }
                }
            }

            // Parse the delay import directory if present (IMAGE_DIRECTORY_ENTRY_DELAY_IMPORT, index 13).
            if (PortableExecutableUtilities.TrySeekToRvaDirectory(reader, ImageNtHeaders.OptionalHeader.DataDirectories[13], ImageSectionHeaders, basePosition))
            {
                List<ImageDelayLoadDescriptor> delayImports = [];
                while (true)
                {
                    // Break if we've reached the end.
                    ref readonly Windows.Win32.System.WindowsProgramming.IMAGE_DELAYLOAD_DESCRIPTOR descriptor = ref PortableExecutableUtilities.ReadStruct<Windows.Win32.System.WindowsProgramming.IMAGE_DELAYLOAD_DESCRIPTOR>(reader);
                    if (descriptor.DllNameRVA == 0)
                    {
                        break;
                    }

                    // Read out the descriptor's name.
                    long savedPosition = reader.BaseStream.Position;
                    string dllName = PortableExecutableUtilities.ReadNullTerminatedStringAtRva(reader, descriptor.DllNameRVA, ImageSectionHeaders, basePosition);

                    // Parse the Import Address Table (thunks).
                    List<ImageThunkData>? importAddressTable = null;
                    if (descriptor.ImportAddressTableRVA != 0)
                    {
                        long iatOffset = PortableExecutableUtilities.RvaToFileOffset(descriptor.ImportAddressTableRVA, ImageSectionHeaders);
                        if (iatOffset >= 0)
                        {
                            reader.BaseStream.Position = basePosition + iatOffset;
                            List<ImageThunkData> parsed = PortableExecutableUtilities.ParseRawThunkArray(reader, ImageNtHeaders.OptionalHeader.Is64Bit);
                            if (parsed.Count > 0)
                            {
                                importAddressTable = parsed;
                            }
                        }
                    }

                    // Parse the Import Name Table (names/ordinals).
                    List<ImageThunkData>? importNameTable = null;
                    if (descriptor.ImportNameTableRVA != 0)
                    {
                        long intOffset = PortableExecutableUtilities.RvaToFileOffset(descriptor.ImportNameTableRVA, ImageSectionHeaders);
                        if (intOffset >= 0)
                        {
                            reader.BaseStream.Position = basePosition + intOffset;
                            List<ImageThunkData> parsed = PortableExecutableUtilities.ParseImportThunkArray(reader, ImageSectionHeaders, basePosition, ImageNtHeaders.OptionalHeader.Is64Bit);
                            if (parsed.Count > 0)
                            {
                                importNameTable = parsed;
                            }
                        }
                    }

                    // Parse the Bound Import Address Table (bound thunks).
                    List<ImageThunkData>? boundImportAddressTable = null;
                    if (descriptor.BoundImportAddressTableRVA != 0)
                    {
                        long boundOffset = PortableExecutableUtilities.RvaToFileOffset(descriptor.BoundImportAddressTableRVA, ImageSectionHeaders);
                        if (boundOffset >= 0)
                        {
                            reader.BaseStream.Position = basePosition + boundOffset;
                            List<ImageThunkData> parsed = PortableExecutableUtilities.ParseRawThunkArray(reader, ImageNtHeaders.OptionalHeader.Is64Bit);
                            if (parsed.Count > 0)
                            {
                                boundImportAddressTable = parsed;
                            }
                        }
                    }

                    // Parse the Unload Information Table (original thunks for unloading).
                    List<ImageThunkData>? unloadInformationTable = null;
                    if (descriptor.UnloadInformationTableRVA != 0)
                    {
                        long unloadOffset = PortableExecutableUtilities.RvaToFileOffset(descriptor.UnloadInformationTableRVA, ImageSectionHeaders);
                        if (unloadOffset >= 0)
                        {
                            reader.BaseStream.Position = basePosition + unloadOffset;
                            List<ImageThunkData> parsed = PortableExecutableUtilities.ParseRawThunkArray(reader, ImageNtHeaders.OptionalHeader.Is64Bit);
                            if (parsed.Count > 0)
                            {
                                unloadInformationTable = parsed;
                            }
                        }
                    }

                    // Build out the completed object.
                    reader.BaseStream.Position = savedPosition;
                    delayImports.Add(new(
                        in descriptor,
                        dllName,
                        importAddressTable is not null ? new(importAddressTable) : null,
                        importNameTable is not null ? new(importNameTable) : null,
                        boundImportAddressTable is not null ? new(boundImportAddressTable) : null,
                        unloadInformationTable is not null ? new(unloadInformationTable) : null));
                }
                if (delayImports.Count > 0)
                {
                    delayImport = new(delayImports);
                }
            }

            // Parse the CLR runtime header if present (IMAGE_DIRECTORY_ENTRY_COM_DESCRIPTOR, index 14).
            if (PortableExecutableUtilities.TrySeekToRvaDirectory(reader, ImageNtHeaders.OptionalHeader.DataDirectories[14], ImageSectionHeaders, basePosition))
            {
                comDescriptor = new(in PortableExecutableUtilities.ReadStruct<IMAGE_COR20_HEADER>(reader));
            }

            // Build the DataDirectories object.
            DataDirectories = new(
                export,
                import,
                resource,
                exception,
                security,
                baseReloc,
                debug,
                globalPtr,
                tls,
                loadConfig,
                boundImport,
                iat,
                delayImport,
                comDescriptor);
        }
    }
}
