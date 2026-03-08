using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Win32.System.SystemServices;

namespace PSADT.PortableExecutable
{
    /// <summary>
    /// Represents a resource directory node identified by numeric ID.
    /// </summary>
    public sealed record ImageResourceIdDirectory : ImageResourceIdNode
    {
        /// <summary>
        /// Initializes a new instance of the ImageResourceIdDirectory class.
        /// </summary>
        /// <param name="directory">The IMAGE_RESOURCE_DIRECTORY structure.</param>
        /// <param name="namedEntries">The collection of child entries identified by name.</param>
        /// <param name="idEntries">The collection of child entries identified by ID.</param>
        /// <param name="id">The numeric ID that identifies this directory.</param>
        internal ImageResourceIdDirectory(in IMAGE_RESOURCE_DIRECTORY directory, ReadOnlyCollection<ImageResourceNamedNode> namedEntries, ReadOnlyCollection<ImageResourceIdNode> idEntries, uint id) : base(isDirectory: true, id)
        {
            Directory = directory;
            NamedEntries = namedEntries;
            IdEntries = idEntries;
        }

        /// <summary>
        /// Gets the time/date stamp of the resource data.
        /// </summary>
        public DateTime? TimeDateStamp => Directory.TimeDateStamp > 0
            ? DateTimeOffset.FromUnixTimeSeconds(Directory.TimeDateStamp).UtcDateTime
            : null;

        /// <summary>
        /// Gets the version of this resource directory.
        /// </summary>
        public Version Version => new(Directory.MajorVersion, Directory.MinorVersion);

        /// <summary>
        /// Gets the child entries identified by name.
        /// </summary>
        public IReadOnlyList<ImageResourceNamedNode> NamedEntries { get; }

        /// <summary>
        /// Gets the child entries identified by numeric ID.
        /// </summary>
        public IReadOnlyList<ImageResourceIdNode> IdEntries { get; }

        /// <summary>
        /// The underlying IMAGE_RESOURCE_DIRECTORY structure.
        /// </summary>
        private readonly IMAGE_RESOURCE_DIRECTORY Directory;
    }
}
