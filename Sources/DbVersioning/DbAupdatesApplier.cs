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

    public delegate void OnProcessDbUpdateSourceDefinitionDelegate(DbAupdatesApplier sender, DbUpdateSourceDefinition dbUpdateSourceDefinition);

    public class DbAupdatesApplier
    {
        #region Constructors
        public DbAupdatesApplier(IEnumerable<DbUpdateSourceDefinition> dbUpdateSourceDefinitions, bool fromScratch)
        {
            _dbUpdateSourceDefinitions = dbUpdateSourceDefinitions;
            _fromScratch = fromScratch;
        }
        #endregion

        #region Fields (private)
        private readonly IEnumerable<DbUpdateSourceDefinition> _dbUpdateSourceDefinitions;

        private readonly bool _fromScratch;
        #endregion

        #region Events (public)
        public event OnTryToExecuteUpdateDelegate OnBeforeExecuteUpdate;

        public event OnProcessDbUpdateSourceDefinitionDelegate OnBeginProcessDbUpdateSourceDefinition;

        public event OnProcessDbUpdateSourceDefinitionDelegate OnEndProcessDbUpdateSourceDefinition;

        public event OnExecuteUpdateDelegate OnExecutedUpdate;

        public event OnFailureUpdateDelegate OnFailureUpdate;

        public event OnDropDataBaseDelegate OnDropDataBase;
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
                    if (OnDropDataBase != null)
                    {
                        OnDropDataBase(this, dbUpdateSourceDefinition.DatabaseManager.TargetDataBaseName);
                    }
                    dbUpdateSourceDefinition.DatabaseManager.Drop();
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
                    if (OnBeforeExecuteUpdate != null)
                    {
                        OnBeforeExecuteUpdate(this, dbUpdate);
                    }

                    IDbUpdateExecutor dbUpdateExecutor = GetDbUpdateSourceDefinition(dbUpdate.GetType()).DbUpdateExecutor;
                    UpdateDbExecutionResult updateDbExecutionResult = dbUpdateExecutor.Execute(currentDbVersionIdentifier, dbUpdate);
                    if (OnExecutedUpdate != null)
                    {
                        OnExecutedUpdate(this, updateDbExecutionResult);
                    }
                }
                catch (ExecuteDbUpdateException ex)
                {
                    if (OnFailureUpdate != null)
                    {
                        OnFailureUpdate(this, ex);
                    }

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
            OnProcessDbUpdateSourceDefinitionDelegate handler = OnEndProcessDbUpdateSourceDefinition;
            if (handler != null)
            {
                handler(sender, dbUpdateSourceDefinition);
            }
        }
        #endregion

        #region Methods (private)
        private void DoOnBeginProcessDbUpdateSourceDefinition(DbAupdatesApplier sender, DbUpdateSourceDefinition dbUpdateSourceDefinition)
        {
            OnProcessDbUpdateSourceDefinitionDelegate handler = OnBeginProcessDbUpdateSourceDefinition;
            if (handler != null)
            {
                handler(sender, dbUpdateSourceDefinition);
            }
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