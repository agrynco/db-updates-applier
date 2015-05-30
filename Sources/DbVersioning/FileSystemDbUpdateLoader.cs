#region Usings
using System;
using System.IO;
using Lib.Data.DbVersioning.Exceptions;
#endregion

namespace Lib.Data.DbVersioning
{
    public class FileSystemDbUpdateLoader : IDbUpdateLoader
    {
        #region IDbUpdateLoader Methods
        public string Load(string fullSourceName)
        {
            try
            {
                return File.ReadAllText(fullSourceName);
            }
            catch (Exception ex)
            {
                throw new SqlUpdatesSourceLoadException(fullSourceName, ex);
            }
        }
        #endregion
    }
}