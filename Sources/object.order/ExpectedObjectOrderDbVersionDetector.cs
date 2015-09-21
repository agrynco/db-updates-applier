using DbVersioning;

namespace @object.order
{
    public class ExpectedObjectOrderDbVersionDetector : SqlDbVersionDetectorBase<ObjectOrderDbVersionIdentifier>
    {
        public override ObjectOrderDbVersionIdentifier Detect(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string content)
        {
            throw new System.NotImplementedException();
        }
    }
}