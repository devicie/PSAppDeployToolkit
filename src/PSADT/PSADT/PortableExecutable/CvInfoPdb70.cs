using System;
using System.IO;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents RSDS (PDB 7.0) CodeView debug information.
    /// </summary>
    /// <remarks>
    /// The RSDS format is used by modern Visual Studio compilers and contains:
    /// - 4-byte signature "RSDS" (0x53445352)
    /// - 16-byte GUID
    /// - 4-byte age
    /// - Null-terminated UTF-8 PDB path
    /// </remarks>
    public sealed record CvInfoPdb70 : CvInfoBase
    {
        /// <summary>
        /// Parses RSDS CodeView data from the given reader.
        /// </summary>
        internal static CvInfoPdb70 Parse(BinaryReader reader)
        {
            // Read the 16-byte GUID
            byte[] guidBytes = reader.ReadBytes(16);
            Guid pdbGuid = new(guidBytes);

            // Read the 4-byte age
            uint age = reader.ReadUInt32();

            // Read the null-terminated PDB path
            string pdbPath = PortableExecutableUtilities.ReadNullTerminatedUtf8String(reader);
            return new(pdbGuid, age, pdbPath);
        }

        /// <summary>
        /// Initializes a new instance of the CodeViewRsdsInfo class.
        /// </summary>
        private CvInfoPdb70(Guid pdbGuid, uint age, string pdbPath) : base(CODEVIEW_SIGNATURE.CODEVIEW_SIGNATURE_RSDS, age, pdbPath)
        {
            PdbGuid = pdbGuid;
        }

        /// <summary>
        /// Gets the GUID that uniquely identifies the PDB file.
        /// </summary>
        public Guid PdbGuid { get; }

        /// <summary>
        /// Gets the PDB identifier string in the format used by symbol servers: GUID + Age (no hyphens).
        /// </summary>
        /// <remarks>
        /// This is the format used in Microsoft Symbol Server paths:
        /// {PdbFileName}/{GuidNoHyphens}{Age}/{PdbFileName}
        /// </remarks>
        public override string ToString()
        {
            return $"{PdbGuid:N}{Age:X}";
        }
    }
}
