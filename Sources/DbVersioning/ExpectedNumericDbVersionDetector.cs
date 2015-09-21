#region Usings
using System;
#endregion

namespace DbVersioning
{
    public class ExpectedNumericDbVersionDetector : SqlDbVersionDetectorBase<NumericDbVersionIdentifier>
    {
        #region Methods (public)
        public override NumericDbVersionIdentifier Detect(DbUpdateSourceDescriptor dbUpdateSourceDescriptor, string content)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}