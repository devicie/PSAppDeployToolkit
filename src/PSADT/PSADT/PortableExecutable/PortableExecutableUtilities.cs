using System;
using System.IO;
using System.Runtime.InteropServices;
using PSADT.Interop.Extensions;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Provides shared utility methods for PE file parsing.
    /// </summary>
    internal static class PortableExecutableUtilities
    {
        /// <summary>
        /// Reads a structure from the binary reader.
        /// </summary>
        /// <typeparam name="T">The unmanaged structure type to read.</typeparam>
        /// <param name="reader">The binary reader to read from.</param>
        /// <returns>A reference to the read structure.</returns>
        internal static ref readonly T ReadStruct<T>(BinaryReader reader) where T : unmanaged
        {
            return ref reader.ReadBytes(Marshal.SizeOf<T>()).AsSpan().AsReadOnlyStructure<T>();
        }
    }
}
