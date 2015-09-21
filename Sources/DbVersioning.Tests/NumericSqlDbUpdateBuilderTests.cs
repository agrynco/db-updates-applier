#region Usings
using NUnit.Framework;
#endregion

namespace DbVersioning.Tests
{
    [TestFixture]
    public class NumericSqlDbUpdateBuilderTests
    {
        [Test]
        public void BuildTest()
        {
            NumericSqlDbUpdateBuilder target = new NumericSqlDbUpdateBuilder();
            EmbeddedResourceDbUpdateSourceDescriptor embeddedResourceDbUpdateSourceDescriptor = new EmbeddedResourceDbUpdateSourceDescriptor(GetType(), Constants.DbMigrations.NUMERIC);
            SqlDbUpdate<NumericDbVersionIdentifier> result = target.Build(
                embeddedResourceDbUpdateSourceDescriptor,
                new EmbeddedResourceDbUpdateLoader());

            Assert.AreEqual(result.ExpectedDbVersion, new NumericDbVersionIdentifier(4));
            Assert.AreEqual(result.NewDbVersion, new NumericDbVersionIdentifier(5));
            Assert.AreEqual(result.DbUpdateSourceDescriptor, embeddedResourceDbUpdateSourceDescriptor);
            Assert.IsFalse(string.IsNullOrEmpty(result.Content));
        }
    }
}