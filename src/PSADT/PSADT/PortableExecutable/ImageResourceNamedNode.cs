namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Abstract base class for resource nodes identified by name.
    /// </summary>
    public abstract record ImageResourceNamedNode : ImageResourceNode
    {
        /// <summary>
        /// Initializes a new instance of the ImageResourceNamedNode class.
        /// </summary>
        /// <param name="isDirectory">Whether this node is a directory (true) or a data entry (false).</param>
        /// <param name="name">The name that identifies this resource node.</param>
        private protected ImageResourceNamedNode(bool isDirectory, string name) : base(isDirectory)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name that identifies this resource node.
        /// </summary>
        public string Name { get; }
    }
}
