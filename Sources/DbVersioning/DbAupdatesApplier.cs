#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using AGrynCo.Lib.Validation;
using DbVersioning.Exceptions;
#endregion

namespace DbVersioning
{
    public delegate void OnExecuteUpdateDelegate(DbAupdatesApplier sender, UpdateDbExecutionResult result);

    public delegate void OnTryToExecuteUpdateDelegate(DbAupdatesApplier sender, IDbUpdate dbUpdate);

    public delegate void OnFailureUpdateDelegate(DbAupdatesApplier sender, ExecuteDbUpdateException ex);

    public delegate void OnDropDataBaseDelegate(DbAupdatesApplier sender, string dbName);

    public delegate void OnCleanUpDataBaseDelegate(DbAupdatesApplier sender, string dbName);

    public delegate void OnProcessDbUpdateSourceDefinitionDelegate(DbAupdatesApplier sender, DbUpdateSourceDefinition dbUpdateSourceDefinition);

    public class DbAupdatesApplier
    {
        #region Constructors
        public DbAupdatesApplier(IEnumerable<DbUpdateSourceDefinition> dbUpdateSourceDefinitions, bool fromScratch,
            bool tryCleanUp)
        {
            _dbUpdateSourceDefinitions = dbUpdateSourceDefinitions;
            _fromScratch = fromScratch;
            _tryCleanUp = tryCleanUp;
        }
        #endregion

        #region Fields (private)
        private readonly IEnumerable<DbUpdateSourceDefinition> _dbUpdateSourceDefinitions;

        private readonly bool _fromScratch;
        private readonly bool _tryCleanUp;
        #endregion

        #region Events (public)
        public event OnTryToExecuteUpdateDelegate OnBeforeExecuteUpdate;

        public event OnProcessDbUpdateSourceDefinitionDelegate OnBeginProcessDbUpdateSourceDefinition;

        public event OnProcessDbUpdateSourceDefinitionDelegate OnEndProcessDbUpdateSourceDefinition;

        public event OnExecuteUpdateDelegate OnExecutedUpdate;

        public event OnFailureUpdateDelegate OnFailureUpdate;

        public event OnDropDataBaseDelegate OnDropDataBase;

        public event OnCleanUpDataBaseDelegate OnCleanUpDataBase;
        #endregion

        #region Methods (public)
        public void Apply()
        {
            foreach (DbUpdateSourceDefinition dbUpdateSourceDefinition in _dbUpdateSourceDefinitions)
            {
                DoOnBeginProcessDbUpdateSourceDefinition(this, dbUpdateSourceDefinition);

                DbUpdateList dbUpdates = GetUpdates(dbUpdateSourceDefinition);

                ValidationResultList<DbUpdateList, DbUpdateValidationResult> dbUpdateValidationResults = DbUpdateListValidator.Validate(dbUpdates);
                if (dbUpdateValidationResults.Count > 0)
                {
                    throw new DbUpdatesValidationException("There are invalid updates", dbUpdateValidationResults);
                }

                if (_fromScratch)
                {
                    OnDropDataBase?.Invoke(this, dbUpdateSourceDefinition.DatabaseManager.TargetDataBaseName);

                    if (_tryCleanUp)
                    {
                        if (dbUpdateSourceDefinition.CurrentDbVersionDetector.CheckOnDbExists())
                        {
                            dbUpdateSourceDefinition.DatabaseManager.CleanUp();
                        }
                    }
                    else
                    {
                        dbUpdateSourceDefinition.DatabaseManager.Drop();
                    }
                }

                IDbVersionIdentifier currentDbVersionIdentifier;
                if (dbUpdateSourceDefinition.CurrentDbVersionDetector.CheckOnDbExists())
                {
                    currentDbVersionIdentifier = dbUpdateSourceDefinition.CurrentDbVersionDetector.Detect();
                    RemoveAlreadyAppliedUpdates(currentDbVersionIdentifier, dbUpdates);
                }
                else
                {
                    currentDbVersionIdentifier = dbUpdateSourceDefinition.CurrentDbVersionDetector.CreateZeroIdentifier();
                }

                dbUpdates.Sort(new DbUpdatesComparer());

                ApplyUpdates(dbUpdates, currentDbVersionIdentifier);

                DoOnEndProcessDbUpdateSourceDefinition(this, dbUpdateSourceDefinition);
            }
        }

        private void ApplyUpdates(DbUpdateList dbUpdates, IDbVersionIdentifier currentDbVersionIdentifier)
        {
            foreach (IDbUpdate dbUpdate in dbUpdates)
            {
                try
                {
                    OnBeforeExecuteUpdate?.Invoke(this, dbUpdate);

                    IDbUpdateExecutor dbUpdateExecutor = GetDbUpdateSourceDefinition(dbUpdate.GetType()).DbUpdateExecutor;
                    UpdateDbExecutionResult updateDbExecutionResult = dbUpdateExecutor.Execute(currentDbVersionIdentifier, dbUpdate);
                    OnExecutedUpdate?.Invoke(this, updateDbExecutionResult);
                }
                catch (ExecuteDbUpdateException ex)
                {
                    OnFailureUpdate?.Invoke(this, ex);

                    throw;
                }
            }
        }

        private static DbUpdateList GetUpdates(DbUpdateSourceDefinition dbUpdateSourceDefinition)
        {
            DbUpdateList dbUpdates = new DbUpdateList();

            string[] fullSourceNames = dbUpdateSourceDefinition.DbUpdatesScanner.GetUpdates();

            foreach (string fullSourceName in fullSourceNames)
            {
                try
                {
                    IDbUpdate dbUpdate = dbUpdateSourceDefinition.DbUpdateBuilder.Build(new DbUpdateSourceDescriptor(fullSourceName),
                        dbUpdateSourceDefinition.DbUpdateLoader);

                    dbUpdates.Add(dbUpdate);
                }
                catch (Exception ex)
                {
                    throw new DbUpdateBuildException(fullSourceName, ex);
                }
            }
            return dbUpdates;
        }

        private void DoOnEndProcessDbUpdateSourceDefinition(DbAupdatesApplier sender, DbUpdateSourceDefinition dbUpdateSourceDefinition)
        {
            OnEndProcessDbUpdateSourceDefinition?.Invoke(sender, dbUpdateSourceDefinition);
        }
        #endregion

        #region Methods (private)
        private void DoOnBeginProcessDbUpdateSourceDefinition(DbAupdatesApplier sender, DbUpdateSourceDefinition dbUpdateSourceDefinition)
        {
            OnBeginProcessDbUpdateSourceDefinition?.Invoke(sender, dbUpdateSourceDefinition);
        }

        private DbUpdateSourceDefinition GetDbUpdateSourceDefinition(Type typeOfDbUpdate)
        {
            return _dbUpdateSourceDefinitions.Single(x => x.TypeOfDbUpdate == typeOfDbUpdate);
        }

        private void RemoveAlreadyAppliedUpdates(IDbVersionIdentifier currentDbVersionIdentifier, DbUpdateList dbUpdates)
        {
            for (int i = dbUpdates.Count - 1; i > -1; i--)
            {
                if (dbUpdates[i].NewDbVersion.CompareTo(currentDbVersionIdentifier) != 1)
                {
                    dbUpdates.RemoveAt(i);
                }
            }
        }
        #endregion
    }
}