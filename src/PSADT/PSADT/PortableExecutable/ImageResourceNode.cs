namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Abstract base class for nodes in the PE resource tree.
    /// </summary>
    public abstract record ImageResourceNode
    {
        /// <summary>
        /// Initializes a new instance of the ImageResourceNode class.
        /// </summary>
        /// <param name="isDirectory">Whether this node is a directory (true) or a data entry (false).</param>
        private protected ImageResourceNode(bool isDirectory)
        {
            IsDirectory = isDirectory;
        }

        /// <summary>
        /// Gets whether this node is a directory (true) or a data entry (false).
        /// </summary>
        public bool IsDirectory { get; }
    }
}
