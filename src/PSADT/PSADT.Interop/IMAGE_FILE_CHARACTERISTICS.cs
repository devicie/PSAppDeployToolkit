using System;

namespace PSADT.Interop
{
    /// <summary>
    /// Specifies the characteristics of an image file, such as executability, system file status, and various loading
    /// or optimization behaviors.
    /// </summary>
    /// <remarks>This enumeration defines flags that describe how an image file is handled by the operating
    /// system. The values correspond to the IMAGE_FILE_CHARACTERISTICS flags used in the Windows Portable Executable
    /// (PE) file format. These flags indicate properties such as whether the file is a DLL, if relocation or debugging
    /// information has been stripped, or if the file is intended for a specific system configuration. Some values are
    /// obsolete and retained for compatibility with the Win32 API.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "The type is correct for the underlying Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1712:Do not prefix enum values with type name", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [Flags]
    public enum IMAGE_FILE_CHARACTERISTICS : ushort
    {
        /// <summary>
        /// Relocation information was stripped from the file. The file must be loaded at its preferred base address. If the base address is not available, the loader reports an error.
        /// </summary>
        IMAGE_FILE_RELOCS_STRIPPED = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_RELOCS_STRIPPED,

        /// <summary>
        /// The file is executable (there are no unresolved external references).
        /// </summary>
        IMAGE_FILE_EXECUTABLE_IMAGE = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_EXECUTABLE_IMAGE,

        /// <summary>
        /// COFF line numbers were stripped from the file.
        /// </summary>
        IMAGE_FILE_LINE_NUMS_STRIPPED = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_LINE_NUMS_STRIPPED,

        /// <summary>
        /// COFF symbol table entries were stripped from file.
        /// </summary>
        IMAGE_FILE_LOCAL_SYMS_STRIPPED = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_LOCAL_SYMS_STRIPPED,

        /// <summary>
        /// Aggressively trim the working set. This value is obsolete.
        /// </summary>
        IMAGE_FILE_AGGRESIVE_WS_TRIM = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_AGGRESIVE_WS_TRIM,

        /// <summary>
        /// The application can handle addresses larger than 2 GB.
        /// </summary>
        IMAGE_FILE_LARGE_ADDRESS_AWARE = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_LARGE_ADDRESS_AWARE,

        /// <summary>
        /// The bytes of the word are reversed. This flag is obsolete.
        /// </summary>
        IMAGE_FILE_BYTES_REVERSED_LO = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_BYTES_REVERSED_LO,

        /// <summary>
        /// The computer supports 32-bit words.
        /// </summary>
        IMAGE_FILE_32BIT_MACHINE = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_32BIT_MACHINE,

        /// <summary>
        /// Debugging information was removed and stored separately in another file.
        /// </summary>
        IMAGE_FILE_DEBUG_STRIPPED = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_DEBUG_STRIPPED,

        /// <summary>
        /// If the image is on removable media, copy it to and run it from the swap file.
        /// </summary>
        IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP,

        /// <summary>
        /// If the image is on the network, copy it to and run it from the swap file.
        /// </summary>
        IMAGE_FILE_NET_RUN_FROM_SWAP = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_NET_RUN_FROM_SWAP,

        /// <summary>
        /// The image is a system file.
        /// </summary>
        IMAGE_FILE_SYSTEM = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_SYSTEM,

        /// <summary>
        /// The image is a DLL file. While it is an executable file, it cannot be run directly.
        /// </summary>
        IMAGE_FILE_DLL = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_DLL,

        /// <summary>
        /// The file should be run only on a uniprocessor computer.
        /// </summary>
        IMAGE_FILE_UP_SYSTEM_ONLY = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_UP_SYSTEM_ONLY,

        /// <summary>
        /// The bytes of the word are reversed. This flag is obsolete.
        /// </summary>
        IMAGE_FILE_BYTES_REVERSED_HI = Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_CHARACTERISTICS.IMAGE_FILE_BYTES_REVERSED_HI,
    }
}
