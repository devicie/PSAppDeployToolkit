using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Contains the parsed data directories from a PE file.
    /// Property names correspond to the IMAGE_DIRECTORY_ENTRY_* constants from the Windows SDK.
    /// </summary>
    public sealed record DataDirectories
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataDirectories"/> class.
        /// </summary>
        internal DataDirectories(
            ImageExportDirectory? export,
            ReadOnlyCollection<ImageImportDescriptor>? import,
            ImageResourceIdDirectory? resource,
            ReadOnlyCollection<ImageRuntimeFunctionEntry>? exception,
            ReadOnlyCollection<WinCertificate>? security,
            ReadOnlyCollection<ImageBaseRelocation>? baseReloc,
            ReadOnlyCollection<ImageDebugDirectory>? debug,
            uint? globalPtr,
            ImageTlsDirectory? tls,
            ImageLoadConfigDirectory? loadConfig,
            ReadOnlyCollection<ImageBoundImportDescriptor>? boundImport,
            ReadOnlyCollection<ImageThunkData>? iat,
            ReadOnlyCollection<ImageDelayLoadDescriptor>? delayImport,
            ImageCor20Header? comDescriptor)
        {
            Export = export;
            Import = import;
            Resource = resource;
            Exception = exception;
            Security = security;
            BaseReloc = baseReloc;
            Debug = debug;
            GlobalPtr = globalPtr;
            Tls = tls;
            LoadConfig = loadConfig;
            BoundImport = boundImport;
            Iat = iat;
            DelayImport = delayImport;
            ComDescriptor = comDescriptor;
        }

        /// <summary>
        /// Gets the parsed export directory (IMAGE_DIRECTORY_ENTRY_EXPORT, index 0), or null if not present.
        /// </summary>
        public ImageExportDirectory? Export { get; }

        /// <summary>
        /// Gets the parsed import directory (IMAGE_DIRECTORY_ENTRY_IMPORT, index 1), or null if not present.
        /// </summary>
        public IReadOnlyList<ImageImportDescriptor>? Import { get; }

        /// <summary>
        /// Gets the root resource directory (IMAGE_DIRECTORY_ENTRY_RESOURCE, index 2), or null if not present.
        /// The root directory is always identified by ID (with ID 0).
        /// </summary>
        public ImageResourceIdDirectory? Resource { get; }

        /// <summary>
        /// Gets the parsed exception handling information (IMAGE_DIRECTORY_ENTRY_EXCEPTION, index 3), or null if not present.
        /// </summary>
        public IReadOnlyList<ImageRuntimeFunctionEntry>? Exception { get; }

        /// <summary>
        /// Gets the parsed security/certificate information (IMAGE_DIRECTORY_ENTRY_SECURITY, index 4), or null if not present.
        /// </summary>
        public IReadOnlyList<WinCertificate>? Security { get; }

        /// <summary>
        /// Gets the parsed base relocation information (IMAGE_DIRECTORY_ENTRY_BASERELOC, index 5), or null if not present.
        /// </summary>
        public IReadOnlyList<ImageBaseRelocation>? BaseReloc { get; }

        /// <summary>
        /// Gets the array of IMAGE_DEBUG_DIRECTORY structures (IMAGE_DIRECTORY_ENTRY_DEBUG, index 6), or null if not present.
        /// </summary>
        public IReadOnlyList<ImageDebugDirectory>? Debug { get; }

        /// <summary>
        /// Gets the Global Pointer RVA (IMAGE_DIRECTORY_ENTRY_GLOBALPTR, index 8), or null if not present.
        /// </summary>
        public uint? GlobalPtr { get; }

        /// <summary>
        /// Gets the TLS directory (IMAGE_DIRECTORY_ENTRY_TLS, index 9), or null if not present.
        /// </summary>
        public ImageTlsDirectory? Tls { get; }

        /// <summary>
        /// Gets the load configuration directory (IMAGE_DIRECTORY_ENTRY_LOAD_CONFIG, index 10), or null if not present.
        /// </summary>
        public ImageLoadConfigDirectory? LoadConfig { get; }

        /// <summary>
        /// Gets the parsed bound import information (IMAGE_DIRECTORY_ENTRY_BOUND_IMPORT, index 11), or null if not present.
        /// </summary>
        public IReadOnlyList<ImageBoundImportDescriptor>? BoundImport { get; }

        /// <summary>
        /// Gets the parsed Import Address Table (IMAGE_DIRECTORY_ENTRY_IAT, index 12), or null if not present.
        /// </summary>
        public IReadOnlyList<ImageThunkData>? Iat { get; }

        /// <summary>
        /// Gets the parsed delay import information (IMAGE_DIRECTORY_ENTRY_DELAY_IMPORT, index 13), or null if not present.
        /// </summary>
        public IReadOnlyList<ImageDelayLoadDescriptor>? DelayImport { get; }

        /// <summary>
        /// Gets the CLR runtime header (IMAGE_DIRECTORY_ENTRY_COM_DESCRIPTOR, index 14), or null if not a .NET assembly.
        /// </summary>
        public ImageCor20Header? ComDescriptor { get; }
    }
}
