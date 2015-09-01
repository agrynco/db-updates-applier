namespace DbVersioning
{
    public class SqlDbUpdateBuilder<TDbVersionIdentifier, TNewDbVersionDetector, TExpectedDbVersionDetector> : BaseDbUpdateBuilder<SqlDbUpdate<TDbVersionIdentifier>>
        where TDbVersionIdentifier : IDbVersionIdentifier
        where TNewDbVersionDetector : SqlDbVersionDetectorBase<TDbVersionIdentifier>, new()
        where TExpectedDbVersionDetector : SqlDbVersionDetectorBase<TDbVersionIdentifier>, new()
    {
        #region Methods (protected)
        protected override SqlDbUpdate<TDbVersionIdentifier> DoBuild(string fullSourceName, string content)
        {
            TDbVersionIdentifier newDbVersion = (new TNewDbVersionDetector()).Detect(fullSourceName, content);
            TDbVersionIdentifier expectedDbVersion = (new TExpectedDbVersionDetector()).Detect(fullSourceName, content);

            return new SqlDbUpdate<TDbVersionIdentifier>(fullSourceName, content, expectedDbVersion, newDbVersion);
        }
        #endregion
    }
}