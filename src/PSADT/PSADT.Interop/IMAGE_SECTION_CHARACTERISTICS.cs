using System;

namespace PSADT.Interop
{
    /// <summary>
    /// Specifies the characteristics and attributes of a section in a Portable Executable (PE) file.
    /// </summary>
    /// <remarks>This enumeration defines flags used to describe the properties of sections within a PE file,
    /// such as whether a section contains code, initialized or uninitialized data, and its memory access permissions
    /// (read, write, execute). These values correspond directly to the Win32 API and are used by linkers and loaders to
    /// determine how each section should be handled in memory. Multiple flags can be combined using a bitwise OR
    /// operation to represent a section with multiple characteristics.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "The type is correct for the underlying Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1069:Enums values should not be duplicated", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These values are precisely as they're defined in the Win32 API.")]
    [Flags]
    public enum IMAGE_SECTION_CHARACTERISTICS : uint
    {
        /// <summary>
        /// Represents the section characteristic that specifies no padding is required for the section in a Portable
        /// Executable (PE) file.
        /// </summary>
        /// <remarks>This value is used in section headers to indicate that the section's data does not
        /// need to be aligned with padding bytes. It is typically relevant when working with low-level file formats or
        /// interoperability scenarios involving PE files.</remarks>
        IMAGE_SCN_TYPE_NO_PAD = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_TYPE_NO_PAD,

        /// <summary>
        /// Represents the section characteristic that identifies a section in a Portable Executable (PE) file as
        /// containing executable code.
        /// </summary>
        /// <remarks>This value is used when working with PE file formats to specify that a section holds
        /// code that can be executed by the operating system. It is part of the IMAGE_SECTION_CHARACTERISTICS
        /// enumeration and is commonly used in scenarios involving low-level file analysis or manipulation of Windows
        /// executables.</remarks>
        IMAGE_SCN_CNT_CODE = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_CNT_CODE,

        /// <summary>
        /// Specifies that the section contains initialized data in a Windows executable file.
        /// </summary>
        /// <remarks>Use this constant to identify sections that store data initialized at compile time,
        /// which is important for correct loading and execution of Windows binaries. This characteristic is commonly
        /// used when working with PE (Portable Executable) file formats.</remarks>
        IMAGE_SCN_CNT_INITIALIZED_DATA = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_CNT_INITIALIZED_DATA,

        /// <summary>
        /// Represents a section characteristic that indicates the section contains uninitialized data.
        /// </summary>
        /// <remarks>Use this constant when defining or inspecting image section characteristics to
        /// specify that the section does not contain initialized data. This affects how the section is loaded and
        /// managed in memory by the operating system.</remarks>
        IMAGE_SCN_CNT_UNINITIALIZED_DATA = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_CNT_UNINITIALIZED_DATA,

        /// <summary>
        /// Indicates that the section is linked in a manner defined by the operating system or linker, but the specific
        /// meaning is reserved for future use.
        /// </summary>
        /// <remarks>This constant is part of the IMAGE_SECTION_CHARACTERISTICS enumeration and is
        /// typically encountered when analyzing or manipulating Windows executable files. Its value is reserved and not
        /// currently used by standard Windows tools. Developers should avoid relying on this flag for custom section
        /// handling, as its behavior may change in future versions of the Windows platform.</remarks>
        IMAGE_SCN_LNK_OTHER = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_LNK_OTHER,

        /// <summary>
        /// Indicates that the section contains link information in a Windows executable file.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_SECTION_CHARACTERISTICS enumeration and is typically
        /// used when analyzing or working with Portable Executable (PE) files. Sections marked with this characteristic
        /// may contain additional data used by the linker or for debugging purposes.</remarks>
        IMAGE_SCN_LNK_INFO = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_LNK_INFO,

        /// <summary>
        /// Represents the section characteristic that indicates the section should be removed during linking.
        /// </summary>
        /// <remarks>This value is typically used to mark sections in a portable executable (PE) file that
        /// are not needed in the final linked output. Sections with this characteristic are omitted by the linker,
        /// which can help reduce the size of the resulting binary.</remarks>
        IMAGE_SCN_LNK_REMOVE = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_LNK_REMOVE,

        /// <summary>
        /// Represents the section characteristic that indicates the section is a COMDAT section, which allows the
        /// linker to eliminate duplicate sections during linking.
        /// </summary>
        /// <remarks>COMDAT sections are used to manage duplicate symbols in object files, enabling more
        /// efficient linking and memory usage. This characteristic is typically used in scenarios where multiple object
        /// files may define the same data or function, and the linker must select a single instance.</remarks>
        IMAGE_SCN_LNK_COMDAT = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_LNK_COMDAT,

