using System.IO;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents NB11 (CodeView 5.0 / C7) debug information.
    /// </summary>
    /// <remarks>
    /// The NB11 format is a legacy format used by VC++ compilers and contains:
    /// - 4-byte signature "NB11" (0x3131424E)
    /// - 4-byte file position offset to debug info within the PE file
    /// This format predates external PDB files; debug info is embedded in the executable.
    /// Also known as "C7" (CodeView 7) format.
    /// </remarks>
    public sealed record CvInfoCv50 : CvInfoBase
    {
        /// <summary>
        /// Parses NB11 CodeView data from the given reader.
        /// </summary>
        internal static CvInfoCv50 Parse(BinaryReader reader)
        {
            // Read the 4-byte file position offset
            return new(reader.ReadUInt32());
        }

        /// <summary>
        /// Initializes a new instance of the CvInfoCv50 class.
        /// </summary>
        private CvInfoCv50(uint filePosition) : base(CODEVIEW_SIGNATURE.CODEVIEW_SIGNATURE_NB11, age: 0)
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
