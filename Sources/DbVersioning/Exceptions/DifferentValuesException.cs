#region Usings
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
#endregion

namespace Lib.Data.DbVersioning.Exceptions
{
    public class DifferentValuesException : DbVersionDetectionException
    {
        #region Constructors
        public DifferentValuesException(string message) : base(message)
        {
        }

        public DifferentValuesException(IEnumerable<Match> uniqueMatches)
            : this("There are few dbVersion definings but the values are different: " + string.Join(", ", uniqueMatches.Select(match => string.Format("{0}", match.Value))))
        {
        }
        #endregion
    }
}