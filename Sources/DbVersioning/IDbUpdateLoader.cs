namespace Lib.Data.DbVersioning
{
    public interface IDbUpdateLoader
    {
        #region Abstract Methods
        string Load(string fullSourceName);
        #endregion
    }
}