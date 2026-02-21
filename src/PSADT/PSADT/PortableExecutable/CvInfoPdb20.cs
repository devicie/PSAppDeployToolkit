using System.IO;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents NB10 (PDB 2.0) CodeView debug information.
    /// </summary>
    /// <remarks>
    /// The NB10 format is a legacy format used by older compilers and contains:
    /// - 4-byte signature "NB10" (0x3031424E)
    /// - 4-byte offset (usually 0)
    /// - 4-byte timestamp signature
    /// - 4-byte age
    /// - Null-terminated PDB path
    /// </remarks>
    public sealed record CvInfoPdb20 : CvInfoBase
    {
        /// <summary>
        /// Parses NB10 CodeView data from the given reader.
        /// </summary>
        internal static CvInfoPdb20 Parse(BinaryReader reader)
        {
            // Read the 4-byte offset
            uint offset = reader.ReadUInt32();

            // Read the 4-byte timestamp signature
            uint timeDateStamp = reader.ReadUInt32();

            // Read the 4-byte age
            uint age = reader.ReadUInt32();

            // Read the null-terminated PDB path
            string pdbPath = PortableExecutableUtilities.ReadNullTerminatedUtf8String(reader);
            return new(offset, timeDateStamp, age, pdbPath);
        }

        /// <summary>
        /// Initializes a new instance of the CodeViewNb10Info class.
        /// </summary>
        private CvInfoPdb20(uint offset, uint timeDateStamp, uint age, string pdbPath) : base(CODEVIEW_SIGNATURE.CODEVIEW_SIGNATURE_NB10, age, pdbPath)
        {
            Offset = offset;
            TimeDateStamp = timeDateStamp;
        }

        /// <summary>
        /// Gets the offset value (typically 0).
        /// </summary>
        public uint Offset { get; }

        /// <summary>
        /// Gets the timestamp used as a signature for the PDB file.
        /// </summary>
        public uint TimeDateStamp { get; }

        /// <summary>
        /// Gets the PDB identifier string in the format: TimeDateStamp + Age (hex).
        /// </summary>
        public override string ToString()
        {
            return $"{TimeDateStamp:X8}{Age:X}";
        }
    }
}
