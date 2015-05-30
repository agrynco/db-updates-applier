#region Usings
using System;
#endregion

namespace Lib.Data.DbVersioning
{
    public interface IDbVersionIdentifier : IComparable<IDbVersionIdentifier>
    {
        /// <summary>
        /// </summary>
        /// <remarks></remarks>
        bool IsItZeroIdentifier { get; }
    }
}