#region Usings
using System.IO;
#endregion

namespace Lib.Data.DbVersioning
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

        #region IDBUpdatesScanner Methods
        public string[] GetUpdates()
        {
            return Directory.GetFiles(_pathToUpdates, "*.sql");
        }
        #endregion
    }
}