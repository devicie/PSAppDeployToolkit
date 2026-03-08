namespace PSADT.Interop
{
    /// <summary>
    /// Defines the types of debug information that can be associated with a binary image file.
    /// </summary>
    /// <remarks>Each member of this enumeration represents a specific debug information format or type that
    /// may be present in a binary image, such as an executable or DLL. These debug types are used by debuggers and
    /// analysis tools to interpret and utilize debugging data embedded within or associated with the image.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These are as they're named in the Win32 API.")]
    public enum IMAGE_DEBUG_TYPE : uint
    {
        /// <summary>
        /// Represents an unknown or unspecified type of debug information in the image debug directory.
        /// </summary>
        IMAGE_DEBUG_TYPE_UNKNOWN = Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_UNKNOWN,

        /// <summary>
        /// Represents the COFF (Common Object File Format) debug type used in image debugging.
        /// </summary>
        IMAGE_DEBUG_TYPE_COFF = Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_COFF,

        /// <summary>
        /// Represents the CodeView debug information type, used to link to PDB files.
        /// </summary>
        /// <remarks>CodeView is a debugging format used by Microsoft development tools to store symbol
        /// and debugging information. The data contains a signature (RSDS or NB10) followed by PDB path information.</remarks>
        IMAGE_DEBUG_TYPE_CODEVIEW = Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_CODEVIEW,

        /// <summary>
        /// Specifies the Frame Pointer Omission (FPO) debug type used in image debugging information.
        /// </summary>
        /// <remarks>Contains an array of FPO_DATA structures describing functions where the frame pointer
        /// has been omitted. This is primarily used for x86 binaries.</remarks>
        IMAGE_DEBUG_TYPE_FPO = Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_FPO,

        /// <summary>
        /// Represents the miscellaneous debug information type containing external debug file paths.
        /// </summary>
        /// <remarks>Contains an IMAGE_DEBUG_MISC structure with the path to an external DBG file.</remarks>
        IMAGE_DEBUG_TYPE_MISC = Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_MISC,

        /// <summary>
        /// Represents a copy of the .pdata section for exception handling information.
        /// </summary>
        IMAGE_DEBUG_TYPE_EXCEPTION = Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_EXCEPTION,

        /// <summary>
        /// Reserved for fixup information.
        /// </summary>
        IMAGE_DEBUG_TYPE_FIXUP = Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_FIXUP,

        /// <summary>
        /// Represents the Borland debug type in the Windows debugging system.
        /// </summary>
        /// <remarks>This value identifies debug information specific to Borland compilers.</remarks>
        IMAGE_DEBUG_TYPE_BORLAND = Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_BORLAND,
    }
}
