using System.IO;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents VC_FEATURE debug information from an IMAGE_DEBUG_TYPE_VC_FEATURE entry.
    /// </summary>
    /// <remarks>
    /// VC_FEATURE data contains counts of objects compiled with various Visual C++ compiler features.
    /// This information is useful for understanding the security features enabled during compilation.
    /// The structure contains five 32-bit counts (20 bytes total).
    /// </remarks>
    public sealed record ImageDebugVcFeatureEntry
    {
        /// <summary>
        /// Parses VC_FEATURE data from the given binary reader.
        /// </summary>
        /// <param name="reader">The binary reader positioned at the VC_FEATURE data.</param>
        /// <param name="size">The size of the VC_FEATURE data in bytes.</param>
        /// <returns>An ImageDebugVcFeature instance, or null if the data is invalid.</returns>
        internal static ImageDebugVcFeatureEntry? Parse(BinaryReader reader, uint size)
        {
            // VC_FEATURE structure contains exactly 5 uint values (20 bytes)
            if (size < ExpectedSize)
            {
                return null;
            }
            uint preVCPlusPlusCount = reader.ReadUInt32();
            uint cAndCPlusPlusCount = reader.ReadUInt32();
            uint guardStackCount = reader.ReadUInt32();
            uint sdlCount = reader.ReadUInt32();
            uint guardCount = reader.ReadUInt32();
            return new(preVCPlusPlusCount, cAndCPlusPlusCount, guardStackCount, sdlCount, guardCount);
        }

        /// <summary>
        /// Initializes a new instance of the ImageDebugVcFeature class.
        /// </summary>
        private ImageDebugVcFeatureEntry(uint preVCPlusPlusCount, uint cAndCPlusPlusCount, uint guardStackCount, uint sdlCount, uint guardCount)
        {
            PreVCPlusPlusCount = preVCPlusPlusCount;
            CAndCPlusPlusCount = cAndCPlusPlusCount;
            GuardStackCount = guardStackCount;
            SdlCount = sdlCount;
            GuardCount = guardCount;
        }

        /// <summary>
        /// Gets the count of objects compiled with pre-Visual C++ 11 (VS 2012) compilers.
        /// </summary>
        /// <remarks>
        /// Objects compiled with older compilers may not support modern security features.
        /// </remarks>
        public uint PreVCPlusPlusCount { get; }

        /// <summary>
        /// Gets the count of C/C++ objects linked into the image.
        /// </summary>
        public uint CAndCPlusPlusCount { get; }

        /// <summary>
        /// Gets the count of objects compiled with /GS (buffer security check).
        /// </summary>
        /// <remarks>
        /// The /GS compiler flag enables stack buffer overrun detection.
        /// </remarks>
        public uint GuardStackCount { get; }

        /// <summary>
        /// Gets the count of objects compiled with /sdl (additional security checks).
        /// </summary>
        /// <remarks>
        /// The /sdl flag enables additional Security Development Lifecycle checks
        /// including stricter /GS and additional warnings as errors.
        /// </remarks>
        public uint SdlCount { get; }

        /// <summary>
        /// Gets the count of objects compiled with Control Flow Guard (CFG) support.
        /// </summary>
        /// <remarks>
        /// CFG is a security feature that helps prevent memory corruption vulnerabilities
        /// by validating indirect call targets at runtime.
        /// </remarks>
        public uint GuardCount { get; }

        /// <summary>
        /// The expected size of the VC_FEATURE data in bytes.
        /// </summary>
        private const int ExpectedSize = 20;
    }
}
