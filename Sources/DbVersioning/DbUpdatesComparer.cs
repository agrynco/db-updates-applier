#region Usings
using System.Collections.Generic;
#endregion

namespace Lib.Data.DbVersioning
{
    public class DbUpdatesComparer : IComparer<IDbUpdate>
    {
        #region IComparer<IDbUpdate> Methods
        /// <summary>
        /// Compare to scripts by <see cref="ExpectedMajorMinorDbVersionDetector"/>. Uses for sorting scripts
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(IDbUpdate x, IDbUpdate y)
        {
            return x.NewDbVersion.CompareTo(y.NewDbVersion);
        }
        #endregion
    }
}