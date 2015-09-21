namespace DbVersioning
{
    public abstract class BaseDbUpdateBuilder<TDbUpdate, TDbUpdateLoader> : IDbUpdateBuilder<TDbUpdate, TDbUpdateLoader>
        where TDbUpdate : IDbUpdate
        where TDbUpdateLoader : IDbUpdateLoader
    {
        public abstract TDbUpdate Build(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, TDbUpdateLoader dbUpdateLoader);

        IDbUpdate IDbUpdateBuilder.Build(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, IDbUpdateLoader dbUpdateLoader)
        {
            return Build(dbUpdateSourceDescriptor, (TDbUpdateLoader)dbUpdateLoader);
        }
    }
}