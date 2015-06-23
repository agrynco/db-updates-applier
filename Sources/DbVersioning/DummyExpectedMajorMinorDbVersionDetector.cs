namespace Lib.Data.DbVersioning
{
    public class DummyExpectedMajorMinorDbVersionDetector : SqlDbVersionDetectorBase<MajorMinorDbVersionIdentifier>
    {
        #region Methods (public)
        public override MajorMinorDbVersionIdentifier Detect(string fullSourceName, string content)
        {
            return null;
        }
        #endregion
    }
}