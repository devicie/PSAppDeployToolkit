using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a POGO (Profile Guided Optimization) debug directory entry.
    /// </summary>
    /// <remarks>
    /// Use <see cref="DebugData"/> to access information about sections optimized by PGO.
    /// </remarks>
    public sealed record ImageDebugDirectoryPogo : ImageDebugDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageDebugDirectoryPogo class.
        /// </summary>
        internal ImageDebugDirectoryPogo(in IMAGE_DEBUG_DIRECTORY directory, ImageDebugPogoData debugData) : base(in directory)
        {
            DebugData = debugData;
        }

        /// <summary>
        /// Gets the parsed POGO debug information.
        /// </summary>
        /// <remarks>
        /// Contains the POGO signature type and list of optimized section entries.
        /// </remarks>
        public ImageDebugPogoData DebugData { get; }
    }
}
