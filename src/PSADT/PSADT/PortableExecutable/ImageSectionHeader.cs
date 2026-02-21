using System.Runtime.InteropServices;
using System.Text;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>Represents the image section header format.</summary>
    /// <remarks>
    /// <para><see href="https://learn.microsoft.com/windows/win32/api/winnt/ns-winnt-image_section_header">Learn more about this API from learn.microsoft.com</see>.</para>
    /// </remarks>
    public sealed record ImageSectionHeader
    {
        /// <summary>
        /// Initializes a new instance of the ImageSectionHeader class using the specified IMAGE_SECTION_HEADER
        /// structure.
        /// </summary>
        /// <remarks>This constructor reads the section name from the provided header and initializes the
        /// properties of the ImageSectionHeader accordingly. The name is extracted as a UTF8 string from the first
        /// eight bytes of the header.</remarks>
        /// <param name="header">The IMAGE_SECTION_HEADER structure containing the section header information to initialize the
        /// ImageSectionHeader instance.</param>
        internal ImageSectionHeader(in Windows.Win32.System.Diagnostics.Debug.IMAGE_SECTION_HEADER header)
        {
            // Read out the name as a UTF8 string.
            byte[] nameBytes = new byte[header.Name.Length];
            int length = 0; while (length < 8 && header.Name[length] != 0)
            {
                nameBytes[length] = header.Name[length];
                length++;
            }
            Name = Encoding.UTF8.GetString(nameBytes, 0, length);

            // Set remaining variables.
            Misc = new(header.Misc.PhysicalAddress, header.Misc.VirtualSize);
            VirtualAddress = header.VirtualAddress;
            SizeOfRawData = header.SizeOfRawData;
            PointerToRawData = header.PointerToRawData;
            PointerToRelocations = header.PointerToRelocations;
            PointerToLinenumbers = header.PointerToLinenumbers;
            NumberOfRelocations = header.NumberOfRelocations;
            NumberOfLinenumbers = header.NumberOfLinenumbers;
            Characteristics = (IMAGE_SECTION_CHARACTERISTICS)header.Characteristics;
        }
        /// <summary>
        /// An 8-byte, null-padded UTF-8 string. There is no terminating null character if the string is exactly eight characters long. For longer names, this member contains a forward slash (/) followed by an ASCII representation of a decimal number that is an offset into the string table. Executable images do not use a string table and do not support section names longer than eight characters.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the miscellaneous data associated with the current instance.
        /// </summary>
        /// <remarks>This property provides access to additional information that may be relevant for the
        /// instance, such as configuration settings or metadata. The structure of the data is defined by the
        /// `MiscUnion` type, which can hold various forms of data.</remarks>
        public ImageSectionHeader0 Misc { get; }

        /// <summary>
        /// The address of the first byte of the section when loaded into memory, relative to the image base. For object files, this is the address of the first byte before relocation is applied.
        /// </summary>
        public uint VirtualAddress { get; }

        /// <summary>
        /// <para>The size of the initialized data on disk, in bytes. This value must be a multiple of the <b>FileAlignment</b> member of the <a href="https://docs.microsoft.com/windows/win32/api/winnt/ns-winnt-image_optional_header32">IMAGE_OPTIONAL_HEADER</a> structure. If this value is less than the <b>VirtualSize</b> member, the remainder of the section is filled with zeroes. If the section contains only uninitialized data, the member is zero.</para>
        /// <para><see href="https://learn.microsoft.com/windows/win32/api/winnt/ns-winnt-image_section_header#members">Read more on learn.microsoft.com</see>.</para>
        /// </summary>
        public uint SizeOfRawData { get; }

        /// <summary>
        /// <para>A file pointer to the first page within the COFF file. This value must be a multiple of the <b>FileAlignment</b> member of the <a href="https://docs.microsoft.com/windows/win32/api/winnt/ns-winnt-image_optional_header32">IMAGE_OPTIONAL_HEADER</a> structure. If a section contains only uninitialized data, set this member is zero.</para>
        /// <para><see href="https://learn.microsoft.com/windows/win32/api/winnt/ns-winnt-image_section_header#members">Read more on learn.microsoft.com</see>.</para>
        /// </summary>
        public uint PointerToRawData { get; }

        /// <summary>
        /// A file pointer to the beginning of the relocation entries for the section. If there are no relocations, this value is zero.
        /// </summary>
        public uint PointerToRelocations { get; }

        /// <summary>
        /// A file pointer to the beginning of the line-number entries for the section. If there are no COFF line numbers, this value is zero.
        /// </summary>
        public uint PointerToLinenumbers { get; }

        /// <summary>
        /// The number of relocation entries for the section. This value is zero for executable images.
        /// </summary>
        public ushort NumberOfRelocations { get; }

        /// <summary>
        /// The number of line-number entries for the section.
        /// </summary>
        public ushort NumberOfLinenumbers { get; }

        /// <summary>
        /// <para>The characteristics of the image. The following values are defined.</para>
        /// <para></para>
        /// <para>This doc was truncated.</para>
        /// <para><see href="https://learn.microsoft.com/windows/win32/api/winnt/ns-winnt-image_section_header#members">Read more on learn.microsoft.com</see>.</para>
        /// </summary>
        public IMAGE_SECTION_CHARACTERISTICS Characteristics { get; }

        /// <summary>
        /// Represents a structure that holds both physical and virtual address information.
        /// </summary>
        /// <remarks>This record struct uses explicit layout to define the memory layout of its fields.
        /// The fields are defined at the same offset, allowing for efficient access to either the physical or virtual
        /// address as needed.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "This is meant to reflect an anonymous union.")]
        [StructLayout(LayoutKind.Explicit)]
        public readonly record struct ImageSectionHeader0
        {
            /// <summary>
            /// Initializes a new instance of the MiscUnion class with the specified physical address and virtual size.
            /// </summary>
            /// <param name="physicalAddress">The physical address to associate with the union, represented as a 32-bit unsigned integer.</param>
            /// <param name="virtualSize">The virtual size of the union, represented as a 32-bit unsigned integer.</param>
            internal ImageSectionHeader0(uint physicalAddress, uint virtualSize)
            {
                PhysicalAddress = physicalAddress;
                VirtualSize = virtualSize;
            }

            /// <summary>
            /// Gets the physical address represented as a 32-bit unsigned integer.
            /// </summary>
            [FieldOffset(0)]
            public readonly uint PhysicalAddress;

            /// <summary>
            /// Gets the total size, in bytes, of the section when loaded into memory.
            /// </summary>
            [FieldOffset(0)]
            public readonly uint VirtualSize;
        }
    }
}
