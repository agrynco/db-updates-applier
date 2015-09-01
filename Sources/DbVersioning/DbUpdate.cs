#region Usings
using AGrynCo.Lib;
#endregion

namespace DbVersioning
{
    public class DbUpdate<TDbVersionIdentifier> : BaseClass, IDbUpdate
        where TDbVersionIdentifier : IDbVersionIdentifier
    {
        #region Constructors
        public DbUpdate(string fullName, TDbVersionIdentifier expectedDbVersion, TDbVersionIdentifier newDbVersion)
        {
            IsInitial = newDbVersion.IsItZeroIdentifier;

            FullName = fullName;
            ExpectedDbVersion = expectedDbVersion;
            NewDbVersion = newDbVersion;
        }
        #endregion

        #region IDbUpdate Properties
        IDbVersionIdentifier IDbUpdate.ExpectedDbVersion
        {
            get
            {
                return ExpectedDbVersion;
            }
        }

        public string FullName { get; set; }

        IDbVersionIdentifier IDbUpdate.NewDbVersion
        {
            get
            {
                return NewDbVersion;
            }
        }

        public bool IsInitial { get; private set; }

        public bool IsLast { get; set; }
        #endregion

        #region Properties (public)
        public TDbVersionIdentifier ExpectedDbVersion { get; private set; }

        public TDbVersionIdentifier NewDbVersion { get; private set; }
        #endregion
    }
}