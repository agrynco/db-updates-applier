namespace Lib.Data.DbVersioning
{
    public interface IDbUpdate
    {
        #region Properties (public)
        IDbVersionIdentifier ExpectedDbVersion { get; }
        string FullName { get; set; }
        IDbVersionIdentifier NewDbVersion { get; }

        /// <summary>
        /// Means that thi is a "special" update which can be non "standart"... Used for create DB and postinitial routine
        /// </summary>
        bool IsInitial { get; }
        bool IsLast { get; set; }
        #endregion
    }
}