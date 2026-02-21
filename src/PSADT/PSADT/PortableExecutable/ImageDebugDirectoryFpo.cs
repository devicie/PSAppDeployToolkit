using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents an FPO (Frame Pointer Omission) debug directory entry.
    /// </summary>
    /// <remarks>
    /// Use <see cref="DebugData"/> to access the array of FPO entries describing
    /// functions with omitted frame pointers. Primarily used for x86 binaries.
    /// </remarks>
    public sealed record ImageDebugDirectoryFpo : ImageDebugDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageDebugDirectoryFpo class.
        /// </summary>
        internal ImageDebugDirectoryFpo(in IMAGE_DEBUG_DIRECTORY directory, ReadOnlyCollection<FpoData> debugData) : base(in directory)
        {
            DebugData = debugData;
        }

        /// <summary>
        /// Gets the parsed FPO debug information.
        /// </summary>
        public IReadOnlyList<FpoData> DebugData { get; }
    }
}
