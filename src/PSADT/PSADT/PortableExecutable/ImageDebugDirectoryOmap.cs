using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents an OMAP_TO_SRC debug directory entry for mapping optimized addresses to source addresses.
    /// </summary>
    /// <remarks>
    /// Use <see cref="DebugData"/> to access OMAP entries that map optimized RVAs back to their original source RVAs.
    /// This is used when code has been rearranged during optimization.
    /// </remarks>
    public sealed record ImageDebugDirectoryOmap : ImageDebugDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageDebugDirectoryOmapToSrc class.
        /// </summary>
        internal ImageDebugDirectoryOmap(in IMAGE_DEBUG_DIRECTORY directory, ReadOnlyCollection<Omap> debugData) : base(in directory)
        {
            DebugData = debugData;
        }

        /// <summary>
        /// Gets the parsed OMAP debug information.
        /// </summary>
        public IReadOnlyList<Omap> DebugData { get; }
    }
}
