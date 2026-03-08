namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents an entry in the Export Address Table (EAT).
    /// </summary>
    /// <remarks>
    /// Each entry corresponds to a function exported by ordinal. The ordinal is calculated
    /// as the index into the EAT plus the Base value from the export directory.
    /// If the RVA points within the export directory, this is a forwarded export.
    /// </remarks>
    public sealed record ImageExportFunction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageExportFunction"/> class.
        /// </summary>
        /// <param name="ordinal">The ordinal value for this export.</param>
        /// <param name="rva">The relative virtual address of the function or forwarder string.</param>
        /// <param name="forwarderName">The forwarder name if this is a forwarded export, otherwise null.</param>
        internal ImageExportFunction(uint ordinal, uint rva, string? forwarderName)
        {
            Ordinal = ordinal;
            Rva = rva;
            ForwarderName = forwarderName;
        }

        /// <summary>
        /// Gets the ordinal value for this export.
        /// </summary>
        /// <remarks>
        /// The ordinal is the index into the Export Address Table plus the Base value.
        /// </remarks>
        public uint Ordinal { get; }

        /// <summary>
        /// Gets the relative virtual address of the exported function.
        /// </summary>
        /// <remarks>
        /// If <see cref="IsForwarder"/> is true, this RVA points to the forwarder string
        /// rather than executable code.
        /// </remarks>
        public uint Rva { get; }

        /// <summary>
        /// Gets a value indicating whether this export is forwarded to another DLL.
        /// </summary>
        public bool IsForwarder => ForwarderName is not null;

        /// <summary>
        /// Gets the forwarder name if this is a forwarded export, otherwise null.
        /// </summary>
        /// <remarks>
        /// The forwarder name is in the format "DllName.ExportName" or "DllName.#Ordinal".
        /// </remarks>
        public string? ForwarderName { get; }
    }
}
