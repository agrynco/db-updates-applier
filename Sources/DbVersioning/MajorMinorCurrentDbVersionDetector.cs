#region Usings
using AGrynCo.Lib.Data.DataProviders;
using DbVersioning.Exceptions;
#endregion

namespace DbVersioning
{
    public class MajorMinorCurrentDbVersionDetector : CurrentDbVersionDetector<MajorMinorDbVersionIdentifier>
    {
        #region Constructors
        public MajorMinorCurrentDbVersionDetector(IDataProvider dataProvider, string getVersionSqlCommandText, string checkDbSupportVersioningCommandText)
            : base(dataProvider, getVersionSqlCommandText, checkDbSupportVersioningCommandText)
        {
        }
        #endregion

        #region Methods (protected)
        public override IDbVersionIdentifier CreateZeroIdentifier()
        {
            return new MajorMinorDbVersionIdentifier(0, 0);
        }

        protected override MajorMinorDbVersionIdentifier DoDetect()
        {
            IDbDataReader reader = DataProvider.ExecuteReader(GetVersionSqlCommandText);
            try
            {
                if (reader.Read())
                {
                    try
                    {
                        var result = new MajorMinorDbVersionIdentifier(reader.GetValue<int>("MajorVersion"), reader.GetValue<int>("MinorVersion"));

                        return result;
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            }
            finally
            {
                DataProvider.CloseConnection();
            }

            throw new DbVersionDetectionException("Could not detect current version of the database");
        }
        #endregion
    }
}