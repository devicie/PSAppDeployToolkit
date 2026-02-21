namespace PSADT.Interop
{
    /// <summary>
    /// Defines the various frame types used in Windows structured exception handling and API interactions.
    /// </summary>
    /// <remarks>This enumeration includes constants that represent specific frame types, which are essential
    /// for low-level programming scenarios involving exception handling, stack unwinding, and thread-specific storage
    /// in Windows environments.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<Pending>")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "<Pending>")]
    public enum FRAME : byte
    {
        /// <summary>
        /// Represents the FRAME_FPO frame type used in Windows structured exception handling.
        /// </summary>
        /// <remarks>This constant corresponds to the FRAME_FPO value defined in the Windows.Win32.PInvoke
        /// namespace. It is typically used to identify a specific frame type when working with low-level exception
        /// handling or stack unwinding operations in Windows environments.</remarks>
        FRAME_FPO = (byte)Windows.Win32.PInvoke.FRAME_FPO,

        /// <summary>
        /// Represents the constant value for FRAME_TRAP used in Windows API calls.
        /// </summary>
        /// <remarks>This constant is typically used in low-level programming scenarios involving Windows
        /// API interactions, particularly in the context of frame manipulation.</remarks>
        FRAME_TRAP = (byte)Windows.Win32.PInvoke.FRAME_TRAP,

        /// <summary>
        /// Represents the FRAME_TSS structure used for thread-specific storage in Windows API operations.
        /// </summary>
        /// <remarks>This field references the FRAME_TSS structure defined in the Windows.Win32.PInvoke
        /// namespace, which facilitates management of thread-specific data in Windows environments. Use this member
        /// when interacting with APIs that require thread-local storage structures.</remarks>
        FRAME_TSS = (byte)Windows.Win32.PInvoke.FRAME_TSS,

        /// <summary>
        /// Represents a constant value indicating that a frame is non-FPO (Frame Pointer Omission).
        /// </summary>
        /// <remarks>This constant is used in the context of Windows API calls to specify the frame type
        /// for stack unwinding. It is important for developers working with low-level memory management and
        /// debugging.</remarks>
        FRAME_NONFPO = (byte)Windows.Win32.PInvoke.FRAME_NONFPO,
    }
}
