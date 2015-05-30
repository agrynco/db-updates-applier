#region Usings
using System;
using System.Text.RegularExpressions;
#endregion

namespace Lib.Data.DbVersioning
{
    public class NewNumericDbVersionDetector : SqlDbVersionDetectorBase<NumericDbVersionIdentifier>
    {
        #region Properties (protected)
        private static string VersionIdentifier
        {
            get
            {
                return "@newDbVersion";
            }
        }
        #endregion

        #region Methods (public)
        public override NumericDbVersionIdentifier Detect(string fullSourceName, string content)
        {
            NumericDbVersionIdentifier result = new NumericDbVersionIdentifier(DetectVersion(content, VersionIdentifier));

            return result;
        }
        #endregion

        #region Methods (protected)
        private uint DetectVersion(string updateSqlScript, string versionIdentifier)
        {
            if (String.IsNullOrEmpty(versionIdentifier))
            {
                throw new ArgumentException();
            }

            Match match = Regex.Match(updateSqlScript, versionIdentifier + _ASSIGNMENT_PATTERN + _VERSION_NUMBER_PATTERN, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                match = Regex.Match(match.Value, _VERSION_NUMBER_PATTERN);

                if (match.Success)
                {
                    return uint.Parse(match.Value);
                }
            }

            throw new ArgumentException();
        }
        #endregion

        #region Constants
        private const string _ASSIGNMENT_PATTERN = "((([ ]?)+=([ ]?)+)|(([ ]?)+=>([ ]?)+))";

        private const string _VERSION_NUMBER_PATTERN = "[0-9]+";
        #endregion
    }
}