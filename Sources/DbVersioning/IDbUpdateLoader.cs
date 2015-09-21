namespace DbVersioning
{
    public interface IDbUpdateLoader
    {
        #region Abstract Methods
        string Load(DbUpdateSourceDescriptor dbUpdateSourceDescriptor);
        #endregion
    }

    public interface IDbUpdateLoader<TDbUpdateSourceDescriptor> : IDbUpdateLoader
        where TDbUpdateSourceDescriptor : DbUpdateSourceDescriptor
    {
        string Load(TDbUpdateSourceDescriptor dbUpdateSourceDescriptor);
    }
}