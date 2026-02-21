using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a single OMAP (Optimized MAP) entry for address mapping.
    /// </summary>
    /// <remarks>
    /// OMAP entries are used to map addresses when code has been optimized and rearranged.
    /// Each entry maps an RVA to another RVA.
    /// </remarks>
    public sealed record Omap
    {
        /// <summary>
        /// Parses OMAP data from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader positioned at the OMAP data.</param>
        /// <param name="size">The size of the OMAP data in bytes.</param>
        /// <returns>An ImageDebugOmapData instance, or null if the data is invalid.</returns>
        internal static ReadOnlyCollection<Omap>? Parse(BinaryReader reader, uint size)
        {
            if (size < EntrySize)
            {
                return null;
            }
            int entryCount = (int)(size / EntrySize);
            List<Omap> entries = new(entryCount);
            for (int i = 0; i < entryCount; i++)
            {
                entries.Add(new(in PortableExecutableUtilities.ReadStruct<OMAP>(reader)));
            }
            return new(entries);
        }

        /// <summary>
        /// Initializes a new instance of the ImageDebugOmapEntry class.
        /// </summary>
        private Omap(in OMAP omap)
        {
            _omap = omap;
        }

        /// <summary>
        /// Gets the source relative virtual address.
        /// </summary>
        public uint Rva => _omap.rva;

        /// <summary>
        /// Gets the target relative virtual address.
        /// </summary>
        /// <remarks>
        /// A value of 0 indicates the RVA was removed during optimization.
        /// </remarks>
        public uint RvaTo => _omap.rvaTo;

        /// <summary>
        /// Represents the object that maps the original address space to the new address space.
        /// </summary>
        private readonly OMAP _omap;

        /// <summary>
        /// The size of each OMAP entry in bytes.
        /// </summary>
        private static readonly int EntrySize = Unsafe.SizeOf<OMAP>();
    }
}
