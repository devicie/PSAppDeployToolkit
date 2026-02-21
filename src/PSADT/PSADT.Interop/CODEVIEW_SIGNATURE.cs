namespace PSADT.Interop
{
    /// <summary>
    /// Defines the various CODEVIEW format signatures used for debugging information.
    /// </summary>
    /// <remarks>Each value corresponds to a specific version of the CODEVIEW format, which is essential for
    /// interpreting debugging data correctly.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "These are signature values, not flags.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "The type is correct for the underlying Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These are as they're defined within the API.")]
    public enum CODEVIEW_SIGNATURE : uint
    {
        /// <summary>
        /// Represents the signature for the CODEVIEW format version NB09.
        /// </summary>
        CODEVIEW_SIGNATURE_NB09 = 0x3930424E,

        /// <summary>
        /// Represents the signature for the CODEVIEW format version NB10, which is associated with PDB 2.0 files.
        /// </summary>
        CODEVIEW_SIGNATURE_NB10 = 0x3031424E,

        /// <summary>
        /// Represents the signature for the CODEVIEW format version NB11.
        /// </summary>
        CODEVIEW_SIGNATURE_NB11 = 0x3131424E,

        /// <summary>
        /// Represents the signature for the RSDS code view format, used in debugging information.
        /// </summary>
        CODEVIEW_SIGNATURE_RSDS = 0x53445352,
    }
}
