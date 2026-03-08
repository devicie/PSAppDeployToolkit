using System;

namespace PSADT.Interop
{
    /// <summary>
    /// Defines a set of flags that control the behavior of heap memory allocation in Windows.
    /// </summary>
    /// <remarks>These flags can be used when creating or managing heaps to specify options such as
    /// serialization, memory initialization, and error handling. Each flag modifies the behavior of the heap to suit
    /// different application needs, including performance optimizations and debugging support.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "This is how they're named within the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "This is how it's named within the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "This is how they're named within the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2217:Do not mark enums with FlagsAttribute", Justification = "This is a bitfield, though...")]
    [Flags]
    public enum HEAP_FLAGS : uint
    {
        /// <summary>
        /// Represents a flag that indicates no special heap options are set.
        /// </summary>
        /// <remarks>Use this flag when allocating memory to specify that the default heap behavior should
        /// be applied without any additional options.</remarks>
        HEAP_NONE = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_NONE,

        /// <summary>
        /// Specifies that the heap does not perform serialization, allowing for faster access in multithreaded
        /// scenarios when synchronization is managed externally.
        /// </summary>
        /// <remarks>Use this flag when creating a heap if you can guarantee that all access to the heap
        /// is properly synchronized by the application. Disabling serialization can improve performance, but it is the
        /// developer's responsibility to ensure thread safety when multiple threads access the heap
        /// concurrently.</remarks>
        HEAP_NO_SERIALIZE = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_NO_SERIALIZE,

        /// <summary>
        /// Represents a flag that enables a heap to grow dynamically as additional memory is required.
        /// </summary>
        /// <remarks>Use this flag when creating a heap to allow it to increase in size during its
        /// lifetime. This provides more flexible memory management, especially for applications with unpredictable or
        /// variable memory usage patterns.</remarks>
        HEAP_GROWABLE = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_GROWABLE,

        /// <summary>
        /// Specifies that the heap should generate exceptions on memory allocation failures.
        /// </summary>
        /// <remarks>This flag can be used when creating a heap to ensure that any allocation errors are
        /// reported via exceptions, allowing for better error handling in applications.</remarks>
        HEAP_GENERATE_EXCEPTIONS = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_GENERATE_EXCEPTIONS,

        /// <summary>
        /// Specifies a flag that indicates the memory allocated by the heap should be initialized to zero.
        /// </summary>
        /// <remarks>This flag is used when allocating memory from the heap to ensure that the allocated
        /// memory is set to zero, which can help prevent uninitialized memory access issues.</remarks>
        HEAP_ZERO_MEMORY = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_ZERO_MEMORY,

        /// <summary>
        /// Specifies that memory reallocation should occur only at the current memory block's address, without moving
        /// it to a new location.
        /// </summary>
        /// <remarks>Use this flag when pointer stability is required, as it prevents the memory block
        /// from being relocated during reallocation. If the memory cannot be expanded in place, the reallocation will
        /// fail rather than move the block.</remarks>
        HEAP_REALLOC_IN_PLACE_ONLY = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_REALLOC_IN_PLACE_ONLY,

        /// <summary>
        /// Specifies that tail checking is enabled for heap memory allocations.
        /// </summary>
        /// <remarks>When this flag is set, the system adds a small signature to the end of each allocated
        /// heap block and verifies its integrity when the block is freed. This helps detect buffer overruns that write
        /// past the end of allocated memory. Enabling tail checking may impact performance and is typically used for
        /// debugging purposes.</remarks>
        HEAP_TAIL_CHECKING_ENABLED = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_TAIL_CHECKING_ENABLED,

        /// <summary>
        /// Enables heap free checking to help detect memory corruption when managing heap memory.
        /// </summary>
        /// <remarks>This flag is primarily intended for debugging and development scenarios. When
        /// enabled, the heap manager performs additional checks on freed memory blocks to identify potential memory
        /// corruption issues. It may impact performance and should not be used in production environments.</remarks>
        HEAP_FREE_CHECKING_ENABLED = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_FREE_CHECKING_ENABLED,

