namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Abstract base class for resource nodes identified by numeric ID.
    /// </summary>
    public abstract record ImageResourceIdNode : ImageResourceNode
    {
        /// <summary>
        /// Initializes a new instance of the ImageResourceIdNode class.
        /// </summary>
        /// <param name="isDirectory">Whether this node is a directory (true) or a data entry (false).</param>
        /// <param name="id">The numeric ID that identifies this resource node.</param>
        private protected ImageResourceIdNode(bool isDirectory, uint id) : base(isDirectory)
        {
            Id = id;
        }

        /// <summary>
        /// Gets the numeric ID that identifies this resource node.
        /// </summary>
        public uint Id { get; }
    }
}
