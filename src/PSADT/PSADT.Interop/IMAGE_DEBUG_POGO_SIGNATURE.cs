namespace PSADT.Interop
{
    /// <summary>
    /// Defines the set of signatures used to identify Profile-Guided Optimization (PGO) debug information within image
    /// files. These signatures distinguish between various optimization techniques, such as Link-Time Code Generation
    /// (LTCG), standard PGO, and sample-based PGO, enabling tools and compilers to interpret and process optimization
    /// data appropriately.
    /// </summary>
    /// <remarks>This enumeration is intended for internal use when parsing or generating image files that
    /// contain PGO-related debug information. Each value corresponds to a specific optimization strategy or data
    /// format, which may affect how the debug information is handled during compilation or analysis. Refer to official
    /// documentation or relevant image file specifications for details on the meaning and usage of each
    /// signature.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "These are signature values, not flags.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "The type is correct for the underlying Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These are as they're defined within the API.")]
    public enum IMAGE_DEBUG_POGO_SIGNATURE : uint
    {
        /// <summary>
        /// Represents the signature for the POGO (Profile-Guided Optimization) debug information in the context of
        /// Link-Time Code Generation (LTCG).
        /// </summary>
        /// <remarks>https://github.com/winsiderss/systeminformer/blob/af8f8d245a5e8c934bbbe5de9de3869974f56a4e/phnt/include/ntimage.h#L34</remarks>
        IMAGE_DEBUG_POGO_SIGNATURE_LTCG = 0x4C544347,

        /// <summary>
        /// Represents the signature for the POGO (Profile-Guided Optimization) debug information in an image file.
        /// </summary>
        /// <remarks>https://github.com/winsiderss/systeminformer/blob/af8f8d245a5e8c934bbbe5de9de3869974f56a4e/phnt/include/ntimage.h#L35</remarks>
        IMAGE_DEBUG_POGO_SIGNATURE_PGI = 0x50474900,

        /// <summary>
        /// Represents the signature for Profile-Guided Optimization (PGO) data in image debug information.
        /// </summary>
        /// <remarks>https://github.com/winsiderss/systeminformer/blob/af8f8d245a5e8c934bbbe5de9de3869974f56a4e/phnt/include/ntimage.h#L36</remarks>
        IMAGE_DEBUG_POGO_SIGNATURE_PGO = 0x50474F00,

        /// <summary>
        /// Represents the signature for the POGO (Profile-Guided Optimization) debug information in an image.
        /// </summary>
        /// <remarks>https://github.com/winsiderss/systeminformer/blob/af8f8d245a5e8c934bbbe5de9de3869974f56a4e/phnt/include/ntimage.h#L37</remarks>
        IMAGE_DEBUG_POGO_SIGNATURE_PGU = 0x50475500,

        /// <summary>
        /// Represents the signature for the SPGO (Sample-Based Profile-Guided Optimization) debug information in an
        /// image.
        /// </summary>
        /// <remarks>https://github.com/winsiderss/systeminformer/blob/af8f8d245a5e8c934bbbe5de9de3869974f56a4e/phnt/include/ntimage.h#L38</remarks>
        IMAGE_DEBUG_POGO_SIGNATURE_SPGO = 0x5350474F,
    }
}