        /// <summary>
        /// Specifies that the heap should not coalesce adjacent free memory blocks when memory is freed.
        /// </summary>
        /// <remarks>Use this flag when creating a heap to control how free memory blocks are managed.
        /// Disabling coalescing can help reduce heap fragmentation in scenarios where frequent allocations and
        /// deallocations of similarly sized blocks occur. However, it may also increase the number of free blocks,
        /// potentially impacting overall memory usage.</remarks>
        HEAP_DISABLE_COALESCE_ON_FREE = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_DISABLE_COALESCE_ON_FREE,

        /// <summary>
        /// Specifies the heap creation flag that aligns memory allocations to a 16-byte boundary.
        /// </summary>
        /// <remarks>This flag is used when creating a heap to ensure that all memory allocations are
        /// aligned to 16 bytes, which can improve performance on certain architectures.</remarks>
        HEAP_CREATE_ALIGN_16 = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_CREATE_ALIGN_16,

        /// <summary>
        /// Specifies that heap tracing is enabled for the created heap.
        /// </summary>
        /// <remarks>This flag can be used when creating a heap to enable tracing for memory allocations,
        /// which can assist in debugging memory-related issues.</remarks>
        HEAP_CREATE_ENABLE_TRACING = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_CREATE_ENABLE_TRACING,

        /// <summary>
        /// Specifies that the heap created with this flag allows executable memory to be allocated.
        /// </summary>
        /// <remarks>This flag is used when creating a heap to enable the allocation of executable memory,
        /// which can be necessary for certain applications that require dynamic code execution.</remarks>
        HEAP_CREATE_ENABLE_EXECUTE = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_CREATE_ENABLE_EXECUTE,

        /// <summary>
        /// Specifies the maximum tag value that can be used for heap allocations in the Windows API.
        /// </summary>
        /// <remarks>This constant is used to identify the upper limit for tag values when working with
        /// heap memory management functions. It is relevant when tracking or categorizing heap allocations using tags,
        /// and exceeding this value may result in undefined behavior or errors.</remarks>
        HEAP_MAXIMUM_TAG = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_MAXIMUM_TAG,

        /// <summary>
        /// Specifies the heap pseudo tag flag used to indicate special tagging behavior for heap allocations in Windows
        /// memory management.
        /// </summary>
        /// <remarks>This flag is part of the Windows heap management system and is typically used when
        /// working with low-level memory operations. It enables pseudo-tagging of heap allocations, which can assist in
        /// debugging and profiling memory usage. Use this flag only if you require detailed tracking of heap
        /// allocations for diagnostic purposes.</remarks>
        HEAP_PSEUDO_TAG_FLAG = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_PSEUDO_TAG_FLAG,

        /// <summary>
        /// Specifies the bit position used for heap tagging in heap flag values.
        /// </summary>
        /// <remarks>This constant is used when working with heap management APIs to identify or
        /// manipulate the tag portion of heap flags. The value corresponds to the shift amount defined by the
        /// underlying Windows API.</remarks>
        HEAP_TAG_SHIFT = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_TAG_SHIFT,

        /// <summary>
        /// Specifies that the heap to be created is a segment heap, which is optimized for scenarios involving large
        /// memory allocations and deallocations.
        /// </summary>
        /// <remarks>Use this flag when creating a heap to enable segment heap behavior. Segment heaps can
        /// provide improved performance and memory usage patterns for applications that frequently allocate and free
        /// large blocks of memory. This flag is supported on Windows 10, version 1703 and later.</remarks>
        HEAP_CREATE_SEGMENT_HEAP = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_CREATE_SEGMENT_HEAP,

        /// <summary>
        /// Specifies the flag used to create a hardened heap, which provides additional protection against certain
        /// types of memory corruption attacks.
        /// </summary>
        /// <remarks>Use this flag when creating a heap to enhance security by enabling mitigations
        /// against common heap exploitation techniques. This flag is part of the Windows heap management system and can
        /// be combined with other heap creation flags as needed.</remarks>
        HEAP_CREATE_HARDENED = Windows.Win32.System.Memory.HEAP_FLAGS.HEAP_CREATE_HARDENED,
    }
}
