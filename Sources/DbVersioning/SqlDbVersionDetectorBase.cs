namespace DbVersioning
{
    public abstract class SqlDbVersionDetectorBase<TDbVersionIdentifier> : IDbVersionDetector where TDbVersionIdentifier : IDbVersionIdentifier
    {
        #region IDbVersionDetector Methods
        IDbVersionIdentifier IDbVersionDetector.Detect(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string content)
        {
            return Detect(dbUpdateSourceDescriptor, content);
        }
        #endregion

        #region Abstract Methods
        public abstract TDbVersionIdentifier Detect(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string content);
        #endregion
    }
}