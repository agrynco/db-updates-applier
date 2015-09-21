#region Usings
using DbVersioning;
#endregion

namespace @object.order
{
    public class ObjectOrderSqlDbUpdateBuilder
        : SqlDbUpdateBuilder<ObjectOrderDbVersionIdentifier, NewObjectOrderDbVersionDetector, ExpectedObjectOrderDbVersionDetector>
    {
    }
}