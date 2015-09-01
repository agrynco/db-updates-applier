#region Usings
using AGrynCo.Lib.ResourcesUtils;
using NUnit.Framework;
#endregion

namespace DbVersioning.Tests
{
    [TestFixture]
    public class MajorMinorSqlDbVersionDetectorsTests
    {
        [Test]
        public void DetectNewDbVersion()
        {
            string content = ResourceReader.ReadAsString(GetType(), Constants.DbMigrations.MAJOR_MINOR);
            NewDbMajorMinorVersionDetector target = new NewDbMajorMinorVersionDetector();
            MajorMinorDbVersionIdentifier actual = target.Detect(null, content);
            Assert.AreEqual(new MajorMinorDbVersionIdentifier(5, 8), actual);
        }
    }
}