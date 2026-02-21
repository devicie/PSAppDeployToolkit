using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a COFF debug directory entry containing legacy COFF symbol information.
    /// </summary>
    /// <remarks>
    /// Use <see cref="DebugData"/> to access the COFF symbols header information.
    /// This is a legacy format primarily used by older tools.
    /// </remarks>
    public sealed record ImageDebugDirectoryCoff : ImageDebugDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageDebugDirectoryCoff class.
        /// </summary>
        internal ImageDebugDirectoryCoff(in IMAGE_DEBUG_DIRECTORY directory, ImageCoffSymbolsHeader debugData) : base(in directory)
        {
            DebugData = debugData;
        }

        /// <summary>
        /// Gets the parsed COFF debug information.
        /// </summary>
        public ImageCoffSymbolsHeader DebugData { get; }
    }
}
