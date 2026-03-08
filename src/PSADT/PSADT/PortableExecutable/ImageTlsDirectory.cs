using System.Runtime.InteropServices;
using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents the TLS directory of a PE file, with all pointer-sized fields at 64-bit width.
    /// </summary>
    public sealed record ImageTlsDirectory
    {
        /// <summary>
        /// Initializes a new instance of the ImageTlsDirectory class using the specified 32-bit thread-local storage
        /// (TLS) directory information.
        /// </summary>
        /// <remarks>This constructor is intended for use with 32-bit portable executable (PE) files. It
        /// sets the properties of the ImageTlsDirectory based on the values in the provided IMAGE_TLS_DIRECTORY32
        /// structure. For 64-bit TLS directories, use the corresponding constructor that accepts an
        /// IMAGE_TLS_DIRECTORY64 structure.</remarks>
        /// <param name="directory">The IMAGE_TLS_DIRECTORY32 structure that provides the TLS directory data used to initialize this instance.</param>
        internal ImageTlsDirectory(in IMAGE_TLS_DIRECTORY32 directory)
        {
            StartAddressOfRawData = directory.StartAddressOfRawData;
            EndAddressOfRawData = directory.EndAddressOfRawData;
            AddressOfIndex = directory.AddressOfIndex;
            AddressOfCallBacks = directory.AddressOfCallBacks;
            SizeOfZeroFill = directory.SizeOfZeroFill;
            Anonymous = new(directory.Anonymous.Characteristics, new(directory.Anonymous.Anonymous._bitfield));
            Is64Bit = false;
        }

        /// <summary>
        /// Initializes a new instance of the ImageTlsDirectory class using the specified 64-bit TLS directory
        /// structure.
        /// </summary>
        /// <remarks>This constructor sets the properties of the ImageTlsDirectory based on the values
        /// from the provided 64-bit TLS directory structure. The resulting instance represents a 64-bit TLS
        /// directory.</remarks>
        /// <param name="directory">The IMAGE_TLS_DIRECTORY64 structure that provides the Thread Local Storage (TLS) directory information for
        /// initialization.</param>
        internal ImageTlsDirectory(in IMAGE_TLS_DIRECTORY64 directory)
        {
            StartAddressOfRawData = directory.StartAddressOfRawData;
            EndAddressOfRawData = directory.EndAddressOfRawData;
            AddressOfIndex = directory.AddressOfIndex;
            AddressOfCallBacks = directory.AddressOfCallBacks;
            SizeOfZeroFill = directory.SizeOfZeroFill;
            Anonymous = new(directory.Anonymous.Characteristics, new(directory.Anonymous.Anonymous._bitfield));
            Is64Bit = true;
        }

        /// <summary>
        /// Gets the starting address of the TLS template.
        /// </summary>
        public ulong StartAddressOfRawData { get; }

        /// <summary>
        /// Gets the address of the last byte of the TLS template (excluding zero fill).
        /// </summary>
        public ulong EndAddressOfRawData { get; }

        /// <summary>
        /// Gets the address of the TLS index.
        /// </summary>
        public ulong AddressOfIndex { get; }

        /// <summary>
        /// Gets the address of the TLS callback array.
        /// </summary>
        public ulong AddressOfCallBacks { get; }

        /// <summary>
        /// Gets the size in bytes of the zero fill.
        /// </summary>
        public uint SizeOfZeroFill { get; }

        /// <summary>
        /// Gets the anonymous union containing characteristics and alignment information.
        /// </summary>
        public ImageTlsDirectory0 Anonymous { get; }

        /// <summary>
        /// Gets whether this is from a 64-bit PE file.
        /// </summary>
        public bool Is64Bit { get; }

        /// <summary>
        /// Represents the anonymous union in IMAGE_TLS_DIRECTORY containing either raw Characteristics or the parsed bitfield structure.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "This is meant to reflect an anonymous union.")]
        [StructLayout(LayoutKind.Explicit)]
        public readonly record struct ImageTlsDirectory0
        {
            /// <summary>
            /// Initializes a new instance of the ImageTlsDirectory0 class with the specified TLS directory
            /// characteristics and additional directory information.
            /// </summary>
            /// <param name="characteristics">The characteristics of the TLS directory, represented as a 32-bit unsigned integer that defines specific
            /// attributes.</param>
            /// <param name="anonymous">An instance of ImageTlsDirectory00 containing supplementary information related to the TLS directory.</param>
            internal ImageTlsDirectory0(uint characteristics, ImageTlsDirectory00 anonymous)
            {
                Characteristics = characteristics;
                Anonymous = anonymous;
            }

            /// <summary>
            /// Gets the raw characteristics value containing reserved flags and alignment.
            /// </summary>
            [FieldOffset(0)]
            public readonly uint Characteristics;

            /// <summary>
            /// Gets the parsed bitfield structure containing Reserved0, Alignment, and Reserved1.
            /// </summary>
            [FieldOffset(0)]
            public readonly ImageTlsDirectory00 Anonymous;

            /// <summary>
            /// Represents the nested anonymous struct within the TLS directory union, containing bitfield values.
            /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "This is meant to reflect an anonymous struct within a union.")]
            public readonly record struct ImageTlsDirectory00
            {
                /// <summary>
                /// Initializes a new instance of the ImageTlsDirectory00 struct.
                /// </summary>
                /// <param name="bitfield">The raw bitfield value.</param>
                internal ImageTlsDirectory00(uint bitfield)
                {
                    Bitfield = bitfield;
                }

                /// <summary>
                /// Gets the reserved bits 0-19 (should be 0).
                /// </summary>
                public uint Reserved0 => Bitfield & 0x000FFFFF;

                /// <summary>
                /// Gets the alignment value from bits 20-23.
                /// </summary>
                /// <remarks>
                /// The alignment is stored as a power of 2 (0-13).
                /// The actual alignment in bytes is 2^Alignment.
                /// </remarks>
                public byte Alignment => (byte)((Bitfield >> 20) & 0x0F);

                /// <summary>
                /// Gets the reserved bits 24-31.
                /// </summary>
                public byte Reserved1 => (byte)((Bitfield >> 24) & 0xFF);

                /// <summary>
                /// Gets the raw bitfield value.
                /// </summary>
                private readonly uint Bitfield;
            }
        }
    }
}
