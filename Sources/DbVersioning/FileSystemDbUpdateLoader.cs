#region Usings
using System;
using System.IO;
using DbVersioning.Exceptions;
#endregion

namespace DbVersioning
{
    public class FileSystemDbUpdateLoader : IDbUpdateLoader<DbUpdateSourceDescriptor>
    {
        #region IDbUpdateLoader Methods
        public string Load(DbUpdateSourceDescriptor dbUpdateSourceDescriptor)
        {
            try
            {
                return File.ReadAllText(dbUpdateSourceDescriptor.Path);
            }
            catch (Exception ex)
            {
                throw new SqlUpdatesSourceLoadException(dbUpdateSourceDescriptor, ex);
            }
        }
        #endregion
    }
}