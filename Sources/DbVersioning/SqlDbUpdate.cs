#region Usings
#endregion

namespace DbVersioning
{
    public class SqlDbUpdate<TDbVersionIdentifier> : BaseSqlDbUpdate<TDbVersionIdentifier> where TDbVersionIdentifier : IDbVersionIdentifier
    {
        #region Constructors
        public SqlDbUpdate(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string body, TDbVersionIdentifier expectedDbVersion, TDbVersionIdentifier newDbVersion)
            : base(dbUpdateSourceDescriptor, body, expectedDbVersion, newDbVersion)
        {
        }
        #endregion
    }
}