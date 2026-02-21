using System;

namespace PSADT.Interop
{
    /// <summary>
    /// Defines flags that specify the characteristics of a Dynamic Link Library (DLL) image, controlling aspects of its
    /// loading, execution, and security behavior in the Windows operating system.
    /// </summary>
    /// <remarks>Use the values of this enumeration to indicate or query specific features and requirements of
    /// a DLL, such as support for address space layout randomization (ASLR), data execution prevention (DEP), control
    /// flow guard (CFG), application container compatibility, and other security or compatibility options. These flags
    /// correspond directly to the IMAGE_DLL_CHARACTERISTICS field in the Windows Portable Executable (PE) file format
    /// and are typically set by the linker or examined by system utilities and loaders. Multiple flags can be combined
    /// to represent the full set of characteristics for a given DLL.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "The type is correct for the underlying Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1069:Enums values should not be duplicated", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1700:Do not name enum values 'Reserved'", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [Flags]
    public enum IMAGE_DLL_CHARACTERISTICS : ushort
    {
        /// <summary>
        /// Specifies that the DLL supports high entropy virtual addresses, enabling the use of a larger address space
        /// and providing enhanced security through improved address space layout randomization (ASLR).
        /// </summary>
        /// <remarks>This characteristic is primarily relevant for 64-bit applications running on
        /// operating systems that support high entropy virtual addresses. Enabling this flag can help mitigate certain
        /// types of security vulnerabilities by making it more difficult for attackers to predict the location of code
        /// and data in memory.</remarks>
        IMAGE_DLLCHARACTERISTICS_HIGH_ENTROPY_VA = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_HIGH_ENTROPY_VA,

        /// <summary>
        /// Represents the dynamic base address characteristic for a DLL, indicating that the DLL can be relocated at
        /// load time.
        /// </summary>
        /// <remarks>When this characteristic is set, the operating system can load the DLL at a different
        /// address than its preferred base address, which helps avoid address space conflicts with other
        /// modules.</remarks>
        IMAGE_DLLCHARACTERISTICS_DYNAMIC_BASE = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_DYNAMIC_BASE,

        /// <summary>
        /// Specifies that the DLL enforces code and data integrity checks at load time.
        /// </summary>
        /// <remarks>This flag enhances the security of the DLL by ensuring that its code and data have
        /// not been tampered with. It is part of the IMAGE_DLL_CHARACTERISTICS enumeration and is typically used in
        /// scenarios where integrity verification is required to prevent unauthorized modifications.</remarks>
        IMAGE_DLLCHARACTERISTICS_FORCE_INTEGRITY = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_FORCE_INTEGRITY,

        /// <summary>
        /// Specifies that the DLL is compatible with the NX (No eXecute) processor feature, which helps prevent
        /// execution of code in certain areas of memory to enhance security.
        /// </summary>
        /// <remarks>This flag is part of the IMAGE_DLL_CHARACTERISTICS enumeration and indicates that the
        /// DLL supports Data Execution Prevention (DEP). Enabling NX compatibility can help mitigate certain types of
        /// security vulnerabilities by marking memory regions as non-executable.</remarks>
        IMAGE_DLLCHARACTERISTICS_NX_COMPAT = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_NX_COMPAT,

        /// <summary>
        /// Specifies that the DLL does not use isolation, allowing it to share resources with other DLLs.
        /// </summary>
        /// <remarks>This value is used in the context of Windows DLLs to indicate that the DLL can
        /// operate without isolation from other DLLs. Disabling isolation may affect resource sharing and loading
        /// behavior, and is typically relevant when managing application compatibility or resource access across
        /// multiple DLLs.</remarks>
        IMAGE_DLLCHARACTERISTICS_NO_ISOLATION = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_NO_ISOLATION,

        /// <summary>
        /// Specifies that the DLL does not use Structured Exception Handling (SEH).
        /// </summary>
        /// <remarks>This characteristic indicates that the DLL is not designed to handle exceptions using
        /// SEH, which may affect how exceptions are managed in applications that load this DLL.</remarks>
        IMAGE_DLLCHARACTERISTICS_NO_SEH = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_NO_SEH,

        /// <summary>
        /// Specifies that the DLL does not require binding to any other DLLs at load time.
        /// </summary>
        /// <remarks>This characteristic indicates that the DLL can be loaded without needing to resolve
        /// dependencies, which may improve load performance in certain scenarios.</remarks>
        IMAGE_DLLCHARACTERISTICS_NO_BIND = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_NO_BIND,

        /// <summary>
        /// Indicates that the DLL is intended to run within an application container, enabling additional security and
        /// isolation constraints.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_DLL_CHARACTERISTICS enumeration and is used to
        /// specify that a DLL supports execution in an application container environment. Application containers are
        /// commonly used to restrict the capabilities of applications and enhance security by isolating them from the
        /// rest of the system.</remarks>
        IMAGE_DLLCHARACTERISTICS_APPCONTAINER = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_APPCONTAINER,

        /// <summary>
        /// Indicates that the image is a Windows Driver Model (WDM) driver.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_DLL_CHARACTERISTICS enumeration and is used by the
        /// operating system to identify DLLs that implement WDM drivers. Setting this characteristic ensures that the
        /// image is loaded and managed according to WDM driver requirements.</remarks>
        IMAGE_DLLCHARACTERISTICS_WDM_DRIVER = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_WDM_DRIVER,

