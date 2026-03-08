namespace PSADT.Interop
{
    /// <summary>
    /// Specifies the types of certificates recognized by Windows for use in cryptographic operations.
    /// </summary>
    /// <remarks>Use this enumeration to indicate the format or standard of a certificate when working with
    /// Windows security APIs. The values correspond to certificate types such as X.509, PKCS#7 signed data, reserved
    /// types, and timestamp stack-signed certificates, as defined by the Windows API.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "These are as they're typed in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1700:Do not name enum values 'Reserved'", Justification = "These are as they're typed in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These are as they're named in the Win32 API.")]
    public enum WIN_CERT_TYPE : ushort
    {
        /// <summary>
        /// Represents the X.509 certificate type constant used in Windows certificate operations.
        /// </summary>
        WIN_CERT_TYPE_X509 = (ushort)Windows.Win32.PInvoke.WIN_CERT_TYPE_X509,

        /// <summary>
        /// Represents the constant value for PKCS#7 signed data certificate type used in Windows certificate
        /// operations.
        /// </summary>
        WIN_CERT_TYPE_PKCS_SIGNED_DATA = (ushort)Windows.Win32.PInvoke.WIN_CERT_TYPE_PKCS_SIGNED_DATA,

        /// <summary>
        /// Represents a reserved certificate type used in Windows cryptographic operations.
        /// </summary>
        WIN_CERT_TYPE_RESERVED_1 = (ushort)Windows.Win32.PInvoke.WIN_CERT_TYPE_RESERVED_1,

        /// <summary>
        /// Represents the certificate type identifier for a timestamp stack-signed certificate used in Windows API
        /// operations.
        /// </summary>
        WIN_CERT_TYPE_TS_STACK_SIGNED = (ushort)Windows.Win32.PInvoke.WIN_CERT_TYPE_TS_STACK_SIGNED,
    }
}
