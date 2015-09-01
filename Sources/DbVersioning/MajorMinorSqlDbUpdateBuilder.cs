#region Usings
#endregion

namespace DbVersioning
{
    public class MajorMinorSqlDbUpdateBuilder : SqlDbUpdateBuilder<MajorMinorDbVersionIdentifier, NewDbMajorMinorVersionDetector, ExpectedMajorMinorDbVersionDetector>
    {
    }
}