using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using PSADT.Interop;
using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a relocation block with its entries.
    /// </summary>
    public sealed record ImageBaseRelocation
    {
        /// <summary>
        /// Parses relocation information from the given reader.
        /// </summary>
        internal static ReadOnlyCollection<ImageBaseRelocation>? Parse(BinaryReader reader, IReadOnlyList<ImageSectionHeader> sections, long basePosition, ImageDataDirectory relocDataDir)
        {
            // Confirm the offset is valid before starting.
            long offset = PortableExecutableUtilities.RvaToFileOffset(relocDataDir.VirtualAddress, sections);
            if (offset < 0)
            {
                return null;
            }

            // Read out every entry and return the list to the caller.
            reader.BaseStream.Position = basePosition + offset;
            long endPosition = reader.BaseStream.Position + relocDataDir.Size;
            List<ImageBaseRelocation> blocks = [];
            while (reader.BaseStream.Position < endPosition)
            {
                // Confirm we haven't reached the end.
                ref readonly IMAGE_BASE_RELOCATION baseReloc = ref PortableExecutableUtilities.ReadStruct<IMAGE_BASE_RELOCATION>(reader);
                if (baseReloc.VirtualAddress == 0 || baseReloc.SizeOfBlock == 0)
                {
                    break;
                }

                // Calculate number of entries: (SizeOfBlock - header size) / 2 bytes per entry
                int entryCount = (int)((baseReloc.SizeOfBlock - 8) / 2);
                List<ImageBaseRelocationEntry> entries = new(entryCount);
                for (int i = 0; i < entryCount; i++)
                {
                    // Skip padding entries (type ABSOLUTE)
                    ushort rawEntry = reader.ReadUInt16();
                    if ((IMAGE_REL_BASED)(rawEntry >> 12) != IMAGE_REL_BASED.IMAGE_REL_BASED_ABSOLUTE)
                    {
                        entries.Add(new(rawEntry, baseReloc.VirtualAddress));
                    }
                }
                if (entries.Count > 0)
                {
                    blocks.Add(new(in baseReloc, new(entries)));
                }
            }
            return blocks.Count > 0 ? new(blocks) : null;
        }

        /// <summary>
        /// Initializes a new instance of the RelocationBlock class with the specified base relocation information and
        /// relocation entries.
        /// </summary>
        /// <param name="baseRelocation">The base relocation information that defines the starting address and size for the relocation block.</param>
        /// <param name="entries">A read-only collection of relocation entries to be associated with this relocation block. Cannot be null.</param>
        private ImageBaseRelocation(in IMAGE_BASE_RELOCATION baseRelocation, ReadOnlyCollection<ImageBaseRelocationEntry> entries)
        {
            BaseRelocation = baseRelocation;
            Entries = entries;
        }

        /// <summary>
        /// Gets the RVA of the page this block applies to.
        /// </summary>
        public uint VirtualAddress => BaseRelocation.VirtualAddress;

        /// <summary>
        /// Gets the size of the relocation block including the header and all entries.
        /// </summary>
        public uint SizeOfBlock => BaseRelocation.SizeOfBlock;

        /// <summary>
        /// Gets the relocation entries in this block.
        /// </summary>
        public IReadOnlyList<ImageBaseRelocationEntry> Entries { get; }

        /// <summary>
        /// Gets the raw IMAGE_BASE_RELOCATION structure.
        /// </summary>
        private readonly IMAGE_BASE_RELOCATION BaseRelocation;
    }
}
