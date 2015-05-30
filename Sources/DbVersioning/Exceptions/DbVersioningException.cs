#region Usings
using System;
using System.Runtime.Serialization;
#endregion

namespace Lib.Data.DbVersioning.Exceptions
{
    public class DbVersioningException : Exception
    {
        #region Constructors
        public DbVersioningException()
        {
        }

        public DbVersioningException(string message) : base(message)
        {
        }

        public DbVersioningException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DbVersioningException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
        #endregion
    }
}