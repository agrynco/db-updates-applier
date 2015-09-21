using System.IO;

namespace @object.order
{
    public class ObjectOrderFileSystemSqlDbUpdatesScanner : DbVersioning.FileSystemSqlDbUpdatesScanner
    {
        public ObjectOrderFileSystemSqlDbUpdatesScanner(string pathToUpdates) : base(pathToUpdates)
        {
        }

        public override string[] GetUpdates()
        {
            return Directory.GetFiles(PathToUpdates, "*.sql", SearchOption.AllDirectories);
        }
    }
}