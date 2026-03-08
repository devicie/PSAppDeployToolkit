namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents an entry in the Export Name Table (ENT).
    /// </summary>
    /// <remarks>
    /// Each entry maps an exported name to its corresponding ordinal.
    /// The ordinal can then be used to look up the function in the Export Address Table.
    /// </remarks>
    public sealed record ImageExportName
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageExportName"/> class.
        /// </summary>
        /// <param name="name">The exported name.</param>
        /// <param name="ordinal">The ordinal corresponding to this name.</param>
        internal ImageExportName(string name, ushort ordinal)
        {
            Name = name;
            Ordinal = ordinal;
        }

        /// <summary>
        /// Gets the exported function name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the ordinal corresponding to this name.
        /// </summary>
        /// <remarks>
        /// This value comes from the Export Ordinal Table (AddressOfNameOrdinals).
        /// To get the actual function, use this ordinal to index into the Export Address Table.
        /// </remarks>
        public ushort Ordinal { get; }
    }
}
