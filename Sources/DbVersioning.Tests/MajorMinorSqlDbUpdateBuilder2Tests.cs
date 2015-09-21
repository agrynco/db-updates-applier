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
            SqlDbUpdate<MajorMinorDbVersionIdentifier> result = target.Build(new DbUpdateSourceDescriptor("0-1.sql"), 
                new FileSystemDbUpdateLoader());

            Assert.AreEqual(result.ExpectedDbVersion, null);
            Assert.AreEqual(result.NewDbVersion, new MajorMinorDbVersionIdentifier(0, 1));
            Assert.AreEqual(result.DbUpdateSourceDescriptor.Path, "0-1.sql");
            Assert.IsFalse(string.IsNullOrEmpty(result.Content));
        }
    }
}