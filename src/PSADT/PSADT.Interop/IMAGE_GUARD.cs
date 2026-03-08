using System;

namespace PSADT.Interop
{
    /// <summary>
    /// Defines constants that represent various states of image guard instrumentation and security features in the
    /// Windows API.
    /// </summary>
    /// <remarks>These constants are used to indicate the presence of Control Flow Guard (CFG) features in an
    /// image, which enhance security by preventing certain types of attacks. The values correspond to those defined in
    /// the Win32 API and are typically used when inspecting or modifying image headers for security
    /// instrumentation.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These are as they're named in the Win32 API.")]
    [Flags]
    public enum IMAGE_GUARD : uint
    {
        /// <summary>
        /// Represents the IMAGE_GUARD_CF_INSTRUMENTED constant, which indicates that control flow guard (CFG)
        /// instrumentation is applied to the image.
        /// </summary>
        IMAGE_GUARD_CF_INSTRUMENTED = Windows.Win32.PInvoke.IMAGE_GUARD_CF_INSTRUMENTED,

        /// <summary>
        /// Specifies that the image is instrumented with Control Flow Guard (CFW) security features.
        /// </summary>
        IMAGE_GUARD_CFW_INSTRUMENTED = Windows.Win32.PInvoke.IMAGE_GUARD_CFW_INSTRUMENTED,

        /// <summary>
        /// Indicates that the image contains a Control Flow Guard (CFG) function table.
        /// </summary>
        IMAGE_GUARD_CF_FUNCTION_TABLE_PRESENT = Windows.Win32.PInvoke.IMAGE_GUARD_CF_FUNCTION_TABLE_PRESENT,

        /// <summary>
        /// Represents a constant value that indicates an unused security cookie in the IMAGE_GUARD structure.
        /// </summary>
        IMAGE_GUARD_SECURITY_COOKIE_UNUSED = Windows.Win32.PInvoke.IMAGE_GUARD_SECURITY_COOKIE_UNUSED,
    }
}
