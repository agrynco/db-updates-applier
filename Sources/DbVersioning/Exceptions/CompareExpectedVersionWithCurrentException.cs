namespace Lib.Data.DbVersioning.Exceptions
{
    public class CompareExpectedVersionWithCurrentException : DbVersioningException
    {
        #region Constructors
        public CompareExpectedVersionWithCurrentException(int compareResult, IDbVersionIdentifier expectedDbVersion, IDbVersionIdentifier dbVersion)
            : base(string.Format("Expected DB version ({0}) {1} current DB version ({2})", expectedDbVersion,
                                 compareResult == -1 ? "is less than" : (compareResult == 0 ? "equals to" : "is greater then"),
                                 dbVersion))
        {
        }
        #endregion
    }
}