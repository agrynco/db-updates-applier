#region Usings
using AGrynCo.Lib.Data.DataProviders.MsSql;
using AGrynCo.Lib.ResourcesUtils;
#endregion

namespace DbVersioning
{
    public class VersionedSqlDataBaseManager : SqlDatabaseManager
    {
        public VersionedSqlDataBaseManager(string connectionString) : base(connectionString)
        {
        }

        protected override string BuildCleanUpDbScript(string dbName)
        {
            string sqlScript = ResourceReader.ReadAsString(typeof(VersionedSqlDataBaseManager),
                "DbVersioning.MsSqlCleanUpDb.sql");

            return sqlScript.Replace("[@dbName]", $"[{dbName}]");
        }
    }
}