using Windows.Win32;
using Windows.Win32.System.WindowsProgramming;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents import thunk data from a PE file.
    /// </summary>
    /// <remarks>
    /// The thunk data is stored as a 64-bit value regardless of the source PE architecture,
    /// with properties to extract the address, ordinal, and other information.
    /// This is the base class for import entries. For named imports, see <see cref="ImageImportByName"/>.
    /// </remarks>
    public record ImageThunkData
    {
        /// <summary>
        /// Initializes a new instance of the ImageThunkData class from a 32-bit thunk.
        /// </summary>
        /// <param name="thunk">The IMAGE_THUNK_DATA32 structure.</param>
        internal ImageThunkData(in IMAGE_THUNK_DATA32 thunk)
        {
            RawValue = thunk.u1.Ordinal;
            Is64Bit = false;
        }

        /// <summary>
        /// Initializes a new instance of the ImageThunkData class from a 64-bit thunk.
        /// </summary>
        /// <param name="thunk">The IMAGE_THUNK_DATA64 structure.</param>
        internal ImageThunkData(in IMAGE_THUNK_DATA64 thunk)
        {
            RawValue = thunk.u1.Ordinal;
            Is64Bit = true;
        }

        /// <summary>
        /// Initializes a new instance of the ImageThunkData class with raw values.
        /// </summary>
        /// <param name="rawValue">The raw thunk value.</param>
        /// <param name="is64Bit">Whether this is from a 64-bit PE file.</param>
        protected ImageThunkData(ulong rawValue, bool is64Bit)
        {
            RawValue = rawValue;
            Is64Bit = is64Bit;
        }

        /// <summary>
        /// Gets the raw value from the thunk union.
        /// </summary>
        public ulong RawValue { get; }

        /// <summary>
        /// Gets whether this is from a 64-bit PE file.
        /// </summary>
        public bool Is64Bit { get; }

        /// <summary>
        /// Gets whether this thunk imports by ordinal.
        /// </summary>
        public bool IsOrdinal => Is64Bit
            ? (RawValue & PInvoke.IMAGE_ORDINAL_FLAG64) != 0
            : (RawValue & PInvoke.IMAGE_ORDINAL_FLAG32) != 0;

        /// <summary>
        /// Gets the ordinal value if importing by ordinal, otherwise 0.
        /// </summary>
        public ushort Ordinal => IsOrdinal ? (ushort)(RawValue & 0xFFFF) : (ushort)0;

        /// <summary>
        /// Gets the forwarder string RVA, function address, or address of import name table entry.
        /// </summary>
        /// <remarks>
        /// When <see cref="IsOrdinal"/> is false, this contains the RVA to an IMAGE_IMPORT_BY_NAME structure.
        /// </remarks>
        public ulong AddressOfData => RawValue;

        /// <summary>
        /// Gets the RVA to the import name (IMAGE_IMPORT_BY_NAME) if importing by name, otherwise 0.
        /// </summary>
        public uint NameRva => IsOrdinal ? 0 : (uint)(RawValue & 0x7FFFFFFF);
    }
}
