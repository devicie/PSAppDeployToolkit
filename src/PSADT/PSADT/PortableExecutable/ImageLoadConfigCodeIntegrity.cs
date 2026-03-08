using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents the code integrity configuration from a PE file's load configuration directory.
    /// </summary>
    public sealed record ImageLoadConfigCodeIntegrity
    {
        /// <summary>
        /// Initializes a new instance of the ImageLoadConfigCodeIntegrity class using the specified
        /// IMAGE_LOAD_CONFIG_CODE_INTEGRITY structure.
        /// </summary>
        /// <param name="codeIntegrity">The IMAGE_LOAD_CONFIG_CODE_INTEGRITY structure containing the code integrity data.</param>
        internal ImageLoadConfigCodeIntegrity(in IMAGE_LOAD_CONFIG_CODE_INTEGRITY codeIntegrity)
        {
            Flags = codeIntegrity.Flags;
            Catalog = codeIntegrity.Catalog;
            CatalogOffset = codeIntegrity.CatalogOffset;
        }

        /// <summary>
        /// Gets the flags for the code integrity check.
        /// </summary>
        public ushort Flags { get; }

        /// <summary>
        /// Gets the catalog identifier.
        /// </summary>
        public ushort Catalog { get; }

        /// <summary>
        /// Gets the offset to the catalog.
        /// </summary>
        public uint CatalogOffset { get; }
    }
}
