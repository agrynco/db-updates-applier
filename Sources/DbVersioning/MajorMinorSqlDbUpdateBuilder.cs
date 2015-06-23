#region Usings

#endregion

namespace Lib.Data.DbVersioning
{
    public class MajorMinorSqlDbUpdateBuilder : SqlDbUpdateBuilder<MajorMinorDbVersionIdentifier, NewDbMajorMinorVersionDetector, ExpectedMajorMinorDbVersionDetector>
    {
    }
}