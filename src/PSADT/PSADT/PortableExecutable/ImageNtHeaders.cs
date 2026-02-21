using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents the IMAGE_NT_HEADERS structure of a PE file.
    /// </summary>
    public sealed record ImageNtHeaders
    {
        /// <summary>
        /// Initializes a new instance of the ImageNtHeaders class using the specified signature, file header, and
        /// optional header values.
        /// </summary>
        /// <param name="signature">The signature value that identifies the image format.</param>
        /// <param name="fileHeader">The file header structure containing information about the image file, such as machine type and section
        /// count.</param>
        /// <param name="optionalHeader">The optional header structure providing additional details about the image, including entry point and image
        /// base address.</param>
        internal ImageNtHeaders(uint signature, in IMAGE_FILE_HEADER fileHeader, in IMAGE_OPTIONAL_HEADER32 optionalHeader)
        {
            Signature = signature;
            FileHeader = new(in fileHeader);
            OptionalHeader = new(in optionalHeader);
        }

        /// <summary>
        /// Initializes a new instance of the ImageNtHeaders class using the specified NT signature, file header, and
        /// optional header for a 64-bit Windows image.
        /// </summary>
        /// <remarks>Use this constructor when manually constructing or parsing the NT headers of a 64-bit
        /// Portable Executable (PE) file. Ensure that the provided header structures are valid and correspond to the
        /// image being processed.</remarks>
        /// <param name="signature">The NT signature that identifies the image file format. Typically set to the constant value representing
        /// 'PE\0\0'.</param>
        /// <param name="fileHeader">A read-only reference to the IMAGE_FILE_HEADER structure containing metadata about the image file, such as
        /// machine type and section count.</param>
        /// <param name="optionalHeader">A read-only reference to the IMAGE_OPTIONAL_HEADER64 structure that provides additional information about
        /// the image, including the entry point address and image base.</param>
        internal ImageNtHeaders(uint signature, in IMAGE_FILE_HEADER fileHeader, in IMAGE_OPTIONAL_HEADER64 optionalHeader)
        {
            Signature = signature;
            FileHeader = new(in fileHeader);
            OptionalHeader = new(in optionalHeader);
        }

        /// <summary>
        /// Gets the PE signature (should be 0x00004550 "PE\0\0").
        /// </summary>
        public uint Signature { get; }

        /// <summary>
        /// Gets the IMAGE_FILE_HEADER structure.
        /// </summary>
        public ImageFileHeader FileHeader { get; }

        /// <summary>
        /// Gets the IMAGE_OPTIONAL_HEADER structure.
        /// </summary>
        public ImageOptionalHeader OptionalHeader { get; }
    }
}
