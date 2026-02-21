namespace PSADT.Interop
{
    /// <summary>
    /// Specifies the magic numbers used to identify the type of optional header present in a Portable Executable (PE)
    /// file format.
    /// </summary>
    /// <remarks>These values are essential for parsing and validating PE file headers, allowing tools and
    /// libraries to distinguish between 32-bit, 64-bit, and ROM image optional header formats. Use this enumeration
    /// when working with low-level file operations, custom loaders, or diagnostic utilities that require precise
    /// identification of PE header structures.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "The type is correct for the underlying Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1069:Enums values should not be duplicated", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "These values are precisely as they're defined in the Win32 API.")]
    public enum IMAGE_OPTIONAL_HEADER_MAGIC : ushort
    {
        /// <summary>
        /// Represents the magic number used to identify the presence of an IMAGE_NT_OPTIONAL_HDR structure in a
        /// Portable Executable (PE) file header.
        /// </summary>
        /// <remarks>This constant is typically used when parsing or validating PE file headers to ensure
        /// that the correct optional header structure is present. It is essential for low-level file format operations
        /// and debugging scenarios involving PE files.</remarks>
        IMAGE_NT_OPTIONAL_HDR_MAGIC = Windows.Win32.System.Diagnostics.Debug.IMAGE_OPTIONAL_HEADER_MAGIC.IMAGE_NT_OPTIONAL_HDR_MAGIC,

        /// <summary>
        /// Represents the magic number that identifies a 32-bit optional header in a Portable Executable (PE) file
        /// format.
        /// </summary>
        /// <remarks>This constant is used to distinguish the 32-bit PE optional header from other header
        /// formats, such as the 64-bit variant, when parsing or generating PE files.</remarks>
        IMAGE_NT_OPTIONAL_HDR32_MAGIC = Windows.Win32.System.Diagnostics.Debug.IMAGE_OPTIONAL_HEADER_MAGIC.IMAGE_NT_OPTIONAL_HDR32_MAGIC,

        /// <summary>
        /// Represents the magic number that identifies a 64-bit optional header in a Portable Executable (PE) file
        /// format.
        /// </summary>
        /// <remarks>Use this constant to determine whether the optional header in a PE file is in the
        /// 64-bit format. This is essential when parsing or validating PE file structures to ensure compatibility with
        /// 64-bit binaries.</remarks>
        IMAGE_NT_OPTIONAL_HDR64_MAGIC = Windows.Win32.System.Diagnostics.Debug.IMAGE_OPTIONAL_HEADER_MAGIC.IMAGE_NT_OPTIONAL_HDR64_MAGIC,

        /// <summary>
        /// Represents the magic number that identifies the optional header for a ROM image in the Windows Portable
        /// Executable (PE) file format.
        /// </summary>
        /// <remarks>Use this constant to determine whether a PE file's optional header corresponds to a
        /// ROM image. This is relevant when parsing or analyzing PE files at a low level, such as in custom loaders or
        /// diagnostic tools.</remarks>
        IMAGE_ROM_OPTIONAL_HDR_MAGIC = Windows.Win32.System.Diagnostics.Debug.IMAGE_OPTIONAL_HEADER_MAGIC.IMAGE_ROM_OPTIONAL_HDR_MAGIC,
    }
}
