using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a data directory entry in a portable executable (PE) image, providing information about the location
    /// and size of a specific data structure within the image.
    /// </summary>
    /// <remarks>The ImageDataDirectory class encapsulates details about a single entry in the PE image's data
    /// directory, such as the export table, import table, or resource table. Each entry specifies the relative virtual
    /// address and size of a particular data structure, which is essential for parsing and interpreting the contents of
    /// a PE file.</remarks>
    public sealed record ImageDataDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageDataDirectory class using the specified directory information.
        /// </summary>
        /// <param name="directory">The IMAGE_DATA_DIRECTORY structure that provides the directory information to initialize this instance.</param>
        internal ImageDataDirectory(in IMAGE_DATA_DIRECTORY directory)
        {
            Directory = directory;
        }

        /// <summary>
        /// The relative virtual address of the table.
        /// </summary>
        public uint VirtualAddress => Directory.VirtualAddress;

        /// <summary>
        /// The size of the table, in bytes.
        /// </summary>
        public uint Size => Directory.Size;

        /// <summary>
        /// Represents the image data directory that contains information about the layout and structure of the image in
        /// memory.
        /// </summary>
        /// <remarks>The image data directory provides access to various data structures within the image,
        /// such as export tables and import tables. It is essential for interpreting the organization of the image and
        /// locating specific resources or sections.</remarks>
        private readonly IMAGE_DATA_DIRECTORY Directory;
    }
}
