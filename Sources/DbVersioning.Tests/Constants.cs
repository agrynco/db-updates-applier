namespace DbVersioning.Tests
{
    public static class Constants
    {
        public static class DbMigrations
        {
            private const string PATH_TO_MIGRATIONS = "DbVersioning.Tests.Resources";
            public const string NUMERIC = PATH_TO_MIGRATIONS + ".Numeric.sql";
            public const string MAJOR_MINOR = PATH_TO_MIGRATIONS + ".MajorMinor.sql";
        }
    }
}