#region Usings
using System;
using System.IO;
using System.Reflection;
using Lib.Data.DbVersioning;
using Lib.Data.DbVersioning.Exceptions;
using Lib.Utils.Console;
using Lib.Utils.Console.CommandLineParameters;
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
        private static readonly ICommandLineParameter[] _COMMAND_LINE_PARAMETERS = {_FROM_SCRATCH};
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
                    DbAupdatesApplier dbAupdatesApplier = new DbAupdatesApplier(new DbUpdateDefinitionsFromConfigBuilder().BuildDbUpdateSourceDefinitions(),
                        _FROM_SCRATCH.Value);

                    dbAupdatesApplier.OnExecutedUpdate += dbAupdateApplier_OnExecutedUpdate;
                    dbAupdatesApplier.OnFailureUpdate += dbAupdateApplier_OnFailureUpdate;
                    dbAupdatesApplier.OnBeforeExecuteUpdate += dbAupdateApplier_OnBeforeExecuteUpdate;
                    dbAupdatesApplier.OnBeginProcessDbUpdateSourceDefinition += DbAupdateApplierOnOnBeginProcessDbUpdateSourceDefinition;
                    dbAupdatesApplier.OnEndProcessDbUpdateSourceDefinition += DbAupdatesApplierOnOnEndProcessDbUpdateSourceDefinition;
                    dbAupdatesApplier.OnDropDataBase += dbAupdatesApplier_OnDropDataBase;

                    dbAupdatesApplier.Apply();
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

        private static void dbAupdatesApplier_OnDropDataBase(DbAupdatesApplier sender, string dbName)
        {
            ConsoleExtensions.WriteInfo(string.Format("Drop database {0}", dbName));
        }

        private static void DbAupdatesApplierOnOnEndProcessDbUpdateSourceDefinition(DbAupdatesApplier sender, DbUpdateSourceDefinition dbUpdateSourceDefinition)
        {
            ConsoleExtensions.WriteInfo(string.Format("End process").AppenCurrentdDateTime());
        }
        #endregion

        #region Static Methods (private)
        private static void DbAupdateApplierOnOnBeginProcessDbUpdateSourceDefinition(DbAupdatesApplier sender, DbUpdateSourceDefinition dbUpdateSourceDefinition)
        {
            ConsoleExtensions.WriteInfo(string.Format("Begin process").AppenCurrentdDateTime());
        }

        private static void dbAupdateApplier_OnBeforeExecuteUpdate(DbAupdatesApplier sender, IDbUpdate dbUpdate)
        {
            ConsoleExtensions.WriteInfo(string.Format("Applying '{0}'", dbUpdate.FullName));
        }

        private static void dbAupdateApplier_OnExecutedUpdate(DbAupdatesApplier sender, UpdateDbExecutionResult result)
        {
            ConsoleExtensions.WriteInfo(string.Format("Update {0} executed successfully", result.ExecutedDbUpdate.FullName));
            if (!string.IsNullOrEmpty(result.ExecutionNote))
            {
                ConsoleExtensions.WriteInfo("Execution note: " + result.ExecutionNote);
            }
        }

        private static void dbAupdateApplier_OnFailureUpdate(DbAupdatesApplier sender, ExecuteDbUpdateException ex)
        {
            ConsoleExtensions.WriteError(string.Format("Update {0} failure.", ex.ExecutedDbUpdate.FullName), ex);
        }
        #endregion
    }
}