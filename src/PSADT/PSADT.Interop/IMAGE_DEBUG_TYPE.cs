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
        /// Represents OMAP (Optimized MAP) data for mapping from optimized addresses to source addresses.
        /// </summary>
        /// <remarks>Contains an array of OMAP structures used when code has been optimized and addresses
        /// have been rearranged. Each entry maps an RVA to its original RVA.</remarks>
        IMAGE_DEBUG_TYPE_OMAP_TO_SRC = 7,

        /// <summary>
        /// Represents OMAP (Optimized MAP) data for mapping from source addresses to optimized addresses.
        /// </summary>
        /// <remarks>Contains an array of OMAP structures used when code has been optimized and addresses
        /// have been rearranged. Each entry maps an original RVA to its optimized RVA.</remarks>
        IMAGE_DEBUG_TYPE_OMAP_FROM_SRC = 8,

        /// <summary>
        /// Represents the Borland debug type in the Windows debugging system.
        /// </summary>
        /// <remarks>This value identifies debug information specific to Borland compilers.</remarks>
        IMAGE_DEBUG_TYPE_BORLAND = Windows.Win32.System.Diagnostics.Debug.IMAGE_DEBUG_TYPE.IMAGE_DEBUG_TYPE_BORLAND,

        /// <summary>
        /// Reserved for future use (also known as BBT - Branch Boundary Table).
        /// </summary>
        IMAGE_DEBUG_TYPE_BBT = 10,

        /// <summary>
        /// Contains a CLSID (Class ID) GUID.
        /// </summary>
        IMAGE_DEBUG_TYPE_CLSID = 11,

        /// <summary>
        /// Contains Visual C++ feature information including compiler flags and counts.
        /// </summary>
        /// <remarks>Contains five uint values: PreVC11, C/C++, /GS, /sdl, and guardN counts.</remarks>
        IMAGE_DEBUG_TYPE_VC_FEATURE = 12,

        /// <summary>
        /// Contains Profile Guided Optimization (POGO) information.
        /// </summary>
        /// <remarks>Contains a signature followed by POGO entries with RVA, size, and name for each section.</remarks>
        IMAGE_DEBUG_TYPE_POGO = 13,

        /// <summary>
        /// Indicates Incremental Link Time Code Generation was used.
        /// </summary>
        /// <remarks>This is a marker type with no associated data.</remarks>
        IMAGE_DEBUG_TYPE_ILTCG = 14,

        /// <summary>
        /// Contains Intel Memory Protection Extensions (MPX) information.
        /// </summary>
        IMAGE_DEBUG_TYPE_MPX = 15,

        /// <summary>
        /// Contains reproducible build hash information.
        /// </summary>
        /// <remarks>Contains a hash that can be used to verify reproducible builds.</remarks>
        IMAGE_DEBUG_TYPE_REPRO = 16,

        /// <summary>
        /// Contains embedded portable PDB debug information.
        /// </summary>
        /// <remarks>The data contains a compressed portable PDB embedded directly in the PE file.</remarks>
        IMAGE_DEBUG_TYPE_EMBEDDED_PORTABLE_PDB = 17,

        /// <summary>
        /// Contains Static Profile Guided Optimization (SPGO) information.
        /// </summary>
        IMAGE_DEBUG_TYPE_SPGO = 18,

        /// <summary>
        /// Contains the hash of the PDB file for verification.
        /// </summary>
        /// <remarks>Used to verify the PDB file matches the executable.</remarks>
        IMAGE_DEBUG_TYPE_PDBCHECKSUM = 19,

        /// <summary>
        /// Contains extended DLL characteristics flags.
        /// </summary>
        /// <remarks>Contains additional DLL characteristics that don't fit in the standard header field.</remarks>
        IMAGE_DEBUG_TYPE_EX_DLLCHARACTERISTICS = 20,
    }
}
