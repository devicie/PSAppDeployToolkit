using System;
using System.Collections.ObjectModel;
using System.IO;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents the debug directory for a portable executable image, providing access to metadata and attributes
    /// related to debugging information.
    /// </summary>
    /// <remarks>
    /// This record encapsulates various properties describing the debug directory, including
    /// versioning, timestamps, and pointers to debugging data. For specific debug types, use the derived classes:
    /// <list type="bullet">
    /// <item><see cref="ImageDebugDirectoryCoff"/> for legacy COFF symbol information</item>
    /// <item><see cref="ImageDebugDirectoryCodeView"/> for CodeView/PDB information</item>
    /// <item><see cref="ImageDebugDirectoryFpo"/> for Frame Pointer Omission data (x86)</item>
    /// <item><see cref="ImageDebugDirectoryMisc"/> for external DBG file paths</item>
    /// <item><see cref="ImageDebugDirectoryOmap"/> for optimized-to-source address mapping</item>
    /// <item><see cref="ImageDebugDirectoryPogo"/> for Profile Guided Optimization data</item>
    /// <item><see cref="ImageDebugDirectoryRepro"/> for Reproducible Build hash data</item>
    /// <item><see cref="ImageDebugDirectoryVcFeature"/> for Visual C++ compiler feature counts</item>
    /// </list>
    /// </remarks>
    public record ImageDebugDirectory
    {
        /// <summary>
        /// Parses the debug data for this directory entry.
        /// </summary>
        /// <param name="reader">The binary reader.</param>
        /// <param name="directory">The debug directory structure.</param>
        /// <param name="basePosition">The base position of the PE file in the stream.</param>
        /// <returns>An ImageDebugDirectory (or derived type) with parsed data where applicable.</returns>
        internal static ImageDebugDirectory Parse(BinaryReader reader, in Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_DIRECTORY directory, long basePosition)
        {
            // Only parse if we have data to read.
            if (directory.PointerToRawData == 0 || directory.SizeOfData == 0)
            {
                return new(in directory);
            }

            // Save current position and seek to the debug data
            long savedPosition = reader.BaseStream.Position;
            reader.BaseStream.Position = basePosition + directory.PointerToRawData;
            try
            {
                switch ((IMAGE_DEBUG_TYPE)directory.Type)
                {
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_UNKNOWN:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_COFF:
                        if (ImageCoffSymbolsHeader.Parse(reader, directory.SizeOfData) is ImageCoffSymbolsHeader coff)
                        {
                            return new ImageDebugDirectoryCoff(in directory, coff);
                        }
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_CODEVIEW:
                        if (CvInfoBase.Parse(reader, directory.SizeOfData) is CvInfoBase codeView)
                        {
                            return new ImageDebugDirectoryCodeView(in directory, codeView);
                        }
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_FPO:
                        if (FpoData.Parse(reader, directory.SizeOfData) is ReadOnlyCollection<FpoData> fpo)
                        {
                            return new ImageDebugDirectoryFpo(in directory, fpo);
                        }
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_MISC:
                        if (ImageDebugMisc.Parse(reader, directory.SizeOfData) is ImageDebugMisc misc)
                        {
                            return new ImageDebugDirectoryMisc(in directory, misc);
                        }
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_EXCEPTION:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_FIXUP:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_OMAP_TO_SRC:
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_OMAP_FROM_SRC:
                        if (Omap.Parse(reader, directory.SizeOfData) is ReadOnlyCollection<Omap> omap)
                        {
                            return new ImageDebugDirectoryOmap(in directory, omap);
                        }
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_BORLAND:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_BBT:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_CLSID:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_VC_FEATURE:
                        if (ImageDebugVcFeatureEntry.Parse(reader, directory.SizeOfData) is ImageDebugVcFeatureEntry vcFeature)
                        {
                            return new ImageDebugDirectoryVcFeature(in directory, vcFeature);
                        }
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_POGO:
                        if (ImageDebugPogoData.Parse(reader, directory.SizeOfData) is ImageDebugPogoData pogo)
                        {
                            return new ImageDebugDirectoryPogo(in directory, pogo);
                        }
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_ILTCG:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_MPX:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_REPRO:
                        if (ImageDebugReproEntry.Parse(reader, directory.SizeOfData) is ImageDebugReproEntry repro)
                        {
                            return new ImageDebugDirectoryRepro(in directory, repro);
                        }
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_EMBEDDED_PORTABLE_PDB:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_SPGO:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_PDBCHECKSUM:
                        break;
                    case IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_EX_DLLCHARACTERISTICS:
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                // Restore position.
                reader.BaseStream.Position = savedPosition;
            }

            // Return base type for unparsed or failed parsing.
            return new(in directory);
        }

        /// <summary>
        /// Initializes a new instance of the ImageDebugDirectory class using the specified debug directory information.
        /// </summary>
        /// <param name="directory">The IMAGE_DEBUG_DIRECTORY structure that provides the debug directory information.</param>
        private protected ImageDebugDirectory(in Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_DIRECTORY directory)
        {
            Directory = directory;
        }

        /// <summary>
        /// The time and date the debugging information was created.
        /// </summary>
        public DateTime? TimeDateStamp => Directory.TimeDateStamp > 0
            ? DateTimeOffset.FromUnixTimeSeconds(Directory.TimeDateStamp).UtcDateTime
            : null;

        /// <summary>
        /// The major version number of the debugging information format.
        /// </summary>
        public Version Version => new(Directory.MajorVersion, Directory.MinorVersion);

        /// <summary>
        /// Gets the type of debug information associated with the image.
        /// </summary>
        public IMAGE_DEBUG_TYPE Type => (IMAGE_DEBUG_TYPE)Directory.Type;

        /// <summary>
        /// The size of the debugging information, in bytes. This value does not include the debug directory itself.
        /// </summary>
        public uint SizeOfData => Directory.SizeOfData;

        /// <summary>
        /// The address of the debugging information when the image is loaded, relative to the image base.
        /// </summary>
        public uint AddressOfRawData => Directory.AddressOfRawData;

        /// <summary>
        /// A file pointer to the debugging information.
        /// </summary>
        public uint PointerToRawData => Directory.PointerToRawData;

        /// <summary>
        /// The underlying IMAGE_DEBUG_DIRECTORY structure.
        /// </summary>
        private protected readonly Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_DIRECTORY Directory;
    }
}
