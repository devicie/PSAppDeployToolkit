using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.Win32;
using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents MISC debug information from an IMAGE_DEBUG_TYPE_MISC entry.
    /// </summary>
    /// <remarks>
    /// MISC data typically contains the path to an external DBG file.
    /// This is a legacy debug format predating PDB files.
    /// </remarks>
    public sealed record ImageDebugMisc
    {
        /// <summary>
        /// Parses MISC data from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader positioned at the MISC data.</param>
        /// <param name="size">The size of the MISC data in bytes.</param>
        /// <returns>An ImageDebugMiscData instance, or null if the data is invalid.</returns>
        internal static ImageDebugMisc? Parse(BinaryReader reader, uint size)
        {
            // Confirm the size is correct to process.
            if (size < MinHeaderSize)
            {
                return null;
            }

            // Read the fixed header portion
            uint dataType = reader.ReadUInt32();
            uint length = reader.ReadUInt32();
            bool isUnicode = reader.ReadByte() != 0;
            _ = reader.ReadBytes(3); // Reserved

            // Calculate string length (Length includes the full structure)
            int stringLength = (int)(length - MinHeaderSize);
            if (stringLength > 0)
            {
                byte[] stringBytes = reader.ReadBytes(stringLength);
                string data = (isUnicode ? Encoding.Unicode.GetString(stringBytes) : Encoding.ASCII.GetString(stringBytes)).TrimEnd('\0');
                return new(dataType, isUnicode, data);
            }
            return new(dataType, isUnicode);
        }

        /// <summary>
        /// Initializes a new instance of the ImageDebugMiscData class.
        /// </summary>
        private ImageDebugMisc(uint dataType, bool isUnicode, string? data = null)
        {
            DataType = dataType;
            IsUnicode = isUnicode;
            Data = !string.IsNullOrWhiteSpace(data) ? data : null;
        }

        /// <summary>
        /// Gets the data type. A value of 1 indicates an external DBG file path.
        /// </summary>
        public uint DataType { get; }

        /// <summary>
        /// Gets a value indicating whether the data string was stored as Unicode.
        /// </summary>
        public bool IsUnicode { get; }

        /// <summary>
        /// Gets the data string (typically the path to an external DBG file).
        /// </summary>
        public string? Data { get; }

        /// <summary>
        /// Gets a value indicating whether this entry contains an external DBG file path.
        /// </summary>
        public bool IsExternalDbgPath => DataType == PInvoke.IMAGE_DEBUG_MISC_EXENAME;

        /// <summary>
        /// The minimum header size for IMAGE_DEBUG_MISC structure (excluding variable-length Data field).
        /// </summary>
        private static readonly uint MinHeaderSize = (uint)Unsafe.SizeOf<IMAGE_DEBUG_MISC>();
    }
}
