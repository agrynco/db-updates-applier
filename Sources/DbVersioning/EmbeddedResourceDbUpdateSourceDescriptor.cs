#region Usings
using System;
#endregion

namespace DbVersioning
{
    public class EmbeddedResourceDbUpdateSourceDescriptor : DbUpdateSourceDescriptor
    {
        public EmbeddedResourceDbUpdateSourceDescriptor(Type asseblyTypeIdentifier, string path) : base(path)
        {
            AsseblyTypeIdentifier = asseblyTypeIdentifier;
        }

        public Type AsseblyTypeIdentifier { get; set; }
    }
}