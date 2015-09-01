#region Usings
using AGrynCo.Lib.Data.DataProviders;
#endregion

namespace DbVersioning
{
    public interface IDbUpdateExecutor
    {
        #region Abstract Methods
        UpdateDbExecutionResult Execute(IDbVersionIdentifier currentDbVersionIdentifier, IDbUpdate dbUpdate);
        #endregion

        #region Properties (public)
        int? ExecutionTimeOut { get; set; }

        IDataProvider DataProvider { get; }
        #endregion
    }
}