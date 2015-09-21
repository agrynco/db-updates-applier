#region Usings
using DbVersioning;
#endregion

namespace @object.order
{
    public class ObjectOrderSqlDbUpdate : SqlDbUpdate<ObjectOrderDbVersionIdentifier>
    {
        public ObjectOrderSqlDbUpdate(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string body, ObjectOrderDbVersionIdentifier expectedDbVersion, ObjectOrderDbVersionIdentifier newDbVersion)
            : base(dbUpdateSourceDescriptor, body, expectedDbVersion, newDbVersion)
        {
        }
    }
}