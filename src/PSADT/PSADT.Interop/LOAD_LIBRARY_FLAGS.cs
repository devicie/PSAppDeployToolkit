using System;

namespace PSADT.Interop
{
    /// <summary>
    /// Defines flags that control the behavior of loading a dynamic-link library (DLL) in a process.
    /// </summary>
    /// <remarks>These flags can be used to modify how the system loads a DLL, including whether to resolve
    /// dependencies, check digital signatures, or alter the search path for the DLL's dependencies. Each flag serves a
    /// specific purpose and can affect the loading process significantly.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1008:Enums should have zero value", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1028:Enum Storage should be Int32", Justification = "These are as they're named in the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These are as they're named in the Win32 API.")]
    [Flags]
    public enum LOAD_LIBRARY_FLAGS : ushort
    {
        /// <summary>
        /// If this value is used, and the executable module is a DLL, the system does not call DllMain for process and thread initialization and termination. Also, the system does not load additional executable modules that are referenced by the specified module.
        /// </summary>
        DONT_RESOLVE_DLL_REFERENCES = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.DONT_RESOLVE_DLL_REFERENCES,

        /// <summary>
        /// If this value is used, the system maps the file into the calling process's virtual address space as if it were a data file. Nothing is done to execute or prepare to execute the mapped file. Therefore, you cannot call functions like GetModuleFileName, GetModuleHandle or GetProcAddress with this DLL. Using this value causes writes to read-only memory to raise an access violation. Use this flag when you want to load a DLL only to extract messages or resources from it.
        /// </summary>
        LOAD_LIBRARY_AS_DATAFILE = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_AS_DATAFILE,

        /// <summary>
        /// If this value is used and lpFileName specifies an absolute path, the system uses the alternate file search strategy discussed in the Remarks section to find associated executable modules that the specified module causes to be loaded. If this value is used and lpFileName specifies a relative path, the behavior is undefined.
        /// </summary>
        LOAD_WITH_ALTERED_SEARCH_PATH = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_WITH_ALTERED_SEARCH_PATH,

        /// <summary>
        /// If this value is used, the system does not check AppLocker rules or apply Software Restriction Policies for the DLL. This action applies only to the DLL being loaded and not to its dependencies. This value is recommended for use in setup programs that must run extracted DLLs during installation.
        /// </summary>
        LOAD_IGNORE_CODE_AUTHZ_LEVEL = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_IGNORE_CODE_AUTHZ_LEVEL,

        /// <summary>
        /// If this value is used, the system maps the file into the process's virtual address space as an image file. However, the loader does not load the static imports or perform the other usual initialization steps. Use this flag when you want to load a DLL only to extract messages or resources from it.
        /// </summary>
        LOAD_LIBRARY_AS_IMAGE_RESOURCE = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_AS_IMAGE_RESOURCE,

        /// <summary>
        /// Similar to LOAD_LIBRARY_AS_DATAFILE, except that the DLL file is opened with exclusive write access for the calling process. Other processes cannot open the DLL file for write access while it is in use. However, the DLL can still be opened by other processes.
        /// </summary>
        LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE,

        /// <summary>
        /// Specifies that the digital signature of the binary image must be checked at load time.
        /// </summary>
        LOAD_LIBRARY_REQUIRE_SIGNED_TARGET = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_REQUIRE_SIGNED_TARGET,

        /// <summary>
        /// If this value is used, the directory that contains the DLL is temporarily added to the beginning of the list of directories that are searched for the DLL's dependencies. Directories in the standard search path are not searched.
        /// </summary>
        LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR,

        /// <summary>
        /// If this value is used, the application's installation directory is searched for the DLL and its dependencies. Directories in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        /// </summary>
        LOAD_LIBRARY_SEARCH_APPLICATION_DIR = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_SEARCH_APPLICATION_DIR,

        /// <summary>
        /// If this value is used, directories added using the AddDllDirectory or the SetDllDirectory function are searched for the DLL and its dependencies. If more than one directory has been added, the order in which the directories are searched is unspecified. Directories in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        /// </summary>
        LOAD_LIBRARY_SEARCH_USER_DIRS = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_SEARCH_USER_DIRS,

        /// <summary>
        /// If this value is used, %windows%\system32 is searched for the DLL and its dependencies. Directories in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        /// </summary>
        LOAD_LIBRARY_SEARCH_SYSTEM32 = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_SEARCH_SYSTEM32,

        /// <summary>
        /// This value is a combination of LOAD_LIBRARY_SEARCH_APPLICATION_DIR, LOAD_LIBRARY_SEARCH_SYSTEM32, and LOAD_LIBRARY_SEARCH_USER_DIRS. Directories in the standard search path are not searched. This value cannot be combined with LOAD_WITH_ALTERED_SEARCH_PATH.
        /// </summary>
        LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_SEARCH_DEFAULT_DIRS,

        /// <summary>
        /// If this value is used, loading a DLL for execution from the current directory is only allowed if it is under a directory in the Safe load list.
        /// </summary>
        LOAD_LIBRARY_SAFE_CURRENT_DIRS = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_SAFE_CURRENT_DIRS,

        /// <summary>
        /// Indicate that the system searches System32 without resolving forwarded exports.
        /// </summary>
        LOAD_LIBRARY_SEARCH_SYSTEM32_NO_FORWARDER = (ushort)Windows.Win32.System.LibraryLoader.LOAD_LIBRARY_FLAGS.LOAD_LIBRARY_SEARCH_SYSTEM32_NO_FORWARDER,
    }
}
