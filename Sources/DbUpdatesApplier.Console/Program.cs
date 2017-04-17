#region Usings
using System;
using System.IO;
using System.Reflection;
using AGrynCo.Lib.Console;
using AGrynCo.Lib.Console.CommandLineParameters;
using DbVersioning;
using DbVersioning.Exceptions;
#endregion

namespace DbUpdateApplier.Console
{
    public static class StringExtension
    {
        public static string AppenCurrentdDateTime(this string str)
        {
            return str + " - " + DateTime.Now;
        }
    }

    internal class Program
    {
        private static readonly ICommandLineParameter<bool> _FROM_SCRATCH = new BooleanCommandLineParameter("fromScratch", "true/false", false);

        private static readonly ICommandLineParameter<bool> _TRY_CLEANUP = new BooleanCommandLineParameter("tryCleanUp", "true/false", false);

        private static readonly ICommandLineParameter[] _COMMAND_LINE_PARAMETERS = {_FROM_SCRATCH, _TRY_CLEANUP};

        private static readonly ICommandLineParameter[] _NO_PARAMETERS = new ICommandLineParameter[0];

        private static readonly ICommandLineParameter[][] _SET_OF_SEQUENCES_OF_COMMAND_LINE_PARAMETERS = {_NO_PARAMETERS, _COMMAND_LINE_PARAMETERS};

        #region Constants
        private const int _APPLY_UPDATE_ERROR = 2;

        private const int _COMMAND_LINE_PARAMETERS_ARE_NOT_VALID = 1;

        private const int _NO_ERROR = 0;

        private const int _UNKNOWN_ERROR = 2;
        #endregion

        #region Main Static Methods (private)
        private static int Main(string[] args)
        {
            CommandLineProcessingResult processingResult = CommandLineParametersPrcessor.Process(args, _COMMAND_LINE_PARAMETERS);

            if (processingResult.IsValid)
            {
                ConsoleExtensions.WriteInfo("Current dir: " + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));

                int resultCode = _NO_ERROR;
                try
                {
                    var dbUpdatesApplier = new DbAupdatesApplier(new DbUpdateDefinitionsFromConfigBuilder().BuildDbUpdateSourceDefinitions(),
                        _FROM_SCRATCH.Value, _TRY_CLEANUP.Value);

                    dbUpdatesApplier.OnExecutedUpdate += dbAupdateApplier_OnExecutedUpdate;
                    dbUpdatesApplier.OnFailureUpdate += dbAupdateApplier_OnFailureUpdate;
                    dbUpdatesApplier.OnBeforeExecuteUpdate += dbAupdateApplier_OnBeforeExecuteUpdate;
                    dbUpdatesApplier.OnBeginProcessDbUpdateSourceDefinition += DbAupdateApplierOnOnBeginProcessDbUpdateSourceDefinition;
                    dbUpdatesApplier.OnEndProcessDbUpdateSourceDefinition += DbAupdatesApplierOnOnEndProcessDbUpdateSourceDefinition;
                    dbUpdatesApplier.OnDropDataBase += dbAupdatesApplier_OnDropDataBase;
                    dbUpdatesApplier.OnCleanUpDataBase += DbUpdatesApplier_OnCleanUpDataBase;

                    dbUpdatesApplier.Apply();
                }
                catch (DbUpdatesValidationException e)
                {
                    ConsoleExtensions.WriteException(e);
                    foreach (DbUpdateValidationResult dbUpdateValidationResult in e.ValidationResult)
                    {
                        ConsoleExtensions.WriteWarning(dbUpdateValidationResult.ValidationMessage);
                        foreach (IDbUpdate dbUpdate in dbUpdateValidationResult.ValidatedObject)
                        {
                            ConsoleExtensions.WriteWarning(string.Format("    {0}", dbUpdate));
                        }
                    }
                }
                catch (Exception e)
                {
                    ConsoleExtensions.WriteException(e);
                    resultCode = _UNKNOWN_ERROR;
                }
                return resultCode;
            }

            return _COMMAND_LINE_PARAMETERS_ARE_NOT_VALID;
        }

        private static void DbUpdatesApplier_OnCleanUpDataBase(DbAupdatesApplier sender, string dbName)
        {
            ConsoleExtensions.WriteInfo($"Clean up database {dbName}");
        }

        private static void dbAupdatesApplier_OnDropDataBase(DbAupdatesApplier sender, string dbName)
        {
            ConsoleExtensions.WriteInfo($"Drop database {dbName}");
        }

        private static void DbAupdatesApplierOnOnEndProcessDbUpdateSourceDefinition(DbAupdatesApplier sender,
            DbUpdateSourceDefinition dbUpdateSourceDefinition)
        {
            ConsoleExtensions.WriteInfo("End process".AppenCurrentdDateTime());
        }
        #endregion

        #region Static Methods (private)
        private static void DbAupdateApplierOnOnBeginProcessDbUpdateSourceDefinition(DbAupdatesApplier sender,
            DbUpdateSourceDefinition dbUpdateSourceDefinition)
        {
            ConsoleExtensions.WriteInfo("Begin process".AppenCurrentdDateTime());
        }

        private static void dbAupdateApplier_OnBeforeExecuteUpdate(DbAupdatesApplier sender, IDbUpdate dbUpdate)
        {
            ConsoleExtensions.WriteInfo($"Applying '{DbUpdateToString(dbUpdate)}'");
        }

        private static void dbAupdateApplier_OnExecutedUpdate(DbAupdatesApplier sender, UpdateDbExecutionResult result)
        {
            ConsoleExtensions.WriteInfo(string.Format("Update {0} executed successfully", DbUpdateToString(result.ExecutedDbUpdate)));
            if (!string.IsNullOrEmpty(result.ExecutionNote))
            {
                ConsoleExtensions.WriteInfo("Execution note: " + result.ExecutionNote);
            }
        }

        private static void dbAupdateApplier_OnFailureUpdate(DbAupdatesApplier sender, ExecuteDbUpdateException ex)
        {
            ConsoleExtensions.WriteError(string.Format("Update {0} failure.", ex.ExecutedDbUpdate), ex);
        }

        private static string DbUpdateToString(IDbUpdate dbUpdate)
        {
            return $"{dbUpdate.DbUpdateSourceDescriptor.Path}";
        }
        #endregion
    }
}