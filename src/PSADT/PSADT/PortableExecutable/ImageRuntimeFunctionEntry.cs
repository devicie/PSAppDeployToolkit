using System.Runtime.InteropServices;
using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a parsed exception handling entry (runtime function).
    /// </summary>
    public sealed record ImageRuntimeFunctionEntry
    {
        /// <summary>
        /// Initializes a new instance of the ExceptionEntry class using the specified runtime function entry.
        /// </summary>
        /// <param name="entry">The runtime function entry that contains the exception handling information for a specific function.</param>
        internal ImageRuntimeFunctionEntry(in IMAGE_RUNTIME_FUNCTION_ENTRY entry)
        {
            Anonymous = new(entry.Anonymous.UnwindInfoAddress, entry.Anonymous.UnwindData);
            Entry = entry;
        }

        /// <summary>
        /// Gets the RVA of the start of the function.
        /// </summary>
        public uint BeginAddress => Entry.BeginAddress;

        /// <summary>
        /// Gets the RVA of the end of the function.
        /// </summary>
        public uint EndAddress => Entry.EndAddress;

        /// <summary>
        /// Gets the anonymous union containing unwind information.
        /// </summary>
        public ImageRuntimeFunctionEntry0 Anonymous { get; }

        /// <summary>
        /// Gets the size of the function in bytes.
        /// </summary>
        public uint FunctionSize => EndAddress - BeginAddress;

        /// <summary>
        /// Gets the raw IMAGE_RUNTIME_FUNCTION_ENTRY structure.
        /// </summary>
        private readonly IMAGE_RUNTIME_FUNCTION_ENTRY Entry;

        /// <summary>
        /// Represents the anonymous union containing unwind information for exception handling.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "This is meant to reflect an anonymous union.")]
        [StructLayout(LayoutKind.Explicit)]
        public readonly record struct ImageRuntimeFunctionEntry0
        {
            /// <summary>
            /// Initializes a new instance of the ImageRuntimeFunctionEntry0 struct.
            /// </summary>
            /// <param name="unwindInfoAddress">The RVA of the unwind information.</param>
            /// <param name="unwindData">The unwind data.</param>
            internal ImageRuntimeFunctionEntry0(uint unwindInfoAddress, uint unwindData)
            {
                UnwindInfoAddress = unwindInfoAddress;
                UnwindData = unwindData;
            }

            /// <summary>
            /// Gets the RVA of the unwind information.
            /// </summary>
            [FieldOffset(0)]
            public readonly uint UnwindInfoAddress;

            /// <summary>
            /// Gets the unwind data.
            /// </summary>
            [FieldOffset(0)]
            public readonly uint UnwindData;
        }
    }
}
