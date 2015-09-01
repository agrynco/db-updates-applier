namespace DbVersioning
{
    public class UpdateDbExecutionResult
    {
        #region Constructors
        public UpdateDbExecutionResult(IDbVersionIdentifier dbVersion, IDbUpdate executedDbUpdate)
        {
            DbVersion = dbVersion;
            ExecutedDbUpdate = executedDbUpdate;
        }
        #endregion

        #region Properties (public)
        public IDbVersionIdentifier DbVersion { get; private set; }
        public IDbUpdate ExecutedDbUpdate { get; private set; }
        public string ExecutionNote { get; set; }
        #endregion
    }
}