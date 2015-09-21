namespace DbVersioning
{
    public interface IDbUpdateBuilder
    {
        IDbUpdate Build(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, IDbUpdateLoader dbUpdateLoader);
    }
}