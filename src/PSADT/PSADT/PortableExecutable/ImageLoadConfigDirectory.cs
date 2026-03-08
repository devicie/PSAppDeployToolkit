using System;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents the load configuration directory of a PE file, with all pointer-sized fields at 64-bit width.
    /// </summary>
    public sealed record ImageLoadConfigDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageLoadConfigDirectory class using the specified
        /// IMAGE_LOAD_CONFIG_DIRECTORY32 structure.
        /// </summary>
        /// <remarks>This constructor maps the fields from the IMAGE_LOAD_CONFIG_DIRECTORY32 structure to
        /// the properties of the ImageLoadConfigDirectory class. The TimeDateStamp is converted from Unix time to UTC
        /// DateTime if it is greater than zero.</remarks>
        /// <param name="directory">The IMAGE_LOAD_CONFIG_DIRECTORY32 structure containing the configuration data to initialize the instance.</param>
        internal ImageLoadConfigDirectory(in Windows.Win32.System.Diagnostics.Debug.IMAGE_LOAD_CONFIG_DIRECTORY32 directory)
        {
            Size = directory.Size;
            TimeDateStamp = directory.TimeDateStamp > 0 ? DateTimeOffset.FromUnixTimeSeconds(directory.TimeDateStamp).UtcDateTime : null;
            Version = new(directory.MajorVersion, directory.MinorVersion);
            GlobalFlagsClear = (NT_GLOBAL_FLAGS)directory.GlobalFlagsClear;
            GlobalFlagsSet = (NT_GLOBAL_FLAGS)directory.GlobalFlagsSet;
            CriticalSectionDefaultTimeout = directory.CriticalSectionDefaultTimeout;
            DeCommitFreeBlockThreshold = directory.DeCommitFreeBlockThreshold;
            DeCommitTotalFreeThreshold = directory.DeCommitTotalFreeThreshold;
            LockPrefixTable = directory.LockPrefixTable;
            MaximumAllocationSize = directory.MaximumAllocationSize;
            VirtualMemoryThreshold = directory.VirtualMemoryThreshold;
            ProcessAffinityMask = directory.ProcessAffinityMask;
            ProcessHeapFlags = (HEAP_FLAGS)directory.ProcessHeapFlags;
            CSDVersion = directory.CSDVersion;
            DependentLoadFlags = (LOAD_LIBRARY_FLAGS)directory.DependentLoadFlags;
            EditList = directory.EditList;
            SecurityCookie = directory.SecurityCookie;
            SEHandlerTable = directory.SEHandlerTable;
            SEHandlerCount = directory.SEHandlerCount;
            GuardCFCheckFunctionPointer = directory.GuardCFCheckFunctionPointer;
            GuardCFDispatchFunctionPointer = directory.GuardCFDispatchFunctionPointer;
            GuardCFFunctionTable = directory.GuardCFFunctionTable;
            GuardCFFunctionCount = directory.GuardCFFunctionCount;
            GuardFlags = (IMAGE_GUARD)directory.GuardFlags;
            CodeIntegrity = new(directory.CodeIntegrity);
            GuardAddressTakenIatEntryTable = directory.GuardAddressTakenIatEntryTable;
            GuardAddressTakenIatEntryCount = directory.GuardAddressTakenIatEntryCount;
            GuardLongJumpTargetTable = directory.GuardLongJumpTargetTable;
            GuardLongJumpTargetCount = directory.GuardLongJumpTargetCount;
            DynamicValueRelocTable = directory.DynamicValueRelocTable;
            CHPEMetadataPointer = directory.CHPEMetadataPointer;
            GuardRFFailureRoutine = directory.GuardRFFailureRoutine;
            GuardRFFailureRoutineFunctionPointer = directory.GuardRFFailureRoutineFunctionPointer;
            DynamicValueRelocTableOffset = directory.DynamicValueRelocTableOffset;
            DynamicValueRelocTableSection = directory.DynamicValueRelocTableSection;
            GuardRFVerifyStackPointerFunctionPointer = directory.GuardRFVerifyStackPointerFunctionPointer;
            HotPatchTableOffset = directory.HotPatchTableOffset;
            EnclaveConfigurationPointer = directory.EnclaveConfigurationPointer;
            VolatileMetadataPointer = directory.VolatileMetadataPointer;
            GuardEHContinuationTable = directory.GuardEHContinuationTable;
            GuardEHContinuationCount = directory.GuardEHContinuationCount;
            GuardXFGCheckFunctionPointer = directory.GuardXFGCheckFunctionPointer;
            GuardXFGDispatchFunctionPointer = directory.GuardXFGDispatchFunctionPointer;
            GuardXFGTableDispatchFunctionPointer = directory.GuardXFGTableDispatchFunctionPointer;
            CastGuardOsDeterminedFailureMode = directory.CastGuardOsDeterminedFailureMode;
            GuardMemcpyFunctionPointer = directory.GuardMemcpyFunctionPointer;
            Is64Bit = false;
        }

        /// <summary>
        /// Initializes a new instance of the ImageLoadConfigDirectory class using the specified
        /// IMAGE_LOAD_CONFIG_DIRECTORY64 structure.
        /// </summary>
        /// <remarks>This constructor sets various properties based on the provided directory structure,
        /// including memory allocation thresholds and flags relevant to the image loading process.</remarks>
        /// <param name="directory">The IMAGE_LOAD_CONFIG_DIRECTORY64 structure containing configuration data for the image load process.</param>
        internal ImageLoadConfigDirectory(in Windows.Win32.System.Diagnostics.Debug.IMAGE_LOAD_CONFIG_DIRECTORY64 directory)
        {
            Size = directory.Size;
            TimeDateStamp = directory.TimeDateStamp > 0 ? DateTimeOffset.FromUnixTimeSeconds(directory.TimeDateStamp).UtcDateTime : null;
            Version = new(directory.MajorVersion, directory.MinorVersion);
            GlobalFlagsClear = (NT_GLOBAL_FLAGS)directory.GlobalFlagsClear;
            GlobalFlagsSet = (NT_GLOBAL_FLAGS)directory.GlobalFlagsSet;
            CriticalSectionDefaultTimeout = directory.CriticalSectionDefaultTimeout;
            DeCommitFreeBlockThreshold = directory.DeCommitFreeBlockThreshold;
            DeCommitTotalFreeThreshold = directory.DeCommitTotalFreeThreshold;
            LockPrefixTable = directory.LockPrefixTable;
            MaximumAllocationSize = directory.MaximumAllocationSize;
            VirtualMemoryThreshold = directory.VirtualMemoryThreshold;
            ProcessAffinityMask = directory.ProcessAffinityMask;
            ProcessHeapFlags = (HEAP_FLAGS)directory.ProcessHeapFlags;
            CSDVersion = directory.CSDVersion;
            DependentLoadFlags = (LOAD_LIBRARY_FLAGS)directory.DependentLoadFlags;
            EditList = directory.EditList;
            SecurityCookie = directory.SecurityCookie;
            SEHandlerTable = directory.SEHandlerTable;
            SEHandlerCount = directory.SEHandlerCount;
            GuardCFCheckFunctionPointer = directory.GuardCFCheckFunctionPointer;
            GuardCFDispatchFunctionPointer = directory.GuardCFDispatchFunctionPointer;
            GuardCFFunctionTable = directory.GuardCFFunctionTable;
            GuardCFFunctionCount = directory.GuardCFFunctionCount;
            GuardFlags = (IMAGE_GUARD)directory.GuardFlags;
            CodeIntegrity = new(directory.CodeIntegrity);
            GuardAddressTakenIatEntryTable = directory.GuardAddressTakenIatEntryTable;
            GuardAddressTakenIatEntryCount = directory.GuardAddressTakenIatEntryCount;
            GuardLongJumpTargetTable = directory.GuardLongJumpTargetTable;
            GuardLongJumpTargetCount = directory.GuardLongJumpTargetCount;
            DynamicValueRelocTable = directory.DynamicValueRelocTable;
            CHPEMetadataPointer = directory.CHPEMetadataPointer;
            GuardRFFailureRoutine = directory.GuardRFFailureRoutine;
            GuardRFFailureRoutineFunctionPointer = directory.GuardRFFailureRoutineFunctionPointer;
            DynamicValueRelocTableOffset = directory.DynamicValueRelocTableOffset;
            DynamicValueRelocTableSection = directory.DynamicValueRelocTableSection;
            GuardRFVerifyStackPointerFunctionPointer = directory.GuardRFVerifyStackPointerFunctionPointer;
            HotPatchTableOffset = directory.HotPatchTableOffset;
            EnclaveConfigurationPointer = directory.EnclaveConfigurationPointer;
            VolatileMetadataPointer = directory.VolatileMetadataPointer;
            GuardEHContinuationTable = directory.GuardEHContinuationTable;
            GuardEHContinuationCount = directory.GuardEHContinuationCount;
            GuardXFGCheckFunctionPointer = directory.GuardXFGCheckFunctionPointer;
            GuardXFGDispatchFunctionPointer = directory.GuardXFGDispatchFunctionPointer;
            GuardXFGTableDispatchFunctionPointer = directory.GuardXFGTableDispatchFunctionPointer;
            CastGuardOsDeterminedFailureMode = directory.CastGuardOsDeterminedFailureMode;
            GuardMemcpyFunctionPointer = directory.GuardMemcpyFunctionPointer;
            Is64Bit = true;
        }

        /// <summary>
        /// Gets the size of the structure.
        /// </summary>
        public uint Size { get; }

        /// <summary>
        /// Gets the date/time stamp of the file, or null if the timestamp is zero.
        /// </summary>
        public DateTime? TimeDateStamp { get; }

        /// <summary>
        /// Gets the version of the load configuration directory.
        /// </summary>
        public Version Version { get; }

        /// <summary>
        /// Gets the global flags to clear.
        /// </summary>
        public NT_GLOBAL_FLAGS GlobalFlagsClear { get; }

        /// <summary>
        /// Gets the global flags to set.
        /// </summary>
        public NT_GLOBAL_FLAGS GlobalFlagsSet { get; }

        /// <summary>
        /// Gets the critical section default timeout.
        /// </summary>
        public uint CriticalSectionDefaultTimeout { get; }

        /// <summary>
        /// Gets the size of the minimum allocation to de-commit.
        /// </summary>
        public ulong DeCommitFreeBlockThreshold { get; }

        /// <summary>
        /// Gets the size of the minimum total memory to de-commit.
        /// </summary>
        public ulong DeCommitTotalFreeThreshold { get; }

        /// <summary>
        /// Gets the VA of the lock prefix table.
        /// </summary>
        public ulong LockPrefixTable { get; }

        /// <summary>
        /// Gets the maximum allocation size.
        /// </summary>
        public ulong MaximumAllocationSize { get; }

        /// <summary>
        /// Gets the maximum virtual memory size.
        /// </summary>
        public ulong VirtualMemoryThreshold { get; }

        /// <summary>
        /// Gets the process affinity mask.
        /// </summary>
        public ulong ProcessAffinityMask { get; }

        /// <summary>
        /// Gets the process heap flags.
        /// </summary>
        public HEAP_FLAGS ProcessHeapFlags { get; }

        /// <summary>
        /// Gets the service pack version.
        /// </summary>
        public ushort CSDVersion { get; }

        /// <summary>
        /// Gets the dependent load flags.
        /// </summary>
        public LOAD_LIBRARY_FLAGS DependentLoadFlags { get; }

        /// <summary>
        /// Gets the VA of the edit list.
        /// </summary>
        public ulong EditList { get; }

        /// <summary>
        /// Gets the VA of the security cookie.
        /// </summary>
        public ulong SecurityCookie { get; }

        /// <summary>
        /// Gets the VA of the SEH handler table.
        /// </summary>
        public ulong SEHandlerTable { get; }

        /// <summary>
        /// Gets the count of SEH handlers.
        /// </summary>
        public ulong SEHandlerCount { get; }

        /// <summary>
        /// Gets the VA of the CFG check function pointer.
        /// </summary>
        public ulong GuardCFCheckFunctionPointer { get; }

        /// <summary>
        /// Gets the VA of the CFG dispatch function pointer.
        /// </summary>
        public ulong GuardCFDispatchFunctionPointer { get; }

        /// <summary>
        /// Gets the VA of the CFG function table.
        /// </summary>
        public ulong GuardCFFunctionTable { get; }

        /// <summary>
        /// Gets the count of CFG functions.
        /// </summary>
        public ulong GuardCFFunctionCount { get; }

        /// <summary>
        /// Gets the CFG flags.
        /// </summary>
        public IMAGE_GUARD GuardFlags { get; }

        /// <summary>
        /// Gets the code integrity configuration.
        /// </summary>
        public ImageLoadConfigCodeIntegrity CodeIntegrity { get; }

        /// <summary>
        /// Gets the VA of the guard address taken IAT entry table.
        /// </summary>
        public ulong GuardAddressTakenIatEntryTable { get; }

        /// <summary>
        /// Gets the count of guard address taken IAT entries.
        /// </summary>
        public ulong GuardAddressTakenIatEntryCount { get; }

        /// <summary>
        /// Gets the VA of the guard long jump target table.
        /// </summary>
        public ulong GuardLongJumpTargetTable { get; }

        /// <summary>
        /// Gets the count of guard long jump targets.
        /// </summary>
        public ulong GuardLongJumpTargetCount { get; }

        /// <summary>
        /// Gets the VA of the dynamic value relocation table.
        /// </summary>
        public ulong DynamicValueRelocTable { get; }

        /// <summary>
        /// Gets the VA of the CHPE metadata pointer.
        /// </summary>
        public ulong CHPEMetadataPointer { get; }

        /// <summary>
        /// Gets the VA of the guard RF failure routine.
        /// </summary>
        public ulong GuardRFFailureRoutine { get; }

        /// <summary>
        /// Gets the VA of the guard RF failure routine function pointer.
        /// </summary>
        public ulong GuardRFFailureRoutineFunctionPointer { get; }

        /// <summary>
        /// Gets the offset of the dynamic value relocation table.
        /// </summary>
        public uint DynamicValueRelocTableOffset { get; }

        /// <summary>
        /// Gets the section index of the dynamic value relocation table.
        /// </summary>
        public ushort DynamicValueRelocTableSection { get; }

        /// <summary>
        /// Gets the VA of the guard RF verify stack pointer function pointer.
        /// </summary>
        public ulong GuardRFVerifyStackPointerFunctionPointer { get; }

        /// <summary>
        /// Gets the offset of the hot patch table.
        /// </summary>
        public uint HotPatchTableOffset { get; }

        /// <summary>
        /// Gets the VA of the enclave configuration pointer.
        /// </summary>
        public ulong EnclaveConfigurationPointer { get; }

        /// <summary>
        /// Gets the VA of the volatile metadata pointer.
        /// </summary>
        public ulong VolatileMetadataPointer { get; }

        /// <summary>
        /// Gets the VA of the guard EH continuation table.
        /// </summary>
        public ulong GuardEHContinuationTable { get; }

        /// <summary>
        /// Gets the count of guard EH continuation entries.
        /// </summary>
        public ulong GuardEHContinuationCount { get; }

        /// <summary>
        /// Gets the VA of the guard XFG check function pointer.
        /// </summary>
        public ulong GuardXFGCheckFunctionPointer { get; }

        /// <summary>
        /// Gets the VA of the guard XFG dispatch function pointer.
        /// </summary>
        public ulong GuardXFGDispatchFunctionPointer { get; }

        /// <summary>
        /// Gets the VA of the guard XFG table dispatch function pointer.
        /// </summary>
        public ulong GuardXFGTableDispatchFunctionPointer { get; }

        /// <summary>
        /// Gets the cast guard OS-determined failure mode.
        /// </summary>
        public ulong CastGuardOsDeterminedFailureMode { get; }

        /// <summary>
        /// Gets the VA of the guard memcpy function pointer.
        /// </summary>
        public ulong GuardMemcpyFunctionPointer { get; }

        /// <summary>
        /// Gets whether this is from a 64-bit PE file.
        /// </summary>
        public bool Is64Bit { get; }
    }
}