        /// <summary>
        /// Represents the Control Flow Guard (CFG) characteristic for a DLL, indicating that the DLL is protected by
        /// control flow guard security features.
        /// </summary>
        /// <remarks>Control Flow Guard is a security feature that helps prevent indirect call hijacking
        /// by validating the target of indirect calls at runtime. This characteristic is set by the linker when CFG is
        /// enabled for the DLL.</remarks>
        IMAGE_DLLCHARACTERISTICS_GUARD_CF = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_GUARD_CF,

        /// <summary>
        /// Specifies that the DLL is aware of and can operate correctly in terminal server environments.
        /// </summary>
        /// <remarks>This constant indicates that the DLL is designed to function properly when loaded in
        /// a terminal server context, which may affect its behavior and resource management. Use this flag to ensure
        /// compatibility with terminal server features such as session isolation and resource redirection.</remarks>
        IMAGE_DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_TERMINAL_SERVER_AWARE,

        /// <summary>
        /// Specifies that the DLL is compatible with Control-flow Enforcement Technology (CET), a security feature
        /// supported by modern Windows operating systems.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_DLL_CHARACTERISTICS enumeration and indicates that
        /// the DLL supports CET, which helps protect against certain classes of exploits by enforcing control-flow
        /// integrity. Use this flag to ensure compatibility with enhanced security environments on Windows.</remarks>
        IMAGE_DLLCHARACTERISTICS_EX_CET_COMPAT = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_EX_CET_COMPAT,

        /// <summary>
        /// Represents the strict mode compatibility setting for Control-flow Enforcement Technology (CET) in DLL
        /// characteristics.
        /// </summary>
        /// <remarks>This constant is used to indicate that a DLL operates in strict CET compatibility
        /// mode. Enabling strict mode may affect how the operating system enforces security features related to
        /// control-flow integrity for the DLL.</remarks>
        IMAGE_DLLCHARACTERISTICS_EX_CET_COMPAT_STRICT_MODE = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_EX_CET_COMPAT_STRICT_MODE,

        /// <summary>
        /// Specifies the DLL characteristic that enables relaxed instruction pointer (IP) validation when setting the
        /// execution context for a DLL.
        /// </summary>
        /// <remarks>This characteristic allows for more permissive checks on the instruction pointer
        /// during context switching, which can be useful in certain debugging or diagnostic scenarios. Use with
        /// caution, as relaxed validation may impact security or application stability.</remarks>
        IMAGE_DLLCHARACTERISTICS_EX_CET_SET_CONTEXT_IP_VALIDATION_RELAXED_MODE = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_EX_CET_SET_CONTEXT_IP_VALIDATION_RELAXED_MODE,

        /// <summary>
        /// Specifies that dynamic APIs are permitted to be called in-process by the DLL.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_DLLCHARACTERISTICS enumeration and indicates that the
        /// DLL supports dynamic APIs that can be invoked from within the same process. This characteristic may be
        /// relevant for compatibility or security considerations when loading or interacting with DLLs that expose
        /// dynamic APIs.</remarks>
        IMAGE_DLLCHARACTERISTICS_EX_CET_DYNAMIC_APIS_ALLOW_IN_PROC = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_EX_CET_DYNAMIC_APIS_ALLOW_IN_PROC,

        /// <summary>
        /// Represents a reserved DLL characteristic flag corresponding to IMAGE_DLLCHARACTERISTICS_EX_CET_RESERVED_1.
        /// </summary>
        /// <remarks>This value is reserved for future use and should not be relied upon in current
        /// applications. It is defined by the Windows API for potential compatibility or security features related to
        /// Control-flow Enforcement Technology (CET).</remarks>
        IMAGE_DLLCHARACTERISTICS_EX_CET_RESERVED_1 = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_EX_CET_RESERVED_1,

        /// <summary>
        /// Represents a reserved characteristic for the DLL, specifically the
        /// IMAGE_DLLCHARACTERISTICS_EX_CET_RESERVED_2 value.
        /// </summary>
        /// <remarks>This field is part of the IMAGE_DLL_CHARACTERISTICS enumeration and is reserved for
        /// future use by the Windows operating system. It should not be used or modified in application code.</remarks>
        IMAGE_DLLCHARACTERISTICS_EX_CET_RESERVED_2 = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_EX_CET_RESERVED_2,

        /// <summary>
        /// Specifies that the DLL supports forward control flow integrity (CFI) compatibility.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_DLL_CHARACTERISTICS enumeration and indicates that
        /// the DLL is compatible with forward CFI, which helps enhance security by ensuring that control flow is
        /// maintained as intended.</remarks>
        IMAGE_DLLCHARACTERISTICS_EX_FORWARD_CFI_COMPAT = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_EX_FORWARD_CFI_COMPAT,

        /// <summary>
        /// Specifies that the DLL is compatible with hot patching, allowing it to be updated in memory without
        /// requiring an application restart.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_DLL_CHARACTERISTICS enumeration and indicates that
        /// the DLL supports hot patching. Hot patching enables updates to be applied to the DLL while it is loaded,
        /// which can be useful for applying security fixes or updates without interrupting running
        /// applications.</remarks>
        IMAGE_DLLCHARACTERISTICS_EX_HOTPATCH_COMPATIBLE = Windows.Win32.System.Diagnostics.Debug.IMAGE_DLL_CHARACTERISTICS.IMAGE_DLLCHARACTERISTICS_EX_HOTPATCH_COMPATIBLE,
    }
}
