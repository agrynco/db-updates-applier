namespace Lib.Data.DbVersioning
{
    public class NumericSqlDbUpdateBuilder : SqlDbUpdateBuilder<NumericDbVersionIdentifier, NewNumericDbVersionDetector, ExpectedNumericDbVersionDetector>
    {
        #region Methods (protected)
        protected override SqlDbUpdate<NumericDbVersionIdentifier> DoBuild(string fullSourceName, string content)
        {
            NumericDbVersionIdentifier newDbVersion = (new NewNumericDbVersionDetector()).Detect(fullSourceName, content);
            NumericDbVersionIdentifier expectedDbVersion = new NumericDbVersionIdentifier(newDbVersion.Number - 1);

            return new SqlDbUpdate<NumericDbVersionIdentifier>(fullSourceName, content, expectedDbVersion, newDbVersion);
        }
        #endregion
    }
}