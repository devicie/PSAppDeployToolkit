using System;

namespace PSADT.Interop
{
    /// <summary>
    /// Defines global flags that control various aspects of the NT environment, such as debugging and memory management
    /// behaviors. See https://www.geoffchappell.com/studies/windows/km/ntoskrnl/api/ex/sysinfo/flags.htm for more info.
    /// </summary>
    [Flags]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "This is how they're typed within the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "This is how they're named within the Win32 API.")]
    public enum NT_GLOBAL_FLAGS : uint
    {
        /// <summary>
        /// The Stop on exception flag causes the kernel to break into the kernel debugger whenever a kernel-mode exception occurs.
        /// </summary>
        FLG_STOP_ON_EXCEPTION = 0x00000001,

        /// <summary>
        /// The Show loader snaps flag captures detailed information about the loading and unloading of executable images and their supporting library modules and displays the data in the kernel debugger console.
        /// </summary>
        FLG_SHOW_LDR_SNAPS = 0x00000002,

        /// <summary>
        /// The Debug initial command flag debugs the Client Server Run-time Subsystem (CSRSS) and the WinLogon process.
        /// </summary>
        FLG_DEBUG_INITIAL_COMMAND = 0x00000004,

        /// <summary>
        /// The Stop on hung GUI flag appears in GFlags, but it has no effect on Windows.
        /// </summary>
        FLG_STOP_ON_HUNG_GUI = 0x00000008,

        /// <summary>
        /// The Enable heap tail checking flag checks for buffer overruns when the heap is freed.
        /// </summary>
        FLG_HEAP_ENABLE_TAIL_CHECK = 0x00000010,

        /// <summary>
        /// The Enable heap free checking flag validates each heap allocation when it is freed.
        /// </summary>
        FLG_HEAP_ENABLE_FREE_CHECK = 0x00000020,

        /// <summary>
        /// The Enable heap parameter checking flag verifies selected aspects of the heap whenever a heap function is called.
        /// </summary>
        FLG_HEAP_VALIDATE_PARAMETERS = 0x00000040,

        /// <summary>
        /// The Enable heap validation on call flag validates the entire heap each time a heap function is called.
        /// </summary>
        FLG_HEAP_VALIDATE_ALL = 0x00000080,

        /// <summary>
        /// The Enable application verifier flag enables system features that are used for user-mode application testing, such as page heap verification, lock checks, and handle checks.
        /// </summary>
        FLG_APPLICATION_VERIFIER = 0x00000100,

        /// <summary>
        /// The Enable silent process exit monitoring flag enables silent exit monitoring for a process.
        /// </summary>
        FLG_MONITOR_SILENT_PROCESS_EXIT = 0x00000200,

        /// <summary>
        /// The Enable pool tagging flag collects data and calculates statistics about pool memory allocations sorted by pool tag value.
        /// </summary>
        FLG_POOL_ENABLE_TAGGING = 0x00000400,

        /// <summary>
        /// The Enable heap tagging flag assigns unique tags to heap allocations.
        /// </summary>
        FLG_HEAP_ENABLE_TAGGING = 0x00000800,

        /// <summary>
        /// The Create user mode stack trace database flag creates a run-time stack trace database in the address space of a particular process (image file mode) or all processes (system-wide).
        /// </summary>
        FLG_USER_STACK_TRACE_DB = 0x00001000,

        /// <summary>
        /// The Create kernel mode stack trace database flag creates a run-time stack trace database of kernel operations, such as resource objects and object management operations, and works only when using the checked build of Windows. Checked builds were available on older versions of Windows before Windows 10, version 1803.
        /// </summary>
        FLG_KERNEL_STACK_TRACE_DB = 0x00002000,

        /// <summary>
        /// The Maintain a list of objects for each type flag collects and maintains a list of active objects by object type, for example, event, mutex, and semaphore.
        /// </summary>
        FLG_MAINTAIN_OBJECT_TYPELIST = 0x00004000,

