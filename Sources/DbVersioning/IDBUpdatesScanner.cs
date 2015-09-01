namespace DbVersioning
{
    public interface IDBUpdatesScanner
    {
        #region Abstract Methods
        /// <summary>
        /// Scan source for DB updates
        /// </summary>
        /// <returns>Gets full names (path + name) of DB updates</returns>
        string[] GetUpdates();
        #endregion
    }
}