        /// <summary>
        /// Specifies that the section does not defer specification exceptions.
        /// </summary>
        /// <remarks>This characteristic indicates that the section will not defer exceptions related to
        /// specifications, which can affect how the section is processed during execution.</remarks>
        IMAGE_SCN_NO_DEFER_SPEC_EXC = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_NO_DEFER_SPEC_EXC,

        /// <summary>
        /// Represents the section characteristic that indicates the section contains data that is relative to the
        /// global pointer (GP).
        /// </summary>
        /// <remarks>This value is used in Portable Executable (PE) file section headers to mark sections
        /// whose data can be accessed using the global pointer, which is relevant for certain processor architectures.
        /// It is primarily used in low-level Windows development and PE file analysis.</remarks>
        IMAGE_SCN_GPREL = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_GPREL,

        /// <summary>
        /// Specifies that the section contains far data in a Windows executable file.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_SECTION_CHARACTERISTICS enumeration and is used to
        /// indicate that a section holds data accessible via far addressing. This characteristic is relevant for
        /// certain memory models and may affect how the section is accessed or managed by the operating system or
        /// loader.</remarks>
        IMAGE_SCN_MEM_FARDATA = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_FARDATA,

        /// <summary>
        /// Represents a section characteristic that indicates the section is purgeable, allowing the operating system
        /// to reclaim its memory if needed.
        /// </summary>
        /// <remarks>This characteristic is typically used for sections that can be discarded when system
        /// memory is low. Marking a section as purgeable can help optimize memory usage in applications by enabling the
        /// operating system to free memory resources when appropriate.</remarks>
        IMAGE_SCN_MEM_PURGEABLE = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_PURGEABLE,

        /// <summary>
        /// Indicates that the section contains code or data that is memory-mapped as 16-bit in a Windows executable
        /// file.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_SECTION_CHARACTERISTICS enumeration and is used when
        /// working with Portable Executable (PE) file formats. It is primarily relevant for low-level programming,
        /// debugging, or tools that analyze or manipulate PE files.</remarks>
        IMAGE_SCN_MEM_16BIT = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_16BIT,

        /// <summary>
        /// Represents a section characteristic that indicates the memory for the section is locked and cannot be paged
        /// out.
        /// </summary>
        /// <remarks>Use this constant when specifying image section characteristics to ensure that the
        /// associated memory remains resident in physical memory and is not swapped to disk. This is typically used for
        /// sections that require high performance or must remain available at all times.</remarks>
        IMAGE_SCN_MEM_LOCKED = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_LOCKED,

        /// <summary>
        /// Represents the section characteristic that indicates the section should be preloaded into memory before
        /// execution.
        /// </summary>
        /// <remarks>This value is used in the context of executable file sections to specify that the
        /// section's data is to be loaded into memory prior to execution. It is part of the
        /// IMAGE_SECTION_CHARACTERISTICS enumeration and is typically relevant when working with low-level Windows
        /// executable formats.</remarks>
        IMAGE_SCN_MEM_PRELOAD = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_PRELOAD,

        /// <summary>
        /// Specifies that a section is aligned on a 1-byte boundary in memory.
        /// </summary>
        /// <remarks>This constant is used when defining section characteristics for executable files or
        /// object files. Aligning a section to 1-byte boundaries may impact performance and compatibility, as most
        /// systems perform optimally with larger alignment values. Use this alignment only when required by the file
        /// format or specific use cases.</remarks>
        IMAGE_SCN_ALIGN_1BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_1BYTES,

        /// <summary>
        /// Specifies that section data must be aligned on a 2-byte boundary within a Portable Executable (PE) file.
        /// </summary>
        /// <remarks>Use this constant when defining section characteristics to ensure that the section's
        /// data alignment meets the requirements for proper loading and execution of PE files. Incorrect alignment may
        /// result in loading errors or unexpected behavior on some platforms.</remarks>
        IMAGE_SCN_ALIGN_2BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_2BYTES,

        /// <summary>
        /// Specifies that a section is aligned on a 4-byte boundary in the Windows Portable Executable (PE) file
        /// format.
        /// </summary>
        /// <remarks>Use this constant when defining section characteristics to ensure proper alignment
        /// for data or code that requires 4-byte boundaries. Proper alignment can be important for performance and
        /// compatibility with certain processors and tools.</remarks>
        IMAGE_SCN_ALIGN_4BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_4BYTES,

