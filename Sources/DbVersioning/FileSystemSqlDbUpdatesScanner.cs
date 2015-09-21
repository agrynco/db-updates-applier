#region Usings
using System.IO;
#endregion

namespace DbVersioning
{
    /// <summary>
    /// Scanner of <see cref="SqlDbUpdate{TDbVersionIdentifier}"/> in some directory (<see cref="GetUpdates"/>)
    /// </summary>
    public class FileSystemSqlDbUpdatesScanner : IDBUpdatesScanner
    {
        #region Fields (private)
        private readonly string _pathToUpdates;
        #endregion

        #region Constructors
        public FileSystemSqlDbUpdatesScanner(string pathToUpdates)
        {
            _pathToUpdates = pathToUpdates;
        }
        #endregion

        protected string PathToUpdates
        {
            get { return _pathToUpdates; }
        }

        #region IDBUpdatesScanner Methods
        public virtual string[] GetUpdates()
        {
            return Directory.GetFiles(PathToUpdates, "*.sql");
        }
        #endregion
    }
}