using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using PSADT.Interop;
using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a single FPO (Frame Pointer Omission) entry from an IMAGE_DEBUG_TYPE_FPO debug directory.
    /// </summary>
    /// <remarks>
    /// FPO data describes functions where the frame pointer has been omitted for optimization.
    /// This is primarily used for x86 binaries and helps debuggers properly unwind the stack.
    /// </remarks>
    public sealed record FpoData
    {
        /// <summary>
        /// Parses FPO data from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader positioned at the FPO data.</param>
        /// <param name="size">The size of the FPO data in bytes.</param>
        /// <returns>An ImageDebugFpoData instance, or null if the data is invalid.</returns>
        internal static ReadOnlyCollection<FpoData>? Parse(BinaryReader reader, uint size)
        {
            if (size < EntrySize)
            {
                return null;
            }
            int entryCount = (int)(size / EntrySize);
            List<FpoData> entries = new(entryCount);
            for (int i = 0; i < entryCount; i++)
            {
                entries.Add(new(in PortableExecutableUtilities.ReadStruct<FPO_DATA>(reader)));
            }
            return new(entries);
        }

        /// <summary>
        /// Initializes a new instance of the FpoData class.
        /// </summary>
        private FpoData(in FPO_DATA fpoData)
        {
            _fpoData = fpoData;
        }

        /// <summary>
        /// Gets the offset of the first byte of the function code.
        /// </summary>
        public uint OffsetStart => _fpoData.ulOffStart;

        /// <summary>
        /// Gets the size of the function in bytes.
        /// </summary>
        public uint ProcedureSize => _fpoData.cbProcSize;

        /// <summary>
        /// Gets the size of local variables in DWORDs.
        /// </summary>
        public uint LocalsSize => _fpoData.cdwLocals;

        /// <summary>
        /// Gets the size of parameters in DWORDs.
        /// </summary>
        public ushort ParamsSize => _fpoData.cdwParams;

        /// <summary>
        /// Gets the size of the function prolog in bytes.
        /// </summary>
        public byte PrologSize => _fpoData.cbProlog;

        /// <summary>
        /// Gets the count of saved registers.
        /// </summary>
        public byte SavedRegsCount => _fpoData.cbRegs;

        /// <summary>
        /// Gets a value indicating whether the function uses structured exception handling.
        /// </summary>
        public bool HasSeh => _fpoData.fHasSEH;

        /// <summary>
        /// Gets a value indicating whether EBP is used as a base pointer.
        /// </summary>
        public bool UsesBasePointer => _fpoData.fUseBP;

        /// <summary>
        /// Gets the frame type for this function.
        /// </summary>
        public FRAME FrameType => (FRAME)_fpoData.cbFrame;

        /// <summary>
        /// The underlying FPO_DATA structure.
        /// </summary>
        private readonly FPO_DATA _fpoData;

        /// <summary>
        /// The size of each FPO_DATA entry in bytes.
        /// </summary>
        private static readonly uint EntrySize = (uint)Unsafe.SizeOf<FPO_DATA>();
    }
}
