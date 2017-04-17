namespace DbVersioning
{
    public interface IDbUpdate
    {
        #region Properties (public)
        IDbVersionIdentifier ExpectedDbVersion { get; }
        DbUpdateSourceDescriptor DbUpdateSourceDescriptor { get; set; }

        IDbVersionIdentifier NewDbVersion { get; }

        /// <summary>
        /// Means that this is a "special" update which can be non "standard"... Used for create DB and post initial routine
        /// </summary>
        bool IsInitial { get; }

        bool IsLast { get; set; }
        #endregion
    }
}