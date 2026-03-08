using System;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a bound import forwarder reference.
    /// </summary>
    public sealed record ImageBoundForwarderRef
    {
        /// <summary>
        /// Initializes a new instance of the BoundForwarderRef class with the specified forwarder reference and DLL name.
        /// </summary>
        /// <param name="forwarderRef">The IMAGE_BOUND_FORWARDER_REF structure that provides information about the forwarder reference.</param>
        /// <param name="moduleName">The name of the DLL associated with the bound forwarder.</param>
        internal ImageBoundForwarderRef(in Windows.Win32.System.SystemServices.IMAGE_BOUND_FORWARDER_REF forwarderRef, string moduleName)
        {
            ForwarderRef = forwarderRef;
            ModuleName = moduleName;
        }

        /// <summary>
        /// Gets the timestamp of the forwarded DLL, or null if the timestamp is zero.
        /// </summary>
        public DateTime? TimeDateStamp => ForwarderRef.TimeDateStamp > 0
            ? DateTimeOffset.FromUnixTimeSeconds(ForwarderRef.TimeDateStamp).UtcDateTime
            : null;

        /// <summary>
        /// Gets the name of the forwarded DLL.
        /// </summary>
        public string ModuleName { get; }

        /// <summary>
        /// Gets the raw IMAGE_BOUND_FORWARDER_REF structure.
        /// </summary>
        private readonly Windows.Win32.System.SystemServices.IMAGE_BOUND_FORWARDER_REF ForwarderRef;
    }
}
