using System;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents the debug directory for a portable executable image, providing access to metadata and attributes
    /// related to debugging information.
    /// </summary>
    /// <remarks>This record encapsulates various properties describing the debug directory, including
    /// versioning, timestamps, and pointers to debugging data. It is typically used to interpret and access debugging
    /// information embedded within executable files, which is essential for diagnostic and development
    /// purposes.</remarks>
    public sealed record ImageDebugDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageDebugDirectory class using the specified debug directory information.
        /// </summary>
        /// <param name="directory">The IMAGE_DEBUG_DIRECTORY structure that provides the debug directory information.</param>
        internal ImageDebugDirectory(in Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_DIRECTORY directory)
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
        private readonly Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_DIRECTORY Directory;
    }
}
