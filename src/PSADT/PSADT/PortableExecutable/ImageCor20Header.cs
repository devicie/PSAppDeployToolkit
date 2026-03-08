using System;
using System.Runtime.InteropServices;
using PSADT.Interop;
using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents the CLR runtime header (IMAGE_COR20_HEADER) for .NET assemblies.
    /// </summary>
    /// <remarks>
    /// This header is present in managed executables and contains information about
    /// the CLR metadata, resources, strong name signature, and entry point.
    /// </remarks>
    public sealed record ImageCor20Header
    {
        /// <summary>
        /// Initializes a new instance of the ImageCor20Header class.
        /// </summary>
        /// <param name="header">The IMAGE_COR20_HEADER structure.</param>
        internal ImageCor20Header(in IMAGE_COR20_HEADER header)
        {
            Anonymous = new(header.Anonymous.EntryPointToken, header.Anonymous.EntryPointRVA);
            Header = header;
        }

        /// <summary>
        /// Gets the size of the header in bytes.
        /// </summary>
        public uint Size => Header.cb;

        /// <summary>
        /// Gets the major version of the CLR runtime required.
        /// </summary>
        public Version RuntimeVersion => new(Header.MajorRuntimeVersion, Header.MinorRuntimeVersion);

        /// <summary>
        /// Gets the metadata directory containing the CLI metadata tables.
        /// </summary>
        public ImageDataDirectory MetaData => new(in Header.MetaData);

        /// <summary>
        /// Gets the runtime flags.
        /// </summary>
        public COMIMAGE_FLAGS Flags => (COMIMAGE_FLAGS)Header.Flags;

        /// <summary>
        /// Gets the entry point metadata token (MethodDef or File token).
        /// </summary>
        /// <remarks>
        /// For EXEs, this is the Main method token.
        /// For DLLs, this is typically 0.
        /// </remarks>
        public ImageCor20Header0 Anonymous { get; }

        /// <summary>
        /// Gets the entry point RVA for native entry points.
        /// </summary>
        public uint EntryPointRva => Header.Anonymous.EntryPointRVA;

        /// <summary>
        /// Gets the managed resources directory.
        /// </summary>
        /// <remarks>
        /// Contains embedded managed resources (not to be confused with Win32 resources).
        /// </remarks>
        public ImageDataDirectory Resources => new(in Header.Resources);

        /// <summary>
        /// Gets the strong name signature directory.
        /// </summary>
        public ImageDataDirectory StrongNameSignature => new(in Header.StrongNameSignature);

        /// <summary>
        /// Gets the code manager table directory.
        /// </summary>
        /// <remarks>
        /// Reserved, should be zero.
        /// </remarks>
        public ImageDataDirectory CodeManagerTable => new(in Header.CodeManagerTable);

        /// <summary>
        /// Gets the VTable fixups directory.
        /// </summary>
        /// <remarks>
        /// Used for COM interop and mixed-mode assemblies.
        /// </remarks>
        public ImageDataDirectory VTableFixups => new(in Header.VTableFixups);

        /// <summary>
        /// Gets the export address table jumps directory.
        /// </summary>
        /// <remarks>
        /// Reserved, should be zero.
        /// </remarks>
        public ImageDataDirectory ExportAddressTableJumps => new(in Header.ExportAddressTableJumps);

        /// <summary>
        /// Gets the managed native header directory.
        /// </summary>
        /// <remarks>
        /// Used for native images (NGEN/ReadyToRun).
        /// </remarks>
        public ImageDataDirectory ManagedNativeHeader => new(in Header.ManagedNativeHeader);

        /// <summary>
        /// Gets the raw IMAGE_COR20_HEADER structure.
        /// </summary>
        private readonly IMAGE_COR20_HEADER Header;

        /// <summary>
        /// Represents the entry point information for a .NET assembly, including either the managed entry point token
        /// or the native entry point relative virtual address (RVA).
        /// </summary>
        /// <remarks>This record struct encapsulates the header fields used by the runtime to identify the
        /// starting point of execution in a portable executable (PE) file. The entry point can be specified as a
        /// managed token or a native RVA, depending on the assembly type. This struct is designed to reflect the union
        /// structure found in the PE format specification.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "This is meant to reflect an anonymous union.")]
        [StructLayout(LayoutKind.Explicit)]
        public readonly record struct ImageCor20Header0
        {
            /// <summary>
            /// Initializes a new instance of the ImageCor20Header0 class using the specified entry point token and
            /// relative virtual address (RVA).
            /// </summary>
            /// <param name="entryPointToken">The metadata token that identifies the entry point method within the portable executable image.</param>
            /// <param name="entryPointRVA">The relative virtual address (RVA) of the entry point method in the image.</param>
            internal ImageCor20Header0(uint entryPointToken, uint entryPointRVA)
            {
                EntryPointToken = entryPointToken;
                EntryPointRVA = entryPointRVA;
            }

            /// <summary>
            /// Gets the metadata token that identifies the entry point of the module.
            /// </summary>
            /// <remarks>The entry point token is used by the runtime to locate the starting method
            /// for execution. This value is typically set for executable modules and is essential for determining where
            /// program execution begins.</remarks>
            [FieldOffset(0)]
            public readonly uint EntryPointToken;

            /// <summary>
            /// Gets the relative virtual address (RVA) of the entry point for the module.
            /// </summary>
            /// <remarks>The entry point RVA is used to identify the starting point of execution for
            /// the module. It is essential for loading and executing the module correctly.</remarks>
            [FieldOffset(0)]
            public readonly uint EntryPointRVA;
        }
    }
}
