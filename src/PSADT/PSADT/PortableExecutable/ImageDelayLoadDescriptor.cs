using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Win32.System.WindowsProgramming;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a delay-loaded DLL with its functions.
    /// </summary>
    public sealed record ImageDelayLoadDescriptor
    {
        /// <summary>
        /// Initializes a new instance of the ImageDelayLoadDescriptor class.
        /// </summary>
        /// <param name="descriptor">The delay load descriptor structure.</param>
        /// <param name="dllName">The name of the DLL.</param>
        /// <param name="importAddressTable">The Import Address Table entries (thunks), or null if not present.</param>
        /// <param name="importNameTable">The Import Name Table entries (names/ordinals), or null if not present.</param>
        /// <param name="boundImportAddressTable">The Bound Import Address Table entries (bound thunks), or null if not present.</param>
        /// <param name="unloadInformationTable">The Unload Information Table entries (original thunks for unloading), or null if not present.</param>
        internal ImageDelayLoadDescriptor(in IMAGE_DELAYLOAD_DESCRIPTOR descriptor, string dllName, ReadOnlyCollection<ImageThunkData>? importAddressTable, ReadOnlyCollection<ImageThunkData>? importNameTable, ReadOnlyCollection<ImageThunkData>? boundImportAddressTable, ReadOnlyCollection<ImageThunkData>? unloadInformationTable)
        {
            Descriptor = descriptor;
            DllName = dllName;
            ImportAddressTable = importAddressTable;
            ImportNameTable = importNameTable;
            BoundImportAddressTable = boundImportAddressTable;
            UnloadInformationTable = unloadInformationTable;
        }

        /// <summary>
        /// Gets the name of the delay-imported DLL.
        /// </summary>
        public string DllName { get; }

        /// <summary>
        /// Gets the RVA to the location where the module handle (HMODULE) is stored.
        /// </summary>
        /// <remarks>
        /// This points to a location that is NULL before the DLL is loaded.
        /// After delay-loading, the loader stores the HMODULE at this location.
        /// </remarks>
        public uint ModuleHandleRva => Descriptor.ModuleHandleRVA;

        /// <summary>
        /// Gets the Import Address Table entries, or null if not present.
        /// </summary>
        /// <remarks>
        /// Before delay-loading, this mirrors the Import Name Table.
        /// After delay-loading, contains the resolved function addresses.
        /// </remarks>
        public IReadOnlyList<ImageThunkData>? ImportAddressTable { get; }

        /// <summary>
        /// Gets the Import Name Table entries containing function names and ordinals, or null if not present.
        /// </summary>
        /// <remarks>
        /// This is analogous to the Import Lookup Table (ILT) for regular imports.
        /// Contains <see cref="ImageImportByName"/> for named imports and <see cref="ImageThunkData"/> for ordinal imports.
        /// </remarks>
        public IReadOnlyList<ImageThunkData>? ImportNameTable { get; }

        /// <summary>
        /// Gets the Bound Import Address Table entries, or null if not present.
        /// </summary>
        /// <remarks>
        /// Contains prebound addresses if the DLL was bound at build time.
        /// </remarks>
        public IReadOnlyList<ImageThunkData>? BoundImportAddressTable { get; }

        /// <summary>
        /// Gets the Unload Information Table entries, or null if not present.
        /// </summary>
        /// <remarks>
        /// Contains the original thunk values needed to restore the IAT if the DLL is unloaded.
        /// </remarks>
        public IReadOnlyList<ImageThunkData>? UnloadInformationTable { get; }

        /// <summary>
        /// Gets whether the addresses are RVAs (true) or VAs (false).
        /// </summary>
        /// <remarks>
        /// Modern delay-load descriptors use RVAs. Legacy descriptors use VAs.
        /// </remarks>
        public bool UsesRva => (Descriptor.Attributes.AllAttributes & 1) != 0;

        /// <summary>
        /// Gets the timestamp of the bound DLL, or null if not bound (timestamp is zero).
        /// </summary>
        public DateTime? TimeDateStamp => Descriptor.TimeDateStamp > 0
            ? DateTimeOffset.FromUnixTimeSeconds(Descriptor.TimeDateStamp).UtcDateTime
            : null;

        /// <summary>
        /// Gets the raw IMAGE_DELAYLOAD_DESCRIPTOR structure.
        /// </summary>
        private readonly IMAGE_DELAYLOAD_DESCRIPTOR Descriptor;
    }
}
