namespace DbVersioning
{
    public interface IDbUpdateBuilder
    {
        #region Abstract Methods
        IDbUpdate Build(string fullSourceName, string content);
        #endregion
    }

    public interface IDbUpdateBuilder<TDbUpdate> : IDbUpdateBuilder
        where TDbUpdate : IDbUpdate
    {
        #region Abstract Methods
        new TDbUpdate Build(string fullSourceName, string content);
        #endregion
    }
}