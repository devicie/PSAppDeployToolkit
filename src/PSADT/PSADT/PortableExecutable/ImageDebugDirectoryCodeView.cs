using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a CodeView debug directory entry containing PDB information.
    /// </summary>
    /// <remarks>
    /// Use <see cref="DebugData"/> to access the parsed CodeView information, which may be
    /// <see cref="CvInfoPdb70"/> (modern RSDS format) or <see cref="CvInfoPdb20"/> (legacy NB10 format).
    /// </remarks>
    public sealed record ImageDebugDirectoryCodeView : ImageDebugDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageDebugDirectoryCodeView class.
        /// </summary>
        internal ImageDebugDirectoryCodeView(in IMAGE_DEBUG_DIRECTORY directory, CvInfoBase debugData) : base(in directory)
        {
            DebugData = debugData;
        }

        /// <summary>
        /// Gets the parsed CodeView debug information.
        /// </summary>
        /// <remarks>
        /// The concrete type indicates the CodeView format:
        /// <list type="bullet">
        /// <item><see cref="CvInfoPdb70"/> - Modern PDB 7.0 format with GUID</item>
        /// <item><see cref="CvInfoPdb20"/> - Legacy PDB 2.0 format with timestamp</item>
        /// <item><see cref="CvInfoCv41"/> - CodeView 4.10 embedded debug info</item>
        /// <item><see cref="CvInfoCv50"/> - CodeView 5.0 embedded debug info</item>
        /// </list>
        /// </remarks>
        public CvInfoBase DebugData { get; }
    }
}
