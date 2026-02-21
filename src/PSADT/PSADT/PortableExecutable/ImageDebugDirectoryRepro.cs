using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a REPRO (Reproducible Build) debug directory entry.
    /// </summary>
    /// <remarks>
    /// Use <see cref="DebugData"/> to access the reproducibility hash used for verifying deterministic builds.
    /// </remarks>
    public sealed record ImageDebugDirectoryRepro : ImageDebugDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageDebugDirectoryRepro class.
        /// </summary>
        internal ImageDebugDirectoryRepro(in IMAGE_DEBUG_DIRECTORY directory, ImageDebugReproEntry debugData) : base(in directory)
        {
            DebugData = debugData;
        }

        /// <summary>
        /// Gets the parsed REPRO debug information.
        /// </summary>
        /// <remarks>
        /// Contains the reproducibility hash used to verify deterministic builds.
        /// </remarks>
        public ImageDebugReproEntry DebugData { get; }
    }
}
