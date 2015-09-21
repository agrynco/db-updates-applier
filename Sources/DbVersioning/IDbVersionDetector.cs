namespace DbVersioning
{
    public interface IDbVersionDetector
    {
        #region Abstract Methods
        IDbVersionIdentifier Detect(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string content);
        #endregion
    }
}