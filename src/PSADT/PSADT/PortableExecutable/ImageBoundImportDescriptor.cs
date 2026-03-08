using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a parsed bound import entry.
    /// </summary>
    public sealed record ImageBoundImportDescriptor
    {
        /// <summary>
        /// Parses the bound import directory from the specified binary reader and returns a collection of image bound
        /// import descriptors.
        /// </summary>
        /// <remarks>The method validates the bound import directory before attempting to parse it. If the
        /// directory is invalid, it returns null. The parsing process includes reading the DLL names and any associated
        /// forwarder references.</remarks>
        /// <param name="reader">The binary reader used to read data from the binary stream.</param>
        /// <param name="basePosition">The base position in the binary stream from which the bound import directory is calculated.</param>
        /// <param name="boundImportDataDir">The image data directory that contains the bound import information to be parsed.</param>
        /// <returns>A read-only collection of ImageBoundImportDescriptor objects representing the parsed bound imports, or null
        /// if no valid entries are found.</returns>
        internal static ReadOnlyCollection<ImageBoundImportDescriptor>? Parse(BinaryReader reader, long basePosition, ImageDataDirectory boundImportDataDir)
        {
            // Confirm the directory is valid.
            if (boundImportDataDir.VirtualAddress == 0 || boundImportDataDir.Size == 0)
            {
                return null;
            }

            // Bound import directory uses raw file offset, not RVA.
            long directoryStart = basePosition + boundImportDataDir.VirtualAddress;
            reader.BaseStream.Position = directoryStart;
            List<ImageBoundImportDescriptor> entries = [];
            while (true)
            {
                // Confirm the descriptor is valid.
                ref readonly IMAGE_BOUND_IMPORT_DESCRIPTOR descriptor = ref PortableExecutableUtilities.ReadStruct<IMAGE_BOUND_IMPORT_DESCRIPTOR>(reader);
                if (descriptor.TimeDateStamp == 0 && descriptor.OffsetModuleName == 0)
                {
                    break; // Null-terminated array.
                }

                // Read the DLL name from the offset (relative to the start of the bound import directory).
                string dllName = PortableExecutableUtilities.ReadNullTerminatedStringAtPosition(reader, directoryStart + descriptor.OffsetModuleName);

                // Read forwarder references.
                List<ImageBoundForwarderRef> forwarderRefs = [];
                for (int i = 0; i < descriptor.NumberOfModuleForwarderRefs; i++)
                {
                    ref readonly IMAGE_BOUND_FORWARDER_REF forwarderRef = ref PortableExecutableUtilities.ReadStruct<IMAGE_BOUND_FORWARDER_REF>(reader);
                    string forwarderDllName = PortableExecutableUtilities.ReadNullTerminatedStringAtPosition(reader, directoryStart + forwarderRef.OffsetModuleName);
                    forwarderRefs.Add(new(in forwarderRef, forwarderDllName));
                }
                entries.Add(new(in descriptor, dllName, new(forwarderRefs)));
            }
            return entries.Count > 0 ? new(entries) : null;
        }

        /// <summary>
        /// Initializes a new instance of the BoundImportEntry class with the specified import descriptor, DLL name, and
        /// forwarder references.
        /// </summary>
        /// <param name="descriptor">The IMAGE_BOUND_IMPORT_DESCRIPTOR structure that provides information about the bound import entry.</param>
        /// <param name="moduleName">The name of the DLL associated with this import entry. Cannot be null.</param>
        /// <param name="moduleForwarderRefs">A read-only collection of BoundForwarderRef objects representing forwarder references related to the import
        /// entry. Cannot be null.</param>
        private ImageBoundImportDescriptor(in IMAGE_BOUND_IMPORT_DESCRIPTOR descriptor, string moduleName, ReadOnlyCollection<ImageBoundForwarderRef> moduleForwarderRefs)
        {
            Descriptor = descriptor;
            ModuleName = moduleName;
            ModuleForwarderRefs = moduleForwarderRefs;
        }

        /// <summary>
        /// Gets the timestamp of the bound DLL, or null if the timestamp is zero.
        /// </summary>
        public DateTime? TimeDateStamp => Descriptor.TimeDateStamp > 0
            ? DateTimeOffset.FromUnixTimeSeconds(Descriptor.TimeDateStamp).UtcDateTime
            : null;

        /// <summary>
        /// Gets the name of the bound DLL.
        /// </summary>
        public string ModuleName { get; }

        /// <summary>
        /// Gets the forwarder references for this bound import.
        /// </summary>
        public IReadOnlyList<ImageBoundForwarderRef> ModuleForwarderRefs { get; }

        /// <summary>
        /// Gets the raw IMAGE_BOUND_IMPORT_DESCRIPTOR structure.
        /// </summary>
        private readonly IMAGE_BOUND_IMPORT_DESCRIPTOR Descriptor;
    }
}
