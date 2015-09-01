namespace DbVersioning
{
    public abstract class BaseDbUpdateBuilder<TDbUpdate> : IDbUpdateBuilder<TDbUpdate>
        where TDbUpdate : IDbUpdate
    {
        #region Abstract Methods
        protected abstract TDbUpdate DoBuild(string fullSourceName, string content);
        #endregion

        #region IDbUpdateBuilder<TDbUpdate> Methods
        public TDbUpdate Build(string fullSourceName, string content)
        {
            return DoBuild(fullSourceName, content);
        }

        IDbUpdate IDbUpdateBuilder.Build(string fullSourceName, string content)
        {
            return Build(fullSourceName, content);
        }
        #endregion
    }
}