using System;
using System.Diagnostics.CodeAnalysis;

namespace Lib.Data.DbVersioning.Exceptions
{
    public class DbUpdateBuildException : DbVersioningException
    {
         public DbUpdateBuildException(string fullSourceName, Exception innerException):
             base(string.Format("Can not build IDbUpdate from '{0}'", fullSourceName), innerException)
         {}

        public DbUpdateBuildException(string message) : base(message)
        {
        }
    }
}