#region Usings
using System;
#endregion

namespace Lib.Data.DbVersioning.Exceptions
{
    public class SqlUpdatesSourceLoadException : DbVersioningException
    {
        #region Constructors
        public SqlUpdatesSourceLoadException(string fullSourceName, Exception innerException)
            : base(string.Format("Could not load update script from '{0}'", fullSourceName), innerException)
        {
        }
        #endregion
    }
}