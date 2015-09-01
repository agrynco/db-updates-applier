namespace DbVersioning
{
    public interface IDbVersionDetector
    {
        #region Abstract Methods
        IDbVersionIdentifier Detect(string fullSourceName, string content);
        #endregion
    }
}