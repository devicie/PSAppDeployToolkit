using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using PSADT.Interop;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents POGO (Profile Guided Optimization) debug information from an IMAGE_DEBUG_TYPE_POGO entry.
    /// </summary>
    /// <remarks>
    /// POGO data contains a 4-byte signature followed by entries describing optimized sections.
    /// Known signatures are defined in <see cref="IMAGE_DEBUG_POGO_SIGNATURE"/>.
    /// </remarks>
    public sealed record ImageDebugPogoData
    {
        /// <summary>
        /// Parses POGO data from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader positioned at the POGO data.</param>
        /// <param name="size">The size of the POGO data in bytes.</param>
        /// <returns>A PogoInfo instance, or null if the data is invalid.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2263:Prefer generic overload when type is known", Justification = "This isn't supported on net472.")]
        internal static ImageDebugPogoData? Parse(BinaryReader reader, uint size)
        {
            // Confirm the size is valid.
            if (size < 4)
            {
                return null;
            }

            // Validate known signatures.
            long startPosition = reader.BaseStream.Position; long endPosition = startPosition + size;
            IMAGE_DEBUG_POGO_SIGNATURE signature = (IMAGE_DEBUG_POGO_SIGNATURE)reader.ReadUInt32();
            if (!Enum.IsDefined(typeof(IMAGE_DEBUG_POGO_SIGNATURE), signature))
            {
                return null;
            }

            // Read entries until we reach the end of the data.
            List<ImageDebugPogoEntry> entries = [];
            while (reader.BaseStream.Position + 8 <= endPosition)
            {
                // A zero RVA and size typically indicates the end of entries
                uint rva = reader.ReadUInt32(); uint entrySize = reader.ReadUInt32();
                if (rva == 0 && entrySize == 0)
                {
                    break;
                }

                // Read the null-terminated section name.
                string name = PortableExecutableUtilities.ReadNullTerminatedAsciiString(reader);

                // Align to 4-byte boundary after the name
                long currentPos = reader.BaseStream.Position;
                long alignment = (4 - (currentPos % 4)) % 4;
                if (currentPos + alignment <= endPosition)
                {
                    reader.BaseStream.Position = currentPos + alignment;
                }
                entries.Add(new(rva, entrySize, name));
            }
            return entries.Count > 0 ? new(signature, new(entries)) : null;
        }

        /// <summary>
        /// Initializes a new instance of the PogoInfo class.
        /// </summary>
        private ImageDebugPogoData(IMAGE_DEBUG_POGO_SIGNATURE signature, ReadOnlyCollection<ImageDebugPogoEntry> entries)
        {
            Signature = signature;
            Entries = entries;
        }

        /// <summary>
        /// Gets the signature identifying the POGO type.
        /// </summary>
        public IMAGE_DEBUG_POGO_SIGNATURE Signature { get; }

        /// <summary>
        /// Gets the list of POGO entries describing optimized sections.
        /// </summary>
        public IReadOnlyList<ImageDebugPogoEntry> Entries { get; }
    }
}
