using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using PSADT.Interop;
using Windows.Win32.Security.WinTrust;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a Windows certificate, encapsulating the WIN_CERTIFICATE structure and associated X.509 certificates.
    /// </summary>
    /// <remarks>This class provides access to the properties of the WIN_CERTIFICATE structure, including the
    /// length, revision, and type of the certificate, as well as the associated raw certificate data. Use this class to
    /// inspect certificate information embedded in a portable executable file, such as Authenticode signatures. The
    /// certificate data is read-only and cannot be modified after initialization.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "These values are precisely as they're defined in the Win32 API.")]
    public sealed record WinCertificate
    {
        /// <summary>
        /// Initializes a new instance of the WinCertificate class using the specified certificate structure and raw
        /// certificate data.
        /// </summary>
        /// <param name="winCertificate">A WIN_CERTIFICATE structure that contains the certificate metadata and information.</param>
        /// <param name="certifcateBytes">A byte array containing the raw certificate data associated with the WIN_CERTIFICATE structure. Cannot be
        /// null.</param>
        internal WinCertificate(in WIN_CERTIFICATE winCertificate, byte[] certifcateBytes)
        {
            Revision = (WIN_CERT_REVISION)winCertificate.wRevision;
            CertificateType = (WIN_CERT_TYPE)winCertificate.wCertificateType;
            CertificateBytes = certifcateBytes;
        }

        /// <summary>
        /// Gets the certificate revision.
        /// </summary>
        public WIN_CERT_REVISION Revision { get; }

        /// <summary>
        /// Gets the certificate type.
        /// </summary>
        public WIN_CERT_TYPE CertificateType { get; }

        /// <summary>
        /// Gets the collection of X.509 certificates associated with the signed CMS message.
        /// </summary>
        /// <remarks>The returned collection contains the certificates that are included with the signed
        /// CMS and can be used to validate the digital signature. Ensure that each certificate in the collection is
        /// valid and trusted before relying on the signature for security-sensitive operations.</remarks>
        public X509Certificate2Collection Certificates => GetSignedCms().Certificates;

        /// <summary>
        /// Verifies the digital signature of the signed CMS/PKCS #7 message, with an option to validate only the
        /// signature or both the signature and the signed content.
        /// </summary>
        /// <remarks>Ensure that the signed CMS message is properly initialized before calling this
        /// method. This method delegates signature verification to the underlying CMS object and does not return a
        /// value; it throws an exception if verification fails.</remarks>
        /// <param name="verifySignatureOnly">true to verify only the digital signature without validating the signed content; false to verify both the
        /// signature and the integrity of the signed content.</param>
        public void CheckSignature(bool verifySignatureOnly)
        {
            GetSignedCms().CheckSignature(verifySignatureOnly);
        }

        /// <summary>
        /// Decodes the certificate data contained in the CertificateBytes field and returns a SignedCms object
        /// representing the decoded certificate.
        /// </summary>
        /// <remarks>This method assumes that the CertificateBytes field contains valid encoded
        /// certificate data in a format supported by SignedCms. If the data is invalid or improperly formatted, an
        /// exception may be thrown during decoding.</remarks>
        /// <returns>A SignedCms object that represents the decoded certificate data.</returns>
        private SignedCms GetSignedCms()
        {
            SignedCms cms = new(); cms.Decode(CertificateBytes);
            return cms;
        }

        /// <summary>
        /// Gets the raw certificate data (PKCS#7 SignedData for Authenticode signatures).
        /// </summary>
        private readonly byte[] CertificateBytes;
    }
}
