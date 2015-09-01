#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AGrynCo.Lib;
using DbVersioning.Exceptions;
#endregion

namespace DbVersioning
{
    public abstract class MajorMinorSqlDbVersionDetector : SqlDbVersionDetectorBase<MajorMinorDbVersionIdentifier>
    {
        #region Properties (protected)
        public abstract string DbVersionIdentifier { get; }
        #endregion

        #region Methods (public)
        public override MajorMinorDbVersionIdentifier Detect(string fullSourceName, string content)
        {
            MajorMinorDbVersionIdentifier result = MajorMinorDbVersionIdentifier.Parse(DetectVersion(content, DbVersionIdentifier));

            return result;
        }
        #endregion

        #region Methods (protected)
        private string DetectVersion(string updateSqlScript, string versionIdentifier)
        {
            if (String.IsNullOrEmpty(versionIdentifier))
            {
                throw new ArgumentException();
            }

            MatchCollection matches = Regex.Matches(updateSqlScript, versionIdentifier + ASSIGNMENT_PATTERN + "'" + VERSION_NUMBER_PATTERN + "'", RegexOptions.IgnoreCase);

            if (matches.Count > 0)
            {
                List<Match> matchesList = new List<Match>();
                foreach (Match match in matches)
                {
                    if (match.Success)
                    {
                        matchesList.Add(match);
                    }
                }

                List<Match> uniqueMatches = matchesList.DistinctBy(match => match.Value).ToList();

                if (uniqueMatches.Count == 1)
                {
                    Match versionNumberMatch = Regex.Match(matchesList[0].Value, VERSION_NUMBER_PATTERN);
                    return versionNumberMatch.Value;
                }

                throw new DifferentValuesException(uniqueMatches);
            }

            throw new ThereIsNoDbVersionDefiningException();
        }
        #endregion

        #region Constants
        private const string ASSIGNMENT_PATTERN = "((([ ]?)+=([ ]?)+)|(([ ]?)+=>([ ]?)+))";

        private const string VERSION_NUMBER_PATTERN = "[0-9]+.[0-9]+";
        #endregion
    }
}