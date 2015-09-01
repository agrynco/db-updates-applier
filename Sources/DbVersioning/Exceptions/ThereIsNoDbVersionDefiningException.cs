#region Usings
using System;
#endregion

namespace DbVersioning.Exceptions
{
    public class ThereIsNoDbVersionDefiningException : DbVersionDetectionException
    {
        #region Constructors
        public ThereIsNoDbVersionDefiningException(string message) : base(message)
        {
        }

        public ThereIsNoDbVersionDefiningException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ThereIsNoDbVersionDefiningException()
        {
        }
        #endregion
    }
}