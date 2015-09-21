namespace DbVersioning
{
    public class DummyExpectedMajorMinorDbVersionDetector : SqlDbVersionDetectorBase<MajorMinorDbVersionIdentifier>
    {
        #region Methods (public)
        public override MajorMinorDbVersionIdentifier Detect(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string content)
        {
            return null;
        }
        #endregion
    }
}