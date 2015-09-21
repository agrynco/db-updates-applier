namespace DbVersioning
{
    public class NumericSqlDbUpdateBuilder 
        : SqlDbUpdateBuilder<NumericDbVersionIdentifier, NewNumericDbVersionDetector, ExpectedNumericDbVersionDetector, FileSystemDbUpdateLoader>
    {
        #region Methods (protected)
        public override SqlDbUpdate<NumericDbVersionIdentifier> Build(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, FileSystemDbUpdateLoader dbUpdateLoader)
        {
            string content = dbUpdateLoader.Load(dbUpdateSourceDescriptor);

            NumericDbVersionIdentifier newDbVersion = (new NewNumericDbVersionDetector()).Detect(dbUpdateSourceDescriptor, content);
            NumericDbVersionIdentifier expectedDbVersion = new NumericDbVersionIdentifier(newDbVersion.Number - 1);

            return new SqlDbUpdate<NumericDbVersionIdentifier>(dbUpdateSourceDescriptor, content, expectedDbVersion, newDbVersion);
        }
        #endregion
    }
}