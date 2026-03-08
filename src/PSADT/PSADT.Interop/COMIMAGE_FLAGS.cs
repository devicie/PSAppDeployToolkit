using System;

namespace PSADT.Interop
{
    /// <summary>
    /// Specifies flags that define the characteristics and behavior of a Common Object Model (COM) image.
    /// </summary>
    /// <remarks>These flags indicate properties such as whether the image contains only Microsoft
    /// intermediate language (IL) code, requires a 32-bit process, is strong name signed, or has other specific
    /// requirements. They are typically used when working with managed assemblies to control how the image is loaded
    /// and executed by the runtime.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These are as they're named in the Win32 API.")]
    [Flags]
    public enum COMIMAGE_FLAGS
    {
        /// <summary>
        /// Specifies that the image contains only Microsoft intermediate language (MSIL) code and does not include any
        /// native code.
        /// </summary>
        /// <remarks>Use this flag to indicate that the image is intended to run exclusively in a managed
        /// execution environment. This ensures that the image can be loaded and executed by the common language runtime
        /// (CLR) without requiring native code support.</remarks>
        COMIMAGE_FLAGS_ILONLY = Windows.Win32.System.SystemServices.ReplacesCorHdrNumericDefines.COMIMAGE_FLAGS_ILONLY,

        /// <summary>
        /// Specifies that the associated COM image requires execution in a 32-bit environment.
        /// </summary>
        /// <remarks>Use this flag to indicate that the COM image is not compatible with 64-bit processes
        /// and must be run in a 32-bit context. This is important for ensuring compatibility with 32-bit components or
        /// dependencies.</remarks>
        COMIMAGE_FLAGS_32BITREQUIRED = Windows.Win32.System.SystemServices.ReplacesCorHdrNumericDefines.COMIMAGE_FLAGS_32BITREQUIRED,

        /// <summary>
        /// Specifies that the COM image is an Intermediate Language (IL) library.
        /// </summary>
        /// <remarks>This flag is part of the COMIMAGE_FLAGS enumeration and is used to indicate that the
        /// associated COM image represents an IL library rather than a native binary. It is relevant when processing or
        /// analyzing COM images to determine their type or intended usage.</remarks>
        COMIMAGE_FLAGS_IL_LIBRARY = Windows.Win32.System.SystemServices.ReplacesCorHdrNumericDefines.COMIMAGE_FLAGS_IL_LIBRARY,

        /// <summary>
        /// Represents the flag that indicates a COM image is signed with a strong name.
        /// </summary>
        /// <remarks>This flag is used to identify assemblies that have been strong name signed, which
        /// provides identity and versioning guarantees. Strong name signing helps ensure the integrity and uniqueness
        /// of the assembly.</remarks>
        COMIMAGE_FLAGS_STRONGNAMESIGNED = Windows.Win32.System.SystemServices.ReplacesCorHdrNumericDefines.COMIMAGE_FLAGS_STRONGNAMESIGNED,

        /// <summary>
        /// Specifies that the entry point of the COM image is a native entry point.
        /// </summary>
        /// <remarks>This flag is used in the COMIMAGE_FLAGS enumeration to indicate that the image's
        /// entry point is native code rather than managed code. It is relevant when working with COM image headers and
        /// can affect how the image is loaded and executed by the runtime.</remarks>
        COMIMAGE_FLAGS_NATIVE_ENTRYPOINT = Windows.Win32.System.SystemServices.ReplacesCorHdrNumericDefines.COMIMAGE_FLAGS_NATIVE_ENTRYPOINT,

        /// <summary>
        /// Specifies that debug data is tracked for the COM image.
        /// </summary>
        /// <remarks>This flag indicates that debug information should be included in the COM image, which
        /// can assist with debugging and diagnostics. Enabling this flag may increase the size of the image but
        /// provides additional information useful during development and troubleshooting.</remarks>
        COMIMAGE_FLAGS_TRACKDEBUGDATA = Windows.Win32.System.SystemServices.ReplacesCorHdrNumericDefines.COMIMAGE_FLAGS_TRACKDEBUGDATA,

        /// <summary>
        /// Specifies that the COM image is preferred to be loaded as a 32-bit image when possible.
        /// </summary>
        /// <remarks>This flag is used in the context of COM image loading to indicate a preference for
        /// 32-bit architecture. It is part of the COMIMAGE_FLAGS enumeration and may affect how the image is loaded on
        /// systems that support both 32-bit and 64-bit execution.</remarks>
        COMIMAGE_FLAGS_32BITPREFERRED = Windows.Win32.System.SystemServices.ReplacesCorHdrNumericDefines.COMIMAGE_FLAGS_32BITPREFERRED,
    }
}
