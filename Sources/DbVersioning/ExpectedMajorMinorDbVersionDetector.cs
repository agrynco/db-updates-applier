namespace DbVersioning
{
    public class ExpectedMajorMinorDbVersionDetector : MajorMinorSqlDbVersionDetector
    {
        #region Properties (protected)
        public override string DbVersionIdentifier
        {
            get { return "expectedDbVersion"; }
        }
        #endregion
    }
}