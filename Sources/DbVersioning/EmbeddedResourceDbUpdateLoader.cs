using AGrynCo.Lib.ResourcesUtils;

namespace DbVersioning
{
    public class EmbeddedResourceDbUpdateLoader : IDbUpdateLoader<EmbeddedResourceDbUpdateSourceDescriptor>
    {
        public string Load(EmbeddedResourceDbUpdateSourceDescriptor dbUpdateSourceDescriptor)
        {
            return ResourceReader.ReadAsString(dbUpdateSourceDescriptor.AsseblyTypeIdentifier, dbUpdateSourceDescriptor.Path);
        }

        string IDbUpdateLoader.Load(DbUpdateSourceDescriptor dbUpdateSourceDescriptor)
        {
            return Load((EmbeddedResourceDbUpdateSourceDescriptor) dbUpdateSourceDescriptor);
        }
    }
}