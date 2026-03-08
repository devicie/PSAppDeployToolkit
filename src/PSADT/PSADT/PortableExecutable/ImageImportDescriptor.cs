using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;
using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a single imported DLL with its functions.
    /// </summary>
    public sealed record ImageImportDescriptor
    {
        /// <summary>
        /// Parses import information from the given reader and import descriptors.
        /// </summary>
        /// <param name="reader">The binary reader positioned in the PE file.</param>
        /// <param name="importDescriptors">The array of IMAGE_IMPORT_DESCRIPTOR structures.</param>
        /// <param name="sections">The section headers for RVA translation.</param>
        /// <param name="basePosition">The base position of the PE file in the stream.</param>
        /// <param name="is64Bit">Whether this is a 64-bit PE file.</param>
        internal static ReadOnlyCollection<ImageImportDescriptor>? Parse(BinaryReader reader, ReadOnlyCollection<IMAGE_IMPORT_DESCRIPTOR> importDescriptors, IReadOnlyList<ImageSectionHeader> sections, long basePosition, bool is64Bit)
        {
            List<ImageImportDescriptor> imports = [];
            foreach (IMAGE_IMPORT_DESCRIPTOR descriptor in importDescriptors)
            {
                // Read the DLL name.
                string dllName = PortableExecutableUtilities.ReadNullTerminatedStringAtRva(reader, descriptor.Name, sections, basePosition);

                // Parse the Import Lookup Table (ILT) or Import Address Table (IAT) if ILT is not present.
                // Use OriginalFirstThunk (ILT) if available, otherwise use FirstThunk (IAT).
                uint thunkRva = descriptor.Anonymous.OriginalFirstThunk != 0 ? descriptor.Anonymous.OriginalFirstThunk : descriptor.FirstThunk;
                List<ImageThunkData> entries = [];
                if (thunkRva != 0)
                {
                    long offset = PortableExecutableUtilities.RvaToFileOffset(thunkRva, sections);
                    if (offset >= 0)
                    {
                        reader.BaseStream.Position = basePosition + offset;
                        entries = PortableExecutableUtilities.ParseImportThunkArray(reader, sections, basePosition, is64Bit);
                    }
                }
                if (entries.Count > 0)
                {
                    imports.Add(new(in descriptor, dllName, new(entries)));
                }
            }
            return imports.Count > 0 ? new(imports) : null;
        }

        /// <summary>
        /// Initializes a new instance of the ImageImportDescriptor class.
        /// </summary>
        /// <param name="descriptor">The IMAGE_IMPORT_DESCRIPTOR structure.</param>
        /// <param name="name">The name of the DLL.</param>
        /// <param name="entries">The collection of import entries.</param>
        private ImageImportDescriptor(in IMAGE_IMPORT_DESCRIPTOR descriptor, string name, ReadOnlyCollection<ImageThunkData> entries)
        {
            Anonymous = new(descriptor.Anonymous.Characteristics, descriptor.Anonymous.OriginalFirstThunk);
            Descriptor = descriptor;
            Name = name;
            Entries = entries;
        }

        /// <summary>
        /// Gets the anonymous union containing Characteristics/OriginalFirstThunk.
        /// </summary>
        public ImageImportDescriptor0 Anonymous { get; }

        /// <summary>
        /// Gets the timestamp for bound imports (0 if not bound, -1 if new-style binding).
        /// </summary>
        public DateTime? TimeDateStamp => Descriptor.TimeDateStamp > 0
            ? DateTimeOffset.FromUnixTimeSeconds(Descriptor.TimeDateStamp).UtcDateTime
            : null;

        /// <summary>
        /// Gets the forwarder chain index (-1 if no forwarders).
        /// </summary>
        public uint ForwarderChain => Descriptor.ForwarderChain;

        /// <summary>
        /// Gets the name of the imported DLL.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the RVA of the Import Address Table.
        /// </summary>
        public uint FirstThunk => Descriptor.FirstThunk;

        /// <summary>
        /// Gets the collection of imported functions from this DLL.
        /// </summary>
        /// <remarks>
        /// Contains <see cref="ImageImportByName"/> for named imports and <see cref="ImageThunkData"/> for ordinal imports.
        /// Use <see cref="ImageThunkData.IsOrdinal"/> or type checking to distinguish between them.
        /// </remarks>
        public IReadOnlyList<ImageThunkData> Entries { get; }

        /// <summary>
        /// Gets the raw IMAGE_IMPORT_DESCRIPTOR structure.
        /// </summary>
        private readonly IMAGE_IMPORT_DESCRIPTOR Descriptor;

        /// <summary>
        /// Represents the anonymous union in IMAGE_IMPORT_DESCRIPTOR containing either Characteristics or OriginalFirstThunk.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "This is meant to reflect an anonymous union.")]
        [StructLayout(LayoutKind.Explicit)]
        public readonly record struct ImageImportDescriptor0
        {
            /// <summary>
            /// Initializes a new instance of the ImageImportDescriptor0 struct.
            /// </summary>
            /// <param name="characteristics">The characteristics value (0 for terminating descriptor).</param>
            /// <param name="originalFirstThunk">The RVA of the Import Lookup Table.</param>
            internal ImageImportDescriptor0(uint characteristics, uint originalFirstThunk)
            {
                Characteristics = characteristics;
                OriginalFirstThunk = originalFirstThunk;
            }

            /// <summary>
            /// Gets the characteristics value. Zero indicates a terminating null import descriptor.
            /// </summary>
            [FieldOffset(0)]
            public readonly uint Characteristics;

            /// <summary>
            /// Gets the RVA of the Import Lookup Table (ILT), which contains names or ordinals for each import.
            /// </summary>
            [FieldOffset(0)]
            public readonly uint OriginalFirstThunk;
        }
    }
}
