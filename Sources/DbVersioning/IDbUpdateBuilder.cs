namespace DbVersioning
{
    public interface IDbUpdateBuilder
    {
        IDbUpdate Build(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, IDbUpdateLoader dbUpdateLoader);
    }

    public interface IDbUpdateBuilder<TDbUpdate, TDbUpdateLoader> : IDbUpdateBuilder
        where TDbUpdate : IDbUpdate 
        where TDbUpdateLoader : IDbUpdateLoader
    {
        #region Abstract Methods
        TDbUpdate Build(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, TDbUpdateLoader dbUpdateLoader);
        #endregion
    }
}