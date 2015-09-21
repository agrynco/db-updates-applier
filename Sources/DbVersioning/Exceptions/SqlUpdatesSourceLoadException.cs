#region Usings
using System;
#endregion

namespace DbVersioning.Exceptions
{
    public class SqlUpdatesSourceLoadException : DbVersioningException
    {
        #region Constructors
        public SqlUpdatesSourceLoadException(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, Exception innerException)
            : base(string.Format("Could not load update script from '{0}'", dbUpdateSourceDescriptor), innerException)
        {
        }
        #endregion
    }
}