namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a single POGO (Profile Guided Optimization) entry.
    /// </summary>
    /// <remarks>
    /// Each entry describes a section that was optimized or instrumented by PGO.
    /// </remarks>
    public sealed record ImageDebugPogoEntry
    {
        /// <summary>
        /// Initializes a new instance of the PogoEntry class.
        /// </summary>
        internal ImageDebugPogoEntry(uint rva, uint size, string name)
        {
            Rva = rva;
            Size = size;
            Name = name;
        }

        /// <summary>
        /// Gets the relative virtual address of the section.
        /// </summary>
        public uint Rva { get; }

        /// <summary>
        /// Gets the size of the section in bytes.
        /// </summary>
        public uint Size { get; }

        /// <summary>
        /// Gets the name of the section (e.g., ".text", ".rdata").
        /// </summary>
        public string Name { get; }
    }
}