        /// <summary>
        /// The Enable heap tagging by DLL flag assigns a unique tag to heap allocations created by the same DLL.
        /// </summary>
        FLG_HEAP_ENABLE_TAG_BY_DLL = 0x00008000,

        /// <summary>
        /// The Disable stack extension flag prevents the kernel from extending the stacks of the threads in the process beyond the initial committed memory.
        /// </summary>
        FLG_DISABLE_STACK_EXTENSION = 0x00010000,

        /// <summary>
        /// The Enable debugging of Win32 subsystem flag debugs the Client Server Run-time Subsystem (csrss.exe) in the NTSD debugger.
        /// </summary>
        FLG_ENABLE_CSRDEBUG = 0x00020000,

        /// <summary>
        /// The Enable loading of kernel debugger symbols flag loads kernel symbols into the kernel memory space the next time Windows starts.
        /// </summary>
        FLG_ENABLE_KDEBUG_SYMBOL_LOAD = 0x00040000,

        /// <summary>
        /// The Disable paging of kernel stacks flag prevents paging of the kernel-mode stacks of inactive threads.
        /// </summary>
        FLG_DISABLE_PAGE_KERNEL_STACKS = 0x00080000,

        /// <summary>
        /// The Enable system critical breaks flag forces a system break into the debugger.
        /// </summary>
        FLG_ENABLE_SYSTEM_CRIT_BREAKS = 0x00100000,

        /// <summary>
        /// The Disable heap coalesce on free flag leaves adjacent blocks of heap memory separate when they are freed.
        /// </summary>
        FLG_HEAP_DISABLE_COALESCING = 0x00200000,

        /// <summary>
        /// The Enable close exception flag raises a user-mode exception whenever an invalid handle is passed to the CloseHandle interface or related interfaces, such as SetEvent, that take handles as arguments.
        /// </summary>
        FLG_ENABLE_CLOSE_EXCEPTIONS = 0x00400000,

        /// <summary>
        /// The Enable exception logging flag creates a log of exception records in the kernel run-time library. You can access the log from a kernel debugger.
        /// </summary>
        FLG_ENABLE_EXCEPTION_LOGGING = 0x00800000,

        /// <summary>
        /// The Enable object handle type tagging flag appears in GFlags, but it has no effect on Windows.
        /// </summary>
        FLG_ENABLE_HANDLE_TYPE_TAGGING = 0x01000000,

        /// <summary>
        /// The Enable page heap flag turns on page heap verification, which monitors dynamic heap memory operations, including allocate and free operations, and causes a debugger break when the verifier detects a heap error.
        /// </summary>
        FLG_HEAP_PAGE_ALLOCS = 0x02000000,

        /// <summary>
        /// The Debug WinLogon flag debugs the WinLogon service.
        /// </summary>
        FLG_DEBUG_INITIAL_COMMAND_EX = 0x04000000,

        /// <summary>
        /// The Buffer DbgPrint Output flag suppresses debugger output from DbgPrint, DbgPrintEx, KdPrint, and KdPrintEx calls.
        /// </summary>
        FLG_DISABLE_DBGPRINT = 0x08000000,

        /// <summary>
        /// The Early critical section event creation flag creates event handles when a critical section is initialized, rather than waiting until the event is needed.
        /// </summary>
        FLG_CRITSEC_EVENT_CREATION = 0x10000000,

        /// <summary>
        /// The Stop on unhandled user-mode exception flag causes a break into the kernel debugger whenever an unhandled user-mode exception occurs.
        /// </summary>
        FLG_STOP_ON_UNHANDLED_EXCEPTION = 0x20000000,

        /// <summary>
        /// The Enable bad handles detection flag raises a user-mode exception (STATUS_INVALID_HANDLE) whenever a user-mode process passes an invalid handle to the Object Manager.
        /// </summary>
        FLG_ENABLE_HANDLE_EXCEPTIONS = 0x40000000,

        /// <summary>
        /// The Disable protected DLL verification flag appears in GFlags, but it has no effect on Windows.
        /// </summary>
        FLG_DISABLE_PROTDLLS = 0x80000000,
    }
}
