#region Usings
using Lib.Utils.ResourcesUtils;

using NUnit.Framework;
#endregion

namespace Lib.Data.DbVersioning.Tests
{
    [TestFixture]
    public class MajorMinorSqlDbUpdateBuilderTests
    {
        [Test]
        public void BuildTest()
        {
            MajorMinorSqlDbUpdateBuilder target = new MajorMinorSqlDbUpdateBuilder();
            string content = ResourceReader.ReadAsString(GetType(), Constants.DbMigrations.MAJOR_MINOR);
            SqlDbUpdate<MajorMinorDbVersionIdentifier> result = target.Build("", content);

            Assert.AreEqual(result.ExpectedDbVersion, new MajorMinorDbVersionIdentifier(5, 7));
            Assert.AreEqual(result.NewDbVersion, new MajorMinorDbVersionIdentifier(5, 8));
            Assert.AreEqual(result.FullName, "");
            Assert.IsFalse(string.IsNullOrEmpty(result.Content));
        }
    }
}