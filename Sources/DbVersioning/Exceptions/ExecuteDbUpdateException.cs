#region Usings
using System;
#endregion

namespace DbVersioning.Exceptions
{
    public class ExecuteDbUpdateException : DbVersioningException
    {
        #region Constructors
        public ExecuteDbUpdateException(string message, IDbVersionIdentifier dbVersion, IDbUpdate executedDbUpdate) : base(message)
        {
            DbVersion = dbVersion;
            ExecutedDbUpdate = executedDbUpdate;
        }

        public ExecuteDbUpdateException(string message, Exception innerException, IDbVersionIdentifier dbVersion, IDbUpdate executedDbUpdate) : base(message, innerException)
        {
            DbVersion = dbVersion;
            ExecutedDbUpdate = executedDbUpdate;
        }
        #endregion

        #region Properties (public)
        public IDbVersionIdentifier DbVersion { get; private set; }
        public IDbUpdate ExecutedDbUpdate { get; private set; }
        #endregion
    }
}