#region Usings
using System;
using System.IO;
#endregion

namespace Lib.Data.DbVersioning
{
    public class MajorMinorSqlDbUpdateBuilder : SqlDbUpdateBuilder<MajorMinorDbVersionIdentifier, NewDbMajorMinorVersionDetector, ExpectedMajorMinorDbVersionDetector>
    {
    }

    public class DummyExpectedMajorMinorDbVersionDetector : SqlDbVersionDetectorBase<MajorMinorDbVersionIdentifier>
    {
        #region Methods (public)
        public override MajorMinorDbVersionIdentifier Detect(string fullSourceName, string content)
        {
            return null;
        }
        #endregion
    }

    public class NewDbMajorMinorFromFileNameVersionDetector : SqlDbVersionDetectorBase<MajorMinorDbVersionIdentifier>
    {
        #region Methods (public)
        public override MajorMinorDbVersionIdentifier Detect(string fullSourceName, string content)
        {
            string[] strings = Path.GetFileNameWithoutExtension(fullSourceName).Split(new[] {"-"}, StringSplitOptions.None);

            return new MajorMinorDbVersionIdentifier(int.Parse(strings[0]), int.Parse(strings[1]));
        }
        #endregion
    }
}