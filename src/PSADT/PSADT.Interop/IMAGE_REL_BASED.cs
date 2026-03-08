namespace PSADT.Interop
{
    /// <summary>
    /// Specifies the type of base relocation to apply when an executable is loaded at an address different from its
    /// preferred base address.
    /// </summary>
    /// <remarks>These values correspond to the high 4 bits of each entry in a base relocation block. The
    /// low 12 bits specify the offset within the page where the relocation should be applied.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "The type is correct for the underlying 4-bit field.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1069:Enums values should not be duplicated", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These values are precisely as they're defined in the Win32 API.")]
    public enum IMAGE_REL_BASED : byte
    {
        /// <summary>
        /// The relocation is skipped. This type is used for padding.
        /// </summary>
        IMAGE_REL_BASED_ABSOLUTE = (byte)Windows.Win32.PInvoke.IMAGE_REL_BASED_ABSOLUTE,

        /// <summary>
        /// The base relocation adds the high 16 bits of the difference to the 16-bit field at offset.
        /// </summary>
        IMAGE_REL_BASED_HIGH = (byte)Windows.Win32.PInvoke.IMAGE_REL_BASED_HIGH,

        /// <summary>
        /// The base relocation adds the low 16 bits of the difference to the 16-bit field at offset.
        /// </summary>
        IMAGE_REL_BASED_LOW = (byte)Windows.Win32.PInvoke.IMAGE_REL_BASED_LOW,

        /// <summary>
        /// The base relocation applies all 32 bits of the difference to the 32-bit field at offset.
        /// </summary>
        IMAGE_REL_BASED_HIGHLOW = (byte)Windows.Win32.PInvoke.IMAGE_REL_BASED_HIGHLOW,

        /// <summary>
        /// The base relocation adds the high 16 bits of the difference to the 16-bit field at offset,
        /// adjusted for sign extension of the low 16 bits. The next entry must have a type of ABSOLUTE.
        /// </summary>
        IMAGE_REL_BASED_HIGHADJ = (byte)Windows.Win32.PInvoke.IMAGE_REL_BASED_HIGHADJ,

        /// <summary>
        /// The relocation applies to a MIPS jump instruction.
        /// </summary>
        IMAGE_REL_BASED_MIPS_JMPADDR = (byte)Windows.Win32.PInvoke.IMAGE_REL_BASED_MIPS_JMPADDR,

        /// <summary>
        /// The base relocation applies the difference to the ARM MOV32 instruction pair at offset.
        /// </summary>
        IMAGE_REL_BASED_ARM_MOV32 = (byte)Windows.Win32.PInvoke.IMAGE_REL_BASED_ARM_MOV32,

        /// <summary>
        /// The base relocation applies the difference to the Thumb MOV32 instruction pair at offset.
        /// </summary>
        IMAGE_REL_BASED_THUMB_MOV32 = (byte)Windows.Win32.PInvoke.IMAGE_REL_BASED_THUMB_MOV32,

        /// <summary>
        /// The relocation applies to a MIPS16 jump instruction.
        /// </summary>
        IMAGE_REL_BASED_MIPS_JMPADDR16 = (byte)Windows.Win32.PInvoke.IMAGE_REL_BASED_MIPS_JMPADDR16,

        /// <summary>
        /// The base relocation applies the difference to the 64-bit field at offset.
        /// </summary>
        IMAGE_REL_BASED_DIR64 = (byte)Windows.Win32.PInvoke.IMAGE_REL_BASED_DIR64,
    }
}
