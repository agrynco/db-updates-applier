#region Usings
using System;
using System.Data;
using Lib.Data.DataProviders;
using Lib.Data.DbVersioning.Exceptions;
#endregion

namespace Lib.Data.DbVersioning
{
    public class NumericCurrentDbVersionDetector : CurrentDbVersionDetector<NumericDbVersionIdentifier>
    {
        #region Methods (public)
        public NumericCurrentDbVersionDetector(IDataProvider dataProvider, string getVersionSqlCommandText,
            string checkDbSupportVersioningCommandText) : base(dataProvider, getVersionSqlCommandText, checkDbSupportVersioningCommandText)
        {
        }

        public override IDbVersionIdentifier CreateZeroIdentifier()
        {
            return new NumericDbVersionIdentifier(0);
        }

        protected override NumericDbVersionIdentifier DoDetect()
        {
            DataProvider.Connection.Open();
            try
            {
                return new NumericDbVersionIdentifier(Convert.ToUInt32(DataProvider.ExecuteScalar(GetVersionSqlCommandText, CommandType.Text)));
            }
            catch (Exception ex)
            {
                throw new DbVersionDetectionException(string.Format("Could not detect version of the DB with command '{0}'", GetVersionSqlCommandText), ex);
            }
        }
        #endregion

    }
}