        /// <summary>
        /// Specifies that a section in an image is aligned on an 8-byte boundary when loaded into memory.
        /// </summary>
        /// <remarks>Use this constant to indicate the required alignment for sections within a binary
        /// image, such as a PE (Portable Executable) file. Proper alignment is important for performance and
        /// compatibility on certain architectures.</remarks>
        IMAGE_SCN_ALIGN_8BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_8BYTES,

        /// <summary>
        /// Specifies that a section in a Windows portable executable (PE) image is aligned on a 16-byte boundary.
        /// </summary>
        /// <remarks>Use this constant when defining section alignment to ensure compatibility with tools
        /// and systems that require or benefit from 16-byte alignment. Proper alignment can improve performance and is
        /// sometimes required by certain hardware architectures or operating system loaders.</remarks>
        IMAGE_SCN_ALIGN_16BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_16BYTES,

        /// <summary>
        /// Represents the section alignment characteristic of 32 bytes for an image in the Windows PE format.
        /// </summary>
        /// <remarks>This constant is used to specify the alignment of sections in a Portable Executable
        /// (PE) file, ensuring that sections are aligned to 32-byte boundaries for optimal performance and
        /// compatibility.</remarks>
        IMAGE_SCN_ALIGN_32BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_32BYTES,

        /// <summary>
        /// Specifies that a section in a Portable Executable (PE) file is aligned on a 64-byte boundary.
        /// </summary>
        /// <remarks>Use this constant to ensure that sections within a PE file meet the 64-byte alignment
        /// requirement, which can improve performance and compatibility on certain systems.</remarks>
        IMAGE_SCN_ALIGN_64BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_64BYTES,

        /// <summary>
        /// Specifies that a section is aligned on a 128-byte boundary when loaded into memory.
        /// </summary>
        /// <remarks>This constant is used when defining section characteristics for executable files or
        /// object files. Proper alignment can improve memory access performance and is required for certain hardware or
        /// operating system requirements.</remarks>
        IMAGE_SCN_ALIGN_128BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_128BYTES,

        /// <summary>
        /// Specifies that a section in a Portable Executable (PE) file is aligned on a 256-byte boundary.
        /// </summary>
        /// <remarks>Use this constant to ensure that sections within a PE file are aligned to 256-byte
        /// boundaries, which can be required for compatibility or performance reasons when working with Windows
        /// executable formats.</remarks>
        IMAGE_SCN_ALIGN_256BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_256BYTES,

        /// <summary>
        /// Specifies that a section in a binary file is aligned on a 512-byte boundary.
        /// </summary>
        /// <remarks>Use this constant when defining section characteristics for executable files to
        /// ensure compatibility with file formats or systems that require 512-byte alignment. Proper alignment can
        /// improve performance and is often necessary for correct loading and execution of binaries on certain
        /// platforms.</remarks>
        IMAGE_SCN_ALIGN_512BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_512BYTES,

        /// <summary>
        /// Specifies that a section in a Portable Executable (PE) file is aligned on a 1024-byte boundary.
        /// </summary>
        /// <remarks>Use this constant to ensure that sections within a PE file are aligned to 1024-byte
        /// boundaries, which can be required for compatibility or performance reasons when working with Windows
        /// executable formats.</remarks>
        IMAGE_SCN_ALIGN_1024BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_1024BYTES,

        /// <summary>
        /// Represents the section alignment of 2048 bytes for an image in the Windows Portable Executable (PE) format.
        /// </summary>
        /// <remarks>Use this value to specify that sections within a PE file should be aligned on
        /// 2048-byte boundaries. Proper alignment can be important for compatibility and performance on certain
        /// hardware architectures.</remarks>
        IMAGE_SCN_ALIGN_2048BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_2048BYTES,

        /// <summary>
        /// Represents the section characteristic that aligns data on 4096-byte boundaries within a Portable Executable
        /// (PE) file.
        /// </summary>
        /// <remarks>Use this value to specify that a section in a PE file should be aligned to a
        /// 4096-byte boundary. Proper alignment is required for compatibility with the Windows loader and can affect
        /// performance and memory usage.</remarks>
        IMAGE_SCN_ALIGN_4096BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_4096BYTES,

        /// <summary>
        /// Represents the section characteristic that aligns data on 8192-byte boundaries within a Portable Executable
        /// (PE) file.
        /// </summary>
        /// <remarks>Use this value when defining or interpreting section alignment in PE files to ensure
        /// compatibility with tools and loaders that require or expect 8192-byte alignment. Proper alignment can impact
        /// performance and correctness when loading or mapping sections in memory.</remarks>
        IMAGE_SCN_ALIGN_8192BYTES = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_8192BYTES,

