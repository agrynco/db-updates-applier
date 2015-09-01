#region Usings
using AGrynCo.Lib.ResourcesUtils;
using NUnit.Framework;
#endregion

namespace DbVersioning.Tests
{
    [TestFixture]
    public class MajorMinorSqlDbUpdateBuilderTests
    {
        [Test]
        public void BuildTest()
        {
            MajorMinorSqlDbUpdateBuilder target = new MajorMinorSqlDbUpdateBuilder();
            string content = ResourceReader.ReadAsString(GetType(), Constants.DbMigrations.MAJOR_MINOR);
            SqlDbUpdate<MajorMinorDbVersionIdentifier> result = target.Build("0-1.sql", content);

            Assert.AreEqual(result.ExpectedDbVersion, new MajorMinorDbVersionIdentifier(5, 7));
            Assert.AreEqual(result.NewDbVersion, new MajorMinorDbVersionIdentifier(5, 8));
            Assert.AreEqual(result.FullName, "0-1.sql");
            Assert.IsFalse(string.IsNullOrEmpty(result.Content));
        }
    }
}