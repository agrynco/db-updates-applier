namespace DbVersioning
{
    public abstract class SqlDbVersionDetectorBase<TDbVersionIdentifier> : IDbVersionDetector where TDbVersionIdentifier : IDbVersionIdentifier
    {
        #region IDbVersionDetector Methods
        IDbVersionIdentifier IDbVersionDetector.Detect(string fullSourceName, string content)
        {
            return Detect(fullSourceName, content);
        }
        #endregion

        #region Abstract Methods
        public abstract TDbVersionIdentifier Detect(string fullSourceName, string content);
        #endregion
    }
}