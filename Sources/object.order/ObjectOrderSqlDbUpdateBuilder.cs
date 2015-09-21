using DbVersioning;

namespace @object.order
{
    public class ObjectOrderSqlDbUpdateBuilder 
        : SqlDbUpdateBuilder<ObjectOrderDbVersionIdentifier, NewObjectOrderDbVersionDetector, ExpectedObjectOrderDbVersionDetector,
        FileSystemDbUpdateLoader>
    {
    }
}