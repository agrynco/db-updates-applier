#region Usings
using System;
#endregion

namespace Lib.Data.DbVersioning
{
    public class ExpectedNumericDbVersionDetector : SqlDbVersionDetectorBase<NumericDbVersionIdentifier>
    {
        #region Methods (public)
        public override NumericDbVersionIdentifier Detect(string fullSourceName, string content)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}