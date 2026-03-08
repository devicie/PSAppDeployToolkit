namespace PSADT.Interop
{
    /// <summary>
    /// Specifies the revision version of a Windows certificate structure as defined by the Win32 API.
    /// </summary>
    /// <remarks>Use this enumeration to indicate the certificate revision when working with Windows
    /// certificate structures, such as those used in Authenticode signatures. The values correspond to the revision
    /// identifiers defined by the Windows API and are required for correct interoperability with native Windows
    /// security features.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1027:Mark enums with FlagsAttribute", Justification = "This is not a bitfield...")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "These are as they're typed in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These are as they're named in the Win32 API.")]
    public enum WIN_CERT_REVISION : ushort
    {
        /// <summary>
        /// Specifies the Windows certificate revision version 1.0.
        /// </summary>
        WIN_CERT_REVISION_1_0 = (ushort)Windows.Win32.PInvoke.WIN_CERT_REVISION_1_0,

        /// <summary>
        /// Specifies the certificate revision value 2.0, as defined by the Windows API, for use in certificate
        /// structures.
        /// </summary>
        WIN_CERT_REVISION_2_0 = (ushort)Windows.Win32.PInvoke.WIN_CERT_REVISION_2_0,
    }
}
