using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents the file header of a portable executable (PE) image, providing access to essential metadata such as
    /// machine type, section count, and other header information required to interpret the structure of the image file.
    /// </summary>
    /// <remarks>The file header encapsulated by this type contains critical information used to identify and
    /// parse a PE image. It exposes properties corresponding to the fields of the underlying IMAGE_FILE_HEADER
    /// structure, enabling consumers to inspect details such as the target architecture, section table size, creation
    /// timestamp, and image characteristics. This type is typically used when analyzing or processing PE files in
    /// low-level tooling or diagnostics scenarios.</remarks>
    public sealed record ImageFileHeader
    {
        /// <summary>
        /// Initializes a new instance of the ImageFileHeader class using the specified IMAGE_FILE_HEADER structure.
        /// </summary>
        /// <param name="fileHeader">The IMAGE_FILE_HEADER structure that provides the file header information to initialize the instance.</param>
        internal ImageFileHeader(in Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_HEADER fileHeader)
        {
            FileHeader = fileHeader;
        }

        /// <summary>
        /// The architecture type of the computer. An image file can only be run on the specified computer or a system.
        /// </summary>
        public IMAGE_FILE_MACHINE Machine => (IMAGE_FILE_MACHINE)FileHeader.Machine;

        /// <summary>
        /// <para>The number of sections. This indicates the size of the section table, which immediately follows the headers. Note that the Windows loader limits the number of sections to 96.</para>
        /// <para><see href="https://learn.microsoft.com/windows/win32/api/winnt/ns-winnt-image_file_header#members">Read more on learn.microsoft.com</see>.</para>
        /// </summary>
        public ushort NumberOfSections => FileHeader.NumberOfSections;

        /// <summary>
        /// <para>The low 32 bits of the time stamp of the image. This represents the date and time the image was created by the linker. The value is represented in the number of seconds elapsed since midnight (00:00:00), January 1, 1970, Universal Coordinated Time, according to the system clock.</para>
        /// <para><see href="https://learn.microsoft.com/windows/win32/api/winnt/ns-winnt-image_file_header#members">Read more on learn.microsoft.com</see>.</para>
        /// </summary>
        public uint TimeDateStamp => FileHeader.TimeDateStamp;

        /// <summary>
        /// The offset of the symbol table, in bytes, or zero if no COFF symbol table exists.
        /// </summary>
        public uint PointerToSymbolTable => FileHeader.PointerToSymbolTable;

        /// <summary>
        /// The number of symbols in the symbol table.
        /// </summary>
        public uint NumberOfSymbols => FileHeader.NumberOfSymbols;

        /// <summary>
        /// The size of the optional header, in bytes. This value should be 0 for object files.
        /// </summary>
        public ushort SizeOfOptionalHeader => FileHeader.SizeOfOptionalHeader;

        /// <summary>
        /// The characteristics of the image. This member can be one or more of the following values.
        /// </summary>
        public IMAGE_FILE_CHARACTERISTICS Characteristics => (IMAGE_FILE_CHARACTERISTICS)FileHeader.Characteristics;

        /// <summary>
        /// Represents the file header information for the image file.
        /// </summary>
        /// <remarks>The file header contains essential metadata about the image file, such as the machine
        /// type and the number of sections. This information is used to interpret the structure and format of the image
        /// file.</remarks>
        private readonly Windows.Win32.System.Diagnostics.Debug.IMAGE_FILE_HEADER FileHeader;
    }
}
