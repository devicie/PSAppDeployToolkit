using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Provides parsing functionality for PE resource directories.
    /// </summary>
    internal static class ImageResourceDirectory
    {
        /// <summary>
        /// Parses the resource directory tree from the specified reader.
        /// </summary>
        /// <param name="reader">The binary reader positioned at the resource section.</param>
        /// <param name="resourceSectionBase">The file offset of the resource section base.</param>
        /// <returns>A parsed <see cref="ImageResourceIdDirectory"/> root (the root is always ID-based with ID 0), or null if parsing fails.</returns>
        internal static ImageResourceIdDirectory Parse(BinaryReader reader, long resourceSectionBase)
        {
            return ParseIdDirectory(reader, resourceSectionBase, resourceSectionBase, 0, 0);
        }

        /// <summary>
        /// Parses a resource directory identified by ID.
        /// </summary>
        private static ImageResourceIdDirectory ParseIdDirectory(BinaryReader reader, long resourceSectionBase, long directoryOffset, int depth, uint id)
        {
            reader.BaseStream.Position = directoryOffset;
            ref readonly IMAGE_RESOURCE_DIRECTORY header = ref PortableExecutableUtilities.ReadStruct<IMAGE_RESOURCE_DIRECTORY>(reader);
            (ReadOnlyCollection<ImageResourceNamedNode> namedEntries, ReadOnlyCollection<ImageResourceIdNode> idEntries) = ParseEntries(reader, resourceSectionBase, header, depth);
            return new(in header, namedEntries, idEntries, id);
        }

        /// <summary>
        /// Parses a resource directory identified by name.
        /// </summary>
        private static ImageResourceNamedDirectory ParseNamedDirectory(BinaryReader reader, long resourceSectionBase, long directoryOffset, int depth, string name)
        {
            reader.BaseStream.Position = directoryOffset;
            ref readonly IMAGE_RESOURCE_DIRECTORY header = ref PortableExecutableUtilities.ReadStruct<IMAGE_RESOURCE_DIRECTORY>(reader);
            (ReadOnlyCollection<ImageResourceNamedNode> namedEntries, ReadOnlyCollection<ImageResourceIdNode> idEntries) = ParseEntries(reader, resourceSectionBase, header, depth);
            return new(in header, namedEntries, idEntries, name);
        }

        /// <summary>
        /// Parses the entries of a resource directory.
        /// </summary>
        private static (ReadOnlyCollection<ImageResourceNamedNode> namedEntries, ReadOnlyCollection<ImageResourceIdNode> idEntries) ParseEntries(BinaryReader reader, long resourceSectionBase, in IMAGE_RESOURCE_DIRECTORY header, int depth)
        {
            // Read all entries for the given directory.
            List<ImageResourceNamedNode> namedEntries = new(header.NumberOfNamedEntries);
            List<ImageResourceIdNode> idEntries = new(header.NumberOfIdEntries);
            int totalEntries = header.NumberOfNamedEntries + header.NumberOfIdEntries;
            for (int i = 0; i < totalEntries; i++)
            {
                // Read the directory header.
                ref readonly IMAGE_RESOURCE_DIRECTORY_ENTRY rawEntry = ref PortableExecutableUtilities.ReadStruct<IMAGE_RESOURCE_DIRECTORY_ENTRY>(reader);
                long currentPosition = reader.BaseStream.Position;
                if (rawEntry.Anonymous1.Anonymous.NameIsString)
                {
                    reader.BaseStream.Position = resourceSectionBase + rawEntry.Anonymous1.Anonymous.NameOffset;
                    string entryName = Encoding.Unicode.GetString(reader.ReadBytes(reader.ReadUInt16() * 2));
                    reader.BaseStream.Position = currentPosition;
                    if (!rawEntry.Anonymous2.Anonymous.DataIsDirectory)
                    {
                        long dataEntryOffset = resourceSectionBase + rawEntry.Anonymous2.OffsetToData; reader.BaseStream.Position = dataEntryOffset;
                        namedEntries.Add(new ImageResourceNamedDataEntry(in PortableExecutableUtilities.ReadStruct<IMAGE_RESOURCE_DATA_ENTRY>(reader), entryName));
                    }
                    else
                    {
                        long subdirectoryOffset = resourceSectionBase + rawEntry.Anonymous2.Anonymous.OffsetToDirectory;
                        namedEntries.Add(ParseNamedDirectory(reader, resourceSectionBase, subdirectoryOffset, depth + 1, entryName));
                    }
                }
                else
                {
                    uint entryId = rawEntry.Anonymous1.Id;
                    if (!rawEntry.Anonymous2.Anonymous.DataIsDirectory)
                    {
                        long dataEntryOffset = resourceSectionBase + rawEntry.Anonymous2.OffsetToData; reader.BaseStream.Position = dataEntryOffset;
                        idEntries.Add(new ImageResourceIdDataEntry(in PortableExecutableUtilities.ReadStruct<IMAGE_RESOURCE_DATA_ENTRY>(reader), entryId));
                    }
                    else
                    {
                        long subdirectoryOffset = resourceSectionBase + rawEntry.Anonymous2.Anonymous.OffsetToDirectory;
                        idEntries.Add(ParseIdDirectory(reader, resourceSectionBase, subdirectoryOffset, depth + 1, entryId));
                    }
                }
                reader.BaseStream.Position = currentPosition;
            }
            return (new(namedEntries), new(idEntries));
        }
    }
}
