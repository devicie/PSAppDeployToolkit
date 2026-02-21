using System.IO;
using System.Runtime.CompilerServices;
using Windows.Win32.System.Diagnostics.Debug;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents COFF debug information from an IMAGE_DEBUG_TYPE_COFF entry.
    /// </summary>
    /// <remarks>
    /// COFF debug data contains an IMAGE_COFF_SYMBOLS_HEADER followed by symbol and line number data.
    /// This is a legacy debug format primarily used by older tools.
    /// </remarks>
    public sealed record ImageCoffSymbolsHeader
    {
        /// <summary>
        /// Parses COFF data from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader positioned at the COFF data.</param>
        /// <param name="size">The size of the COFF data in bytes.</param>
        /// <returns>An ImageCoffSymbolsHeader instance, or null if the data is invalid.</returns>
        internal static ImageCoffSymbolsHeader? Parse(BinaryReader reader, uint size)
        {
            return size >= HeaderSize
                ? new ImageCoffSymbolsHeader(in PortableExecutableUtilities.ReadStruct<IMAGE_COFF_SYMBOLS_HEADER>(reader))
                : null;
        }

        /// <summary>
        /// Initializes a new instance of the ImageCoffSymbolsHeader class.
        /// </summary>
        private ImageCoffSymbolsHeader(in IMAGE_COFF_SYMBOLS_HEADER header)
        {
            _header = header;
        }

        /// <summary>
        /// Gets the number of COFF symbols.
        /// </summary>
        public uint NumberOfSymbols => _header.NumberOfSymbols;

        /// <summary>
        /// Gets the offset (LVA) to the first COFF symbol.
        /// </summary>
        public uint LvaToFirstSymbol => _header.LvaToFirstSymbol;

        /// <summary>
        /// Gets the number of COFF line number entries.
        /// </summary>
        public uint NumberOfLinenumbers => _header.NumberOfLinenumbers;

        /// <summary>
        /// Gets the offset (LVA) to the first COFF line number entry.
        /// </summary>
        public uint LvaToFirstLinenumber => _header.LvaToFirstLinenumber;

        /// <summary>
        /// Gets the RVA of the first byte of code.
        /// </summary>
        public uint RvaToFirstByteOfCode => _header.RvaToFirstByteOfCode;

        /// <summary>
        /// Gets the RVA of the last byte of code.
        /// </summary>
        public uint RvaToLastByteOfCode => _header.RvaToLastByteOfCode;

        /// <summary>
        /// Gets the RVA of the first byte of data.
        /// </summary>
        public uint RvaToFirstByteOfData => _header.RvaToFirstByteOfData;

        /// <summary>
        /// Gets the RVA of the last byte of data.
        /// </summary>
        public uint RvaToLastByteOfData => _header.RvaToLastByteOfData;

        /// <summary>
        /// The underlying IMAGE_COFF_SYMBOLS_HEADER structure.
        /// </summary>
        private readonly IMAGE_COFF_SYMBOLS_HEADER _header;

        /// <summary>
        /// The size of the IMAGE_COFF_SYMBOLS_HEADER structure.
        /// </summary>
        private static readonly uint HeaderSize = (uint)Unsafe.SizeOf<IMAGE_COFF_SYMBOLS_HEADER>();
    }
}
