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
            
            SqlDbUpdate<MajorMinorDbVersionIdentifier> result = target.Build(new DbUpdateSourceDescriptor("0-1.sql"), 
                new FileSystemDbUpdateLoader());

            Assert.AreEqual(result.ExpectedDbVersion, new MajorMinorDbVersionIdentifier(5, 7));
            Assert.AreEqual(result.NewDbVersion, new MajorMinorDbVersionIdentifier(5, 8));
            Assert.AreEqual(result.DbUpdateSourceDescriptor.Path, "0-1.sql");
            Assert.IsFalse(string.IsNullOrEmpty(result.Content));
        }
    }
}