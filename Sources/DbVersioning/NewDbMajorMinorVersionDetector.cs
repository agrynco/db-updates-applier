namespace Lib.Data.DbVersioning
{
    public class NewDbMajorMinorVersionDetector : MajorMinorSqlDbVersionDetector
    {
        #region Properties (protected)
        public override string DbVersionIdentifier
        {
            get { return "newDbVersion"; }
        }
        #endregion
    }
}