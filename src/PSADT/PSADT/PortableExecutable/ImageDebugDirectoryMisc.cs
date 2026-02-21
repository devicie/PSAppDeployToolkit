using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a MISC debug directory entry containing external debug file information.
    /// </summary>
    /// <remarks>
    /// Use <see cref="DebugData"/> to access the path to an external DBG file.
    /// This is a legacy format predating PDB files.
    /// </remarks>
    public sealed record ImageDebugDirectoryMisc : ImageDebugDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageDebugDirectoryMisc class.
        /// </summary>
        internal ImageDebugDirectoryMisc(in IMAGE_DEBUG_DIRECTORY directory, ImageDebugMisc debugData) : base(in directory)
        {
            DebugData = debugData;
        }

        /// <summary>
        /// Gets the parsed MISC debug information.
        /// </summary>
        public ImageDebugMisc DebugData { get; }
    }
}
