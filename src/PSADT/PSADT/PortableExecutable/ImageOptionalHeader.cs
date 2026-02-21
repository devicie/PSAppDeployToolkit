using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents the optional header of a PE file, with all pointer-sized fields at 64-bit width.
    /// </summary>
    public sealed record ImageOptionalHeader
    {
        /// <summary>
        /// Initializes a new instance of the ImageOptionalHeader class using the specified IMAGE_OPTIONAL_HEADER32
        /// structure.
        /// </summary>
        /// <remarks>This constructor sets various properties of the ImageOptionalHeader based on the
        /// provided header, which includes details such as the image base, section alignment, and subsystem
        /// version.</remarks>
        /// <param name="header">The IMAGE_OPTIONAL_HEADER32 structure containing the optional header information to initialize the instance.</param>
        internal ImageOptionalHeader(in Windows.Win32.System.Diagnostics.Debug.IMAGE_OPTIONAL_HEADER32 header)
        {
            Magic = (IMAGE_OPTIONAL_HEADER_MAGIC)header.Magic;
            LinkerVersion = new(header.MajorLinkerVersion, header.MinorLinkerVersion);
            SizeOfCode = header.SizeOfCode;
            SizeOfInitializedData = header.SizeOfInitializedData;
            SizeOfUninitializedData = header.SizeOfUninitializedData;
            AddressOfEntryPoint = header.AddressOfEntryPoint;
            BaseOfCode = header.BaseOfCode;
            BaseOfData = header.BaseOfData;
            ImageBase = header.ImageBase;
            SectionAlignment = header.SectionAlignment;
            FileAlignment = header.FileAlignment;
            OperatingSystemVersion = new(header.MajorOperatingSystemVersion, header.MinorOperatingSystemVersion);
            ImageVersion = new(header.MajorImageVersion, header.MinorImageVersion);
            SubsystemVersion = new(header.MajorSubsystemVersion, header.MinorSubsystemVersion);
            Win32VersionValue = header.Win32VersionValue;
            SizeOfImage = header.SizeOfImage;
            SizeOfHeaders = header.SizeOfHeaders;
            CheckSum = header.CheckSum;
            Subsystem = (IMAGE_SUBSYSTEM)header.Subsystem;
            DllCharacteristics = (IMAGE_DLL_CHARACTERISTICS)header.DllCharacteristics;
            SizeOfStackReserve = header.SizeOfStackReserve;
            SizeOfStackCommit = header.SizeOfStackCommit;
            SizeOfHeapReserve = header.SizeOfHeapReserve;
            SizeOfHeapCommit = header.SizeOfHeapCommit;
            NumberOfRvaAndSizes = header.NumberOfRvaAndSizes;
            DataDirectories = new ReadOnlyCollection<ImageDataDirectory>([
                new(in header.DataDirectory._0),
                new(in header.DataDirectory._1),
                new(in header.DataDirectory._2),
                new(in header.DataDirectory._3),
                new(in header.DataDirectory._4),
                new(in header.DataDirectory._5),
                new(in header.DataDirectory._6),
                new(in header.DataDirectory._7),
                new(in header.DataDirectory._8),
                new(in header.DataDirectory._9),
                new(in header.DataDirectory._10),
                new(in header.DataDirectory._11),
                new(in header.DataDirectory._12),
                new(in header.DataDirectory._13),
                new(in header.DataDirectory._14),
                new(in header.DataDirectory._15),
            ]);
            Is64Bit = false;
        }

        /// <summary>
        /// Initializes a new instance of the ImageOptionalHeader class using the specified IMAGE_OPTIONAL_HEADER64
        /// structure.
        /// </summary>
        /// <remarks>This constructor sets various properties of the ImageOptionalHeader based on the
        /// provided header, including magic number, version information, and memory layout details.</remarks>
        /// <param name="header">The IMAGE_OPTIONAL_HEADER64 structure that provides optional header information for initialization.</param>
        internal ImageOptionalHeader(in Windows.Win32.System.Diagnostics.Debug.IMAGE_OPTIONAL_HEADER64 header)
        {
            Magic = (IMAGE_OPTIONAL_HEADER_MAGIC)header.Magic;
            LinkerVersion = new(header.MajorLinkerVersion, header.MinorLinkerVersion);
            SizeOfCode = header.SizeOfCode;
            SizeOfInitializedData = header.SizeOfInitializedData;
            SizeOfUninitializedData = header.SizeOfUninitializedData;
            AddressOfEntryPoint = header.AddressOfEntryPoint;
            BaseOfCode = header.BaseOfCode;
            ImageBase = header.ImageBase;
            SectionAlignment = header.SectionAlignment;
            FileAlignment = header.FileAlignment;
            OperatingSystemVersion = new(header.MajorOperatingSystemVersion, header.MinorOperatingSystemVersion);
            ImageVersion = new(header.MajorImageVersion, header.MinorImageVersion);
            SubsystemVersion = new(header.MajorSubsystemVersion, header.MinorSubsystemVersion);
            Win32VersionValue = header.Win32VersionValue;
            SizeOfImage = header.SizeOfImage;
            SizeOfHeaders = header.SizeOfHeaders;
            CheckSum = header.CheckSum;
            Subsystem = (IMAGE_SUBSYSTEM)header.Subsystem;
            DllCharacteristics = (IMAGE_DLL_CHARACTERISTICS)header.DllCharacteristics;
            SizeOfStackReserve = header.SizeOfStackReserve;
            SizeOfStackCommit = header.SizeOfStackCommit;
            SizeOfHeapReserve = header.SizeOfHeapReserve;
            SizeOfHeapCommit = header.SizeOfHeapCommit;
            NumberOfRvaAndSizes = header.NumberOfRvaAndSizes;
            DataDirectories = new ReadOnlyCollection<ImageDataDirectory>([
                new(in header.DataDirectory._0),
                new(in header.DataDirectory._1),
                new(in header.DataDirectory._2),
                new(in header.DataDirectory._3),
                new(in header.DataDirectory._4),
                new(in header.DataDirectory._5),
                new(in header.DataDirectory._6),
                new(in header.DataDirectory._7),
                new(in header.DataDirectory._8),
                new(in header.DataDirectory._9),
                new(in header.DataDirectory._10),
                new(in header.DataDirectory._11),
                new(in header.DataDirectory._12),
                new(in header.DataDirectory._13),
                new(in header.DataDirectory._14),
                new(in header.DataDirectory._15),
            ]);
            Is64Bit = true;
        }

        /// <summary>
        /// Gets the magic number that identifies the PE format (PE32 or PE32+).
        /// </summary>
        public IMAGE_OPTIONAL_HEADER_MAGIC Magic { get; }

        /// <summary>
        /// Gets the linker version.
        /// </summary>
        public Version LinkerVersion { get; }

        /// <summary>
        /// Gets the size of the code section, in bytes, or the sum of all such sections if there are multiple code sections.
        /// </summary>
        public uint SizeOfCode { get; }

        /// <summary>
        /// Gets the size of the initialized data section, in bytes, or the sum of all such sections if there are multiple initialized data sections.
        /// </summary>
        public uint SizeOfInitializedData { get; }

        /// <summary>
        /// Gets the size of the uninitialized data section, in bytes, or the sum of all such sections if there are multiple uninitialized data sections.
        /// </summary>
        public uint SizeOfUninitializedData { get; }

        /// <summary>
        /// Gets the relative virtual address of the entry point function.
        /// </summary>
        public uint AddressOfEntryPoint { get; }

        /// <summary>
        /// Gets the relative virtual address of the beginning of the code section.
        /// </summary>
        public uint BaseOfCode { get; }

        /// <summary>
        /// Gets the relative virtual address of the beginning of the data section (PE32 only, 0 for PE32+).
        /// </summary>
        public uint BaseOfData { get; }

        /// <summary>
        /// Gets the preferred address of the first byte of the image when loaded into memory.
        /// </summary>
        public ulong ImageBase { get; }

        /// <summary>
        /// Gets the alignment of sections when loaded into memory, in bytes.
        /// </summary>
        public uint SectionAlignment { get; }

        /// <summary>
        /// Gets the alignment of sections in the image file, in bytes.
        /// </summary>
        public uint FileAlignment { get; }

        /// <summary>
        /// Gets the required operating system version.
        /// </summary>
        public Version OperatingSystemVersion { get; }

        /// <summary>
        /// Gets the image version.
        /// </summary>
        public Version ImageVersion { get; }

        /// <summary>
        /// Gets the subsystem version.
        /// </summary>
        public Version SubsystemVersion { get; }

        /// <summary>
        /// Gets the Win32 version value (reserved, should be zero).
        /// </summary>
        public uint Win32VersionValue { get; }

        /// <summary>
        /// Gets the size of the image, in bytes, including all headers.
        /// </summary>
        public uint SizeOfImage { get; }

        /// <summary>
        /// Gets the combined size of all headers rounded to a multiple of FileAlignment.
        /// </summary>
        public uint SizeOfHeaders { get; }

        /// <summary>
        /// Gets the image file checksum.
        /// </summary>
        public uint CheckSum { get; }

        /// <summary>
        /// Gets the subsystem required to run this image.
        /// </summary>
        public IMAGE_SUBSYSTEM Subsystem { get; }

        /// <summary>
        /// Gets the DLL characteristics of the image.
        /// </summary>
        public IMAGE_DLL_CHARACTERISTICS DllCharacteristics { get; }

        /// <summary>
        /// Gets the number of bytes to reserve for the stack.
        /// </summary>
        public ulong SizeOfStackReserve { get; }

        /// <summary>
        /// Gets the number of bytes to commit for the stack.
        /// </summary>
        public ulong SizeOfStackCommit { get; }

        /// <summary>
        /// Gets the number of bytes to reserve for the local heap.
        /// </summary>
        public ulong SizeOfHeapReserve { get; }

        /// <summary>
        /// Gets the number of bytes to commit for the local heap.
        /// </summary>
        public ulong SizeOfHeapCommit { get; }

        /// <summary>
        /// Gets the number of directory entries in the remainder of the optional header.
        /// </summary>
        public uint NumberOfRvaAndSizes { get; }

        /// <summary>
        /// Gets the data directory array.
        /// </summary>
        public IReadOnlyList<ImageDataDirectory> DataDirectories { get; }

        /// <summary>
        /// Gets whether this is a PE32+ (64-bit) image.
        /// </summary>
        public bool Is64Bit { get; }
    }
}
