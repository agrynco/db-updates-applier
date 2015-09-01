#region Usings
using AGrynCo.Lib.ResourcesUtils;
using NUnit.Framework;
#endregion

namespace DbVersioning.Tests
{
    [TestFixture]
    public class MajorMinorSqlDbUpdateBuilder2Tests
    {
        [Test]
        public void BuildTest()
        {
            MajorMinorSqlDbUpdateBuilder2 target = new MajorMinorSqlDbUpdateBuilder2();
            string content = ResourceReader.ReadAsString(GetType(), Constants.DbMigrations.MAJOR_MINOR);
            SqlDbUpdate<MajorMinorDbVersionIdentifier> result = target.Build("0-1.sql", content);

            Assert.AreEqual(result.ExpectedDbVersion, null);
            Assert.AreEqual(result.NewDbVersion, new MajorMinorDbVersionIdentifier(0, 1));
            Assert.AreEqual(result.FullName, "0-1.sql");
            Assert.IsFalse(string.IsNullOrEmpty(result.Content));
        }
    }
}