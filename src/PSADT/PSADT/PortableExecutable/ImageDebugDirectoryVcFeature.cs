using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a VC_FEATURE debug directory entry containing Visual C++ compiler feature counts.
    /// </summary>
    /// <remarks>
    /// Use <see cref="DebugData"/> to access counts of objects compiled with various security features.
    /// </remarks>
    public sealed record ImageDebugDirectoryVcFeature : ImageDebugDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageDebugDirectoryVcFeature class.
        /// </summary>
        internal ImageDebugDirectoryVcFeature(in IMAGE_DEBUG_DIRECTORY directory, ImageDebugVcFeatureEntry debugData) : base(in directory)
        {
            DebugData = debugData;
        }

        /// <summary>
        /// Gets the parsed VC_FEATURE debug information.
        /// </summary>
        /// <remarks>
        /// Contains counts of objects compiled with various Visual C++ security features
        /// such as /GS, /sdl, and Control Flow Guard.
        /// </remarks>
        public ImageDebugVcFeatureEntry DebugData { get; }
    }
}
