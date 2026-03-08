namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents an imported function identified by name.
    /// </summary>
    /// <remarks>
    /// This class extends <see cref="ImageThunkData"/> with the resolved name and hint
    /// from an IMAGE_IMPORT_BY_NAME structure. For ordinal imports, use the base
    /// <see cref="ImageThunkData"/> class directly.
    /// </remarks>
    public sealed record ImageImportByName : ImageThunkData
    {
        /// <summary>
        /// Initializes a new instance of the ImageImportByName class.
        /// </summary>
        /// <param name="rawValue">The raw thunk value.</param>
        /// <param name="is64Bit">Whether this is from a 64-bit PE file.</param>
        /// <param name="hint">The hint value from IMAGE_IMPORT_BY_NAME.</param>
        /// <param name="name">The function name from IMAGE_IMPORT_BY_NAME.</param>
        internal ImageImportByName(ulong rawValue, bool is64Bit, ushort hint, string name)
            : base(rawValue, is64Bit)
        {
            Hint = hint;
            Name = name;
        }

        /// <summary>
        /// Gets the hint value for the imported function.
        /// </summary>
        /// <remarks>
        /// The hint is an index into the export name pointer table that can be used to
        /// speed up name lookup.
        /// </remarks>
        public ushort Hint { get; }

        /// <summary>
        /// Gets the name of the imported function.
        /// </summary>
        public string Name { get; }
    }
}
