#region Usings
using AGrynCo.Lib.ResourcesUtils;
using Moq;
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
            DbUpdateSourceDescriptor dbUpdateSourceDescriptor = new DbUpdateSourceDescriptor("0-1.sql");

            string dbUpdateContent = ResourceReader.ReadAsString(GetType(), Constants.DbMigrations.MAJOR_MINOR);

            Mock<IDbUpdateLoader> dbUpdateLoaderMock = new Mock<IDbUpdateLoader>();
            dbUpdateLoaderMock.Setup(mock => mock.Load(dbUpdateSourceDescriptor)).Returns(dbUpdateContent);

            SqlDbUpdate<MajorMinorDbVersionIdentifier> result = target.Build(dbUpdateSourceDescriptor,
                dbUpdateLoaderMock.Object);

            Assert.AreEqual(result.ExpectedDbVersion, new MajorMinorDbVersionIdentifier(5, 7));
            Assert.AreEqual(result.NewDbVersion, new MajorMinorDbVersionIdentifier(5, 8));
            Assert.AreEqual(result.DbUpdateSourceDescriptor.Path, "0-1.sql");
            Assert.IsFalse(string.IsNullOrEmpty(result.Content));
        }
    }
}