        /// <summary>
        /// Gets the alignment mask for section characteristics in the Windows Portable Executable (PE) format.
        /// </summary>
        /// <remarks>This mask is used to determine the alignment requirements for sections in a PE file.
        /// Proper alignment is necessary to ensure that sections are loaded correctly in memory, which can impact
        /// performance and compatibility with Windows operating systems.</remarks>
        IMAGE_SCN_ALIGN_MASK = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_ALIGN_MASK,

        /// <summary>
        /// Represents the IMAGE_SCN_LNK_NRELOC_OVFL section characteristic, indicating that the section contains an
        /// overflow of relocations.
        /// </summary>
        /// <remarks>This characteristic is used in the context of PE (Portable Executable) files to
        /// specify that the section has more relocations than can be represented in the standard relocation
        /// table.</remarks>
        IMAGE_SCN_LNK_NRELOC_OVFL = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_LNK_NRELOC_OVFL,

        /// <summary>
        /// Represents a section characteristic that indicates the memory for the section can be discarded after it is
        /// no longer needed.
        /// </summary>
        /// <remarks>This characteristic is typically applied to sections that are only required during
        /// the initial loading or execution of an image, such as initialization data. Marking a section as discardable
        /// allows the operating system to reclaim the associated memory, which can help reduce the application's memory
        /// footprint.</remarks>
        IMAGE_SCN_MEM_DISCARDABLE = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_DISCARDABLE,

        /// <summary>
        /// Specifies that the section is not cached in memory.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_SECTION_CHARACTERISTICS enumeration and is used to
        /// indicate that a section in an executable file should not be cached by the system. This may affect
        /// performance and access speed, and is typically used for sections that require direct access to hardware or
        /// memory-mapped devices.</remarks>
        IMAGE_SCN_MEM_NOT_CACHED = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_NOT_CACHED,

        /// <summary>
        /// Represents a section characteristic that specifies the memory associated with the section is not pageable.
        /// </summary>
        /// <remarks>Use this constant when defining or inspecting image section characteristics to
        /// indicate that the section's memory must remain resident in physical memory and cannot be paged out to disk.
        /// This is typically used for sections that require high performance or must always be available.</remarks>
        IMAGE_SCN_MEM_NOT_PAGED = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_NOT_PAGED,

        /// <summary>
        /// Specifies that the section is shared in memory and can be accessed by multiple processes.
        /// </summary>
        /// <remarks>This constant is used with image section characteristics to indicate that the
        /// section's memory can be shared across different processes. This is typically relevant when working with
        /// executable images or memory-mapped files in Windows.</remarks>
        IMAGE_SCN_MEM_SHARED = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_SHARED,

        /// <summary>
        /// Indicates that the section is executable in memory.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_SECTION_CHARACTERISTICS enumeration and specifies
        /// that the section can contain executable code. It is typically used when working with Windows executable file
        /// formats to identify sections that the processor is permitted to execute.</remarks>
        IMAGE_SCN_MEM_EXECUTE = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_EXECUTE,

        /// <summary>
        /// Represents a section attribute that indicates the section can be read from memory.
        /// </summary>
        /// <remarks>This value is part of the IMAGE_SECTION_CHARACTERISTICS enumeration and is used when
        /// working with Windows executable file sections to specify that the section's contents are readable at
        /// runtime.</remarks>
        IMAGE_SCN_MEM_READ = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_READ,

        /// <summary>
        /// Specifies that the section is writable when loaded into memory.
        /// </summary>
        /// <remarks>This constant is part of the IMAGE_SECTION_CHARACTERISTICS enumeration and is used to
        /// indicate that a section in a Windows executable file can be written to at runtime. Setting this flag allows
        /// the section's memory to be modified, which may have security and performance implications. Use caution when
        /// marking sections as writable, especially in security-sensitive applications.</remarks>
        IMAGE_SCN_MEM_WRITE = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_MEM_WRITE,

        /// <summary>
        /// Specifies that the section contains scaled data, as indicated by the IMAGE_SCN_SCALE_INDEX flag in the
        /// Windows API.
        /// </summary>
        /// <remarks>This constant is used with image section characteristics to indicate that the
        /// section's data is subject to scaling. This may affect how the section is interpreted or processed by tools
        /// that handle Windows executable images.</remarks>
        IMAGE_SCN_SCALE_INDEX = Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_CHARACTERISTICS.IMAGE_SCN_SCALE_INDEX,
    }
}
