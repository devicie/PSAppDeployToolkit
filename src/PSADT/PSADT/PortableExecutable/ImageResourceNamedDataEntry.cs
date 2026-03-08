using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a resource data entry (leaf node) identified by name.
    /// </summary>
    public sealed record ImageResourceNamedDataEntry : ImageResourceNamedNode
    {
        /// <summary>
        /// Initializes a new instance of the ImageResourceNamedDataEntry class.
        /// </summary>
        /// <param name="dataEntry">The IMAGE_RESOURCE_DATA_ENTRY structure.</param>
        /// <param name="name">The name that identifies this resource.</param>
        internal ImageResourceNamedDataEntry(in IMAGE_RESOURCE_DATA_ENTRY dataEntry, string name) : base(isDirectory: false, name)
        {
            DataEntry = dataEntry;
        }

        /// <summary>
        /// Gets the RVA of the resource data.
        /// </summary>
        public uint OffsetToData => DataEntry.OffsetToData;

        /// <summary>
        /// Gets the size of the resource data in bytes.
        /// </summary>
        public uint Size => DataEntry.Size;

        /// <summary>
        /// Gets the code page used to decode the resource data.
        /// </summary>
        public uint CodePage => DataEntry.CodePage;

        /// <summary>
        /// The underlying IMAGE_RESOURCE_DATA_ENTRY structure.
        /// </summary>
        private readonly IMAGE_RESOURCE_DATA_ENTRY DataEntry;
    }
}
