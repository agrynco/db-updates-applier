#region Usings
using System;
using Lib.Data.DataProviders;
using Lib.Utils.ObjUtils;
#endregion

namespace Lib.Data.DbVersioning
{
    public class DbUpdateSourceDefinition : BaseClass
    {
        #region Constructors
        public DbUpdateSourceDefinition(Type typeOfDbUpdate,
            ICurrentDbVersionDetector currentDbVersionDetector,
            IDBUpdatesScanner dbUpdatesScanner,
            IDbUpdateLoader dbUpdateLoader,
            IDbUpdateBuilder dbUpdateBuilder,
            IDbUpdateExecutor dbUpdateExecutor,
            IDatabaseManager databaseManager)
        {
            DbUpdatesScanner = dbUpdatesScanner;
            DbUpdateLoader = dbUpdateLoader;
            DbUpdateBuilder = dbUpdateBuilder;
            DbUpdateExecutor = dbUpdateExecutor;
            DatabaseManager = databaseManager;

            TypeOfDbUpdate = typeOfDbUpdate;
            CurrentDbVersionDetector = currentDbVersionDetector;
        }
        #endregion

        #region Properties (public)
        public ICurrentDbVersionDetector CurrentDbVersionDetector { get; private set; }
        public IDbUpdateBuilder DbUpdateBuilder { get; private set; }
        public IDbUpdateExecutor DbUpdateExecutor { get; private set; }
        public IDbUpdateLoader DbUpdateLoader { get; private set; }
        public IDBUpdatesScanner DbUpdatesScanner { get; private set; }
        public Type TypeOfDbUpdate { get; private set; }
        public IDatabaseManager DatabaseManager { get; private set; }
        #endregion
    }
}