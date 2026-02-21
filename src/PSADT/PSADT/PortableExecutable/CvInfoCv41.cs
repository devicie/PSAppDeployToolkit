using System.IO;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents NB09 (CodeView 4.10) debug information.
    /// </summary>
    /// <remarks>
    /// The NB09 format is a legacy format used by VC++ 2.x compilers and contains:
    /// - 4-byte signature "NB09" (0x3930424E)
    /// - 4-byte file position offset to debug info within the PE file
    /// This format predates external PDB files; debug info is embedded in the executable.
    /// </remarks>
    public sealed record CvInfoCv41 : CvInfoBase
    {
        /// <summary>
        /// Parses NB09 CodeView data from the given reader.
        /// </summary>
        internal static CvInfoCv41 Parse(BinaryReader reader)
        {
            // Read the 4-byte file position offset
            return new(reader.ReadUInt32());
        }

        /// <summary>
        /// Initializes a new instance of the CvInfoCv41 class.
        /// </summary>
        private CvInfoCv41(uint filePosition) : base(CODEVIEW_SIGNATURE.CODEVIEW_SIGNATURE_NB09, age: 0)
        {
            FilePosition = filePosition;
        }

        /// <summary>
        /// Gets the file position offset to the debug information within the PE file.
        /// </summary>
        /// <remarks>
        /// This is an offset from the start of the file to the embedded CodeView debug data.
        /// </remarks>
        public uint FilePosition { get; }
    }
}
