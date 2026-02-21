using System.IO;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents CodeView debug information parsed from an IMAGE_DEBUG_TYPE_CODEVIEW entry.
    /// </summary>
    /// <remarks>
    /// CodeView data contains a 4-byte signature that identifies the format:
    /// - "NB09" (0x3930424E): CodeView 4.10 format (VC++ 2.x), embedded debug info
    /// - "NB10" (0x3031424E): PDB 2.0 format (VC++ 4.x-6.x), external PDB
    /// - "NB11" (0x3131424E): CodeView 5.0/C7 format, embedded debug info
    /// - "RSDS" (0x53445352): PDB 7.0 format (VS 2002+), external PDB with GUID
    /// </remarks>
    public abstract record CvInfoBase
    {
        /// <summary>
        /// Parses CodeView data from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader positioned at the CodeView data.</param>
        /// <param name="size">The size of the CodeView data in bytes.</param>
        /// <returns>A CodeViewInfo instance, or null if the format is unrecognized.</returns>
        internal static CvInfoBase? Parse(BinaryReader reader, uint size)
        {
            return size < 4 ? null : (CODEVIEW_SIGNATURE)reader.ReadUInt32() switch
            {
                CODEVIEW_SIGNATURE.CODEVIEW_SIGNATURE_NB09 => CvInfoCv41.Parse(reader),
                CODEVIEW_SIGNATURE.CODEVIEW_SIGNATURE_NB10 => CvInfoPdb20.Parse(reader),
                CODEVIEW_SIGNATURE.CODEVIEW_SIGNATURE_NB11 => CvInfoCv50.Parse(reader),
                CODEVIEW_SIGNATURE.CODEVIEW_SIGNATURE_RSDS => CvInfoPdb70.Parse(reader),
                _ => null
            };
        }

        /// <summary>
        /// Initializes a new instance of the CodeViewInfo class.
        /// </summary>
        /// <param name="signature">The signature identifying the CodeView format.</param>
        /// <param name="age">The age value used for PDB matching.</param>
        /// <param name="pdbPath">The path to the PDB file.</param>
        private protected CvInfoBase(CODEVIEW_SIGNATURE signature, uint age, string? pdbPath = null)
        {
            Signature = signature;
            Age = age;
            PdbPath = !string.IsNullOrWhiteSpace(pdbPath) ? pdbPath : null;
        }

        /// <summary>
        /// Gets the signature identifying the CodeView format.
        /// </summary>
        public CODEVIEW_SIGNATURE Signature { get; }

        /// <summary>
        /// Gets the age value used for PDB matching.
        /// </summary>
        public uint Age { get; }

        /// <summary>
        /// Gets the path to the PDB file.
        /// </summary>
        public string? PdbPath { get; }
    }
}
