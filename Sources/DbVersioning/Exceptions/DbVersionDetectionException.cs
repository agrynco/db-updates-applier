#region Usings
using System;
#endregion

namespace Lib.Data.DbVersioning.Exceptions
{
    public class DbVersionDetectionException : DbVersioningException
    {
        #region Constructors
        public DbVersionDetectionException()
        {
        }

        public DbVersionDetectionException(string message) : base(message)
        {
        }

        public DbVersionDetectionException(string message, Exception innerException) : base(message, innerException)
        {
        }
        #endregion
    }
}