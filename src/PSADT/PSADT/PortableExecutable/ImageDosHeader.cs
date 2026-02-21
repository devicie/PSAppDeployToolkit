using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents the DOS header of a Portable Executable (PE) image, providing access to essential metadata required
    /// for loading and executing the image.
    /// </summary>
    /// <remarks>The DOS header is the first structure in a PE file and contains critical information used by
    /// the operating system to validate and load the executable. This record encapsulates all fields defined in the
    /// IMAGE_DOS_HEADER structure. Ensure that the provided header is valid and conforms to the expected format when
    /// creating an instance of this record.</remarks>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "These are as they're named within the Win32 API.")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "These are as they're named within the Win32 API.")]
    public sealed record ImageDosHeader
    {
        /// <summary>
        /// Initializes a new instance of the ImageDosHeader class using the specified IMAGE_DOS_HEADER structure.
        /// </summary>
        /// <remarks>This constructor extracts the magic number, the bytes on the last page of the file,
        /// and the number of pages from the provided header.</remarks>
        /// <param name="header">The IMAGE_DOS_HEADER structure containing the DOS header information to initialize the ImageDosHeader
        /// instance.</param>
        internal ImageDosHeader(in IMAGE_DOS_HEADER header)
        {
            // Set base values.
            e_magic = header.e_magic;
            e_cblp = header.e_cblp;
            e_cp = header.e_cp;
            e_crlc = header.e_crlc;
            e_cparhdr = header.e_cparhdr;
            e_minalloc = header.e_minalloc;
            e_maxalloc = header.e_maxalloc;
            e_ss = header.e_ss;
            e_sp = header.e_sp;
            e_csum = header.e_csum;
            e_ip = header.e_ip;
            e_cs = header.e_cs;
            e_lfarlc = header.e_lfarlc;
            e_ovno = header.e_ovno;
            e_oemid = header.e_oemid;
            e_oeminfo = header.e_oeminfo;
            e_lfanew = header.e_lfanew;

            // Set array values. CsWin32 is a bit shit here so we have to read it item by item.
            ushort[] eResArray = new ushort[header.e_res.Length];
            ushort[] eRes2Array = new ushort[header.e_res2.Length];
            for (int i = 0; i < eResArray.Length; i++)
            {
                eResArray[i] = header.e_res[i];
            }
            for (int i = 0; i < eRes2Array.Length; i++)
            {
                eRes2Array[i] = header.e_res2[i];
            }
            e_res = new ReadOnlyCollection<ushort>(eResArray);
            e_res2 = new ReadOnlyCollection<ushort>(eRes2Array);
        }

        /// <summary>
        /// Gets the magic number that identifies the file format.
        /// </summary>
        /// <remarks>This property is typically used to verify the integrity of the file format being
        /// processed. The magic number is a specific value that indicates the type of data contained in the
        /// file.</remarks>
        public ushort e_magic { get; }

        /// <summary>
        /// Gets the value representing the current configuration block length in bytes.
        /// </summary>
        public ushort e_cblp { get; }

        /// <summary>
        /// Gets the value representing the current code page used for character encoding.
        /// </summary>
        /// <remarks>This property provides the code page identifier, which can be used to determine the
        /// character encoding for text processing. It is particularly useful when dealing with string conversions or
        /// file I/O operations that require specific encoding formats.</remarks>
        public ushort e_cp { get; }

        /// <summary>
        /// Gets the number of relocation entries for the executable image header.
        /// </summary>
        public ushort e_crlc { get; }

        /// <summary>
        /// Gets the size of the program header in paragraphs.
        /// </summary>
        /// <remarks>This value specifies the length of the header for the executable file, measured in
        /// 16-byte paragraphs. It is used to determine where the program's image begins in memory.</remarks>
        public ushort e_cparhdr { get; }

        /// <summary>
        /// Gets the minimum number of memory paragraphs that must be allocated for the executable image.
        /// </summary>
        /// <remarks>This property corresponds to the e_minalloc field in the DOS header of a portable
        /// executable (PE) file. It indicates the smallest amount of memory, in 16-byte paragraphs, that must be
        /// allocated when loading the executable. This value is primarily relevant for legacy DOS executables and is
        /// typically used for compatibility purposes.</remarks>
        public ushort e_minalloc { get; }

        /// <summary>
        /// Gets the maximum number of paragraphs that can be allocated by the executable at load time.
        /// </summary>
        /// <remarks>This value specifies the upper limit of memory allocation for the program, expressed
        /// in 16-byte paragraphs. It is used by the operating system to determine how much memory can be reserved for
        /// the executable during loading. The actual allocation may be less, depending on system constraints.</remarks>
        public ushort e_maxalloc { get; }

        /// <summary>
        /// Gets the value representing the stack segment (SS) field from the DOS header of a portable executable (PE)
        /// file.
        /// </summary>
        /// <remarks>This value corresponds to the original stack segment specified in the DOS header. It
        /// is primarily relevant for legacy DOS executables and is typically not used by modern Windows PE
        /// loaders.</remarks>
        public ushort e_ss { get; }

        /// <summary>
        /// Gets the initial value of the stack pointer (SP) as specified in the DOS header of the portable executable
        /// (PE) file.
        /// </summary>
        /// <remarks>This value is used by DOS to set up the stack when loading the executable. It is
        /// primarily relevant for legacy DOS compatibility and is typically not used by modern Windows
        /// applications.</remarks>
        public ushort e_sp { get; }

        /// <summary>
        /// Gets the checksum value associated with the entity.
        /// </summary>
        /// <remarks>The checksum is used to verify the integrity of the data. It is calculated based on
        /// the content of the entity and can be used to detect changes or corruption.</remarks>
        public ushort e_csum { get; }

        /// <summary>
        /// Gets the instruction pointer value as specified in the DOS header of the portable executable file.
        /// </summary>
        /// <remarks>This property reflects the value of the instruction pointer (IP) at program startup,
        /// as defined in the DOS executable header. It is primarily used for compatibility with legacy DOS applications
        /// and is read-only.</remarks>
        public ushort e_ip { get; }

        /// <summary>
        /// Gets the initial (relative) CS value used by the loader when loading the executable image.
        /// </summary>
        /// <remarks>This property corresponds to the 'e_cs' field in the DOS header of a portable
        /// executable (PE) file. It is primarily relevant for legacy DOS executables and is typically not used by
        /// modern Windows applications.</remarks>
        public ushort e_cs { get; }

        /// <summary>
        /// Gets the file address of the relocation table for the executable image.
        /// </summary>
        public ushort e_lfarlc { get; }

        /// <summary>
        /// Gets the OEM-specific value associated with the DOS header.
        /// </summary>
        /// <remarks>This property provides the OEM value as defined in the DOS header of a portable
        /// executable (PE) file. The meaning of this value is specific to the OEM and may vary depending on the tool or
        /// system that generated the file.</remarks>
        public ushort e_ovno { get; }

        /// <summary>
        /// Gets the collection of reserved values from the DOS header as a read-only list of unsigned 16-bit integers.
        /// </summary>
        /// <remarks>These values are reserved for future use and typically do not contain meaningful
        /// data. The list is read-only and cannot be modified.</remarks>
        public IReadOnlyList<ushort> e_res { get; }

        /// <summary>
        /// Gets the OEM identifier associated with the current instance.
        /// </summary>
        /// <remarks>This property provides the OEM ID that can be used to identify the manufacturer of
        /// the hardware. It is particularly useful in scenarios where OEM-specific behavior or configurations are
        /// required.</remarks>
        public ushort e_oemid { get; }

        /// <summary>
        /// Gets the OEM-specific information associated with the file header.
        /// </summary>
        /// <remarks>This property provides a 16-bit value reserved for use by the original equipment
        /// manufacturer (OEM). The meaning of this value is defined by the OEM and may vary between different file
        /// formats or systems.</remarks>
        public ushort e_oeminfo { get; }

        /// <summary>
        /// Gets the collection of reserved values for the DOS header as a read-only list of unsigned 16-bit integers.
        /// </summary>
        /// <remarks>These values are reserved for future use and typically do not contain meaningful
        /// data. The list is immutable and cannot be modified.</remarks>
        public IReadOnlyList<ushort> e_res2 { get; }

        /// <summary>
        /// Gets the file offset to the PE (Portable Executable) header within the image file.
        /// </summary>
        /// <remarks>This property provides the offset, in bytes, from the beginning of the file to the PE
        /// header. It is used when parsing or analyzing PE files to locate the main header information.</remarks>
        public int e_lfanew { get; }
    }
}
