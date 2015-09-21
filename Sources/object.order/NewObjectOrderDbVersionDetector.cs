using DbVersioning;

namespace @object.order
{
    public class NewObjectOrderDbVersionDetector : SqlDbVersionDetectorBase<ObjectOrderDbVersionIdentifier>
    {
        public override ObjectOrderDbVersionIdentifier Detect(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string content)
        {
            throw new System.NotImplementedException();
        }
    }
}