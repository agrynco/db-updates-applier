namespace DbVersioning
{
    public abstract class BaseDbUpdateBuilder<TDbUpdate> : IDbUpdateBuilder
        where TDbUpdate : IDbUpdate
    {
        IDbUpdate IDbUpdateBuilder.Build(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, IDbUpdateLoader dbUpdateLoader)
        {
            return Build(dbUpdateSourceDescriptor, dbUpdateLoader);
        }

        public abstract TDbUpdate Build(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, IDbUpdateLoader dbUpdateLoader);
    }
}