using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a single relocation entry within a relocation block.
    /// </summary>
    /// <remarks>
    /// Each entry is a 16-bit value where the high 4 bits specify the relocation type
    /// and the low 12 bits specify the offset within the page.
    /// </remarks>
    public readonly record struct ImageBaseRelocationEntry
    {
        /// <summary>
        /// Initializes a new instance of the ImageBaseRelocationEntry class using the specified relocation entry and
        /// block virtual address.
        /// </summary>
        /// <param name="rawEntry">The raw relocation entry value, used to determine the type and offset of the relocation within the block.</param>
        /// <param name="blockVirtualAddress">The virtual address of the relocation block, specified as a 32-bit unsigned integer.</param>
        internal ImageBaseRelocationEntry(ushort rawEntry, uint blockVirtualAddress)
        {
            RawEntry = rawEntry;
            Rva = blockVirtualAddress;
        }

        /// <summary>
        /// Gets the relocation type (high 4 bits of the entry).
        /// </summary>
        public IMAGE_REL_BASED Type => (IMAGE_REL_BASED)(RawEntry >> 12);

        /// <summary>
        /// Gets the offset within the page where the relocation applies (low 12 bits of the entry).
        /// </summary>
        public ushort Offset => (ushort)(RawEntry & 0x0FFF);

        /// <summary>
        /// Gets the absolute RVA of the relocation (BlockVirtualAddress + Offset).
        /// </summary>
        public uint Rva => field + Offset;

        /// <summary>
        /// Represents the raw entry value as an unsigned 16-bit integer.
        /// </summary>
        private readonly ushort RawEntry;
    }
}
