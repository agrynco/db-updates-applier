#region Usings
using System;
using AGrynCo.Lib.ToStringConverters;
#endregion

namespace DbVersioning
{
    public class BaseSqlDbUpdate<TDbVersionIdentifier> : DbUpdate<TDbVersionIdentifier>
        where TDbVersionIdentifier : IDbVersionIdentifier
    {
        #region Constructors
        public BaseSqlDbUpdate(string fullName, string body, TDbVersionIdentifier expectedDbVersion, TDbVersionIdentifier newDbVersion)
            : base(fullName, expectedDbVersion, newDbVersion)
        {
            if (string.IsNullOrEmpty(body))
            {
                throw new ArgumentException("argument is incorrect.", "body");
            }

            Content = body;
        }
        #endregion

        #region Properties (public)
        [IgnoreConvertToString]
        public string Content { get; private set; }
        #endregion
    }
}