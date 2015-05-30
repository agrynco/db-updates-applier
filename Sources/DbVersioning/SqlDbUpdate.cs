#region Usings
#endregion

namespace Lib.Data.DbVersioning
{
    public class SqlDbUpdate<TDbVersionIdentifier> : BaseSqlDbUpdate<TDbVersionIdentifier> where TDbVersionIdentifier : IDbVersionIdentifier
    {
        #region Constructors
        public SqlDbUpdate(string fullName, string body, TDbVersionIdentifier expectedDbVersion, TDbVersionIdentifier newDbVersion)
            : base(fullName, body, expectedDbVersion, newDbVersion)
        {
        }
        #endregion
    }
}