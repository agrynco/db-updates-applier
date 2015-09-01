#region Usings
using System;
using AGrynCo.Lib.Data.DataProviders;
using DbVersioning.Exceptions;
#endregion

namespace DbVersioning
{
    public class SqlDbUpdateExecutor<TDbVersionIdentifier> : IDbUpdateExecutor
        where TDbVersionIdentifier : IDbVersionIdentifier
    {
        #region Fields (private)
        private readonly IDataProvider _dataProvider;
        #endregion

        #region Constructors
        public SqlDbUpdateExecutor(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        #endregion

        #region Properties (public)
        public IDataProvider DataProvider
        {
            get { return _dataProvider; }
        }
        #endregion

        #region IDbUpdateExecutor Properties
        public int? ExecutionTimeOut { get; set; }
        #endregion

        #region IDbUpdateExecutor Methods
        public UpdateDbExecutionResult Execute(IDbVersionIdentifier currentDbVersionIdentifier, IDbUpdate dbUpdate)
        {
            return Execute((TDbVersionIdentifier) currentDbVersionIdentifier, (BaseSqlDbUpdate<TDbVersionIdentifier>) dbUpdate);
        }
        #endregion

        #region Methods (public)
        public virtual UpdateDbExecutionResult Execute(TDbVersionIdentifier currentDbVersionIdentifier, BaseSqlDbUpdate<TDbVersionIdentifier> dbUpdate)
        {
            BaseSqlDbUpdate<TDbVersionIdentifier> dbUpdateToBeExecuted = dbUpdate;

            string currentDataBase = _dataProvider.Connection.Database;
            if (dbUpdate.IsInitial)
            {
                dbUpdateToBeExecuted = PrepareInitialDbUpdate(dbUpdate);

                _dataProvider.SwitchToDataBase("master");
            }

            try
            {
                if (ExecutionTimeOut.HasValue)
                {
                    _dataProvider.ExecuteBatch(dbUpdateToBeExecuted.Content, ExecutionTimeOut.Value);
                }
                else
                {
                    _dataProvider.ExecuteBatch(dbUpdateToBeExecuted.Content);
                }

                return new UpdateDbExecutionResult(currentDbVersionIdentifier, dbUpdateToBeExecuted);
            }
            catch (Exception ex)
            {
                throw new ExecuteDbUpdateException("Could not apply update", ex, currentDbVersionIdentifier, dbUpdateToBeExecuted);
            }
            finally
            {
                _dataProvider.CloseConnection();
                if (dbUpdate.IsInitial)
                {
                    _dataProvider.SwitchToDataBase(currentDataBase);
                }
            }
        }
        #endregion

        #region Methods (private)
        private BaseSqlDbUpdate<TDbVersionIdentifier> PrepareInitialDbUpdate(BaseSqlDbUpdate<TDbVersionIdentifier> dbUpdate)
        {
            return new BaseSqlDbUpdate<TDbVersionIdentifier>(
                dbUpdate.FullName,
                dbUpdate.Content.Replace("{DB_NAME}", _dataProvider.Connection.Database),
                dbUpdate.ExpectedDbVersion,
                dbUpdate.NewDbVersion);
        }
        #endregion
    }
}