#region Usings
using System;
#endregion

namespace DbVersioning
{
    public class EmbeddedResourceDbUpdateSourceDescriptor : DbUpdateSourceDescriptor
    {
        public EmbeddedResourceDbUpdateSourceDescriptor(string path, Type asseblyTypeIdentifier) : base(path)
        {
            AsseblyTypeIdentifier = asseblyTypeIdentifier;
        }

        public Type AsseblyTypeIdentifier { get; set; }
    }
}