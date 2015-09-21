#region Usings
using AGrynCo.Lib.ResourcesUtils;
using Moq;
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
            DbUpdateSourceDescriptor dbUpdateSourceDescriptor = new DbUpdateSourceDescriptor("0-1.sql");
            string dbUpdateContent = ResourceReader.ReadAsString(GetType(), Constants.DbMigrations.MAJOR_MINOR);

            Mock<IDbUpdateLoader> dbUpdateLoaderMock = new Mock<IDbUpdateLoader>();
            dbUpdateLoaderMock.Setup(mock => mock.Load(dbUpdateSourceDescriptor)).Returns(dbUpdateContent);

            SqlDbUpdate<MajorMinorDbVersionIdentifier> result = target.Build(dbUpdateSourceDescriptor, 
                dbUpdateLoaderMock.Object);

            Assert.AreEqual(result.ExpectedDbVersion, null);
            Assert.AreEqual(result.NewDbVersion, new MajorMinorDbVersionIdentifier(0, 1));
            Assert.AreEqual(result.DbUpdateSourceDescriptor.Path, "0-1.sql");
            Assert.IsFalse(string.IsNullOrEmpty(result.Content));
        }
    }
}