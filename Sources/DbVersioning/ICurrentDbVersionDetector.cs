namespace Lib.Data.DbVersioning
{
    public interface ICurrentDbVersionDetector
    {
        #region Abstract Methods
        bool CheckOnDbSupportVersioning();
        IDbVersionIdentifier Detect();
        IDbVersionIdentifier CreateZeroIdentifier();

        /// <summary>
        /// Ñhecking for the existence of the database
        /// </summary>
        /// <returns>true - DB exists, otherwise - false</returns>
        bool CheckOnDbExists();
        #endregion
    }
}