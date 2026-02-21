using System;
using System.IO;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents REPRO (Reproducible Build) debug information from an IMAGE_DEBUG_TYPE_REPRO entry.
    /// </summary>
    /// <remarks>
    /// REPRO data is present when a PE file is compiled with reproducible build options:
    /// <list type="bullet">
    /// <item>MSVC: /Brepro linker flag - stores length-prefixed hash</item>
    /// <item>Clang/LLD: /Brepro or --build-id - stores raw hash bytes</item>
    /// </list>
    /// The hash replaces the timestamp in the PE header for deterministic builds.
    /// </remarks>
    public sealed record ImageDebugReproEntry
    {
        /// <summary>
        /// Parses REPRO data from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader positioned at the REPRO data.</param>
        /// <param name="size">The size of the REPRO data in bytes.</param>
        /// <returns>An ImageDebugRepro instance, or null if the data is invalid.</returns>
        internal static ImageDebugReproEntry? Parse(BinaryReader reader, uint size)
        {
            // REPRO has two formats depending on the toolchain:
            //
            // MSVC (/Brepro):
            //   - 4-byte length prefix followed by hash bytes
            //   - SizeOfData = length + 4
            //
            // Clang/LLD (/Brepro or --build-id):
            //   - Raw hash bytes with no length prefix
            //   - SizeOfData = hash length

            // If there's no size, this just indicates reproducible build was enabled.
            if (size == 0)
            {
                return new([]);
            }

            // If the size is too small for MSVC format, treat as raw hash (LLD format).
            if (size < 4)
            {
                return new(reader.ReadBytes((int)size));
            }

            // Read potential length prefix (MSVC format)
            uint potentialLength = reader.ReadUInt32();

            // Heuristic: If length + 4 == size, it's MSVC format with length prefix
            // Otherwise, treat entire data (including the 4 bytes we just read) as raw hash (LLD format)
            if (potentialLength + 4 == size)
            {
                // MSVC format: length prefix followed by hash
                byte[] hash = reader.ReadBytes((int)potentialLength);
                return new(hash);
            }
            else
            {
                // LLD format: raw hash bytes (need to include the 4 bytes we already read)
                reader.BaseStream.Position -= 4; byte[] hash = reader.ReadBytes((int)size);
                return new(hash);
            }
        }

        /// <summary>
        /// Initializes a new instance of the ImageDebugRepro class.
        /// </summary>
        private ImageDebugReproEntry(byte[] hash)
        {
            _hash = hash;
        }

        /// <summary>
        /// Gets a value indicating whether a hash is present.
        /// </summary>
        public bool HasHash => _hash.Length > 0;

        /// <summary>
        /// Gets the reproducibility hash bytes.
        /// </summary>
        /// <remarks>
        /// This hash is used to verify that builds are reproducible. An empty span indicates
        /// that /Brepro was used but no hash data was stored.
        /// </remarks>
        public ReadOnlySpan<byte> Hash => _hash;

        /// <summary>
        /// Gets the hash as a hexadecimal string.
        /// </summary>
        /// <returns>The hash in uppercase hexadecimal format, or an empty string if no hash is present.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1872:Prefer 'Convert.ToHexString' and 'Convert.ToHexStringLower' over call chains based on 'BitConverter.ToString'", Justification = "We need to suppress this while we're dual-targeting.")]
        public string GetHashString()
        {
            return _hash.Length > 0 ? BitConverter.ToString(_hash).Replace("-", null) : string.Empty;
        }

        /// <summary>
        /// Stores the computed hash value as a byte array.
        /// </summary>
        private readonly byte[] _hash;
    }
}
