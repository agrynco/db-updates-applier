﻿#region Usings
using AGrynCo.Lib.ResourcesUtils;
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
            string content = ResourceReader.ReadAsString(GetType(), Constants.DbMigrations.NUMERIC);
            SqlDbUpdate<NumericDbVersionIdentifier> result = target.Build("", content);

            Assert.AreEqual(result.ExpectedDbVersion, new NumericDbVersionIdentifier(4));
            Assert.AreEqual(result.NewDbVersion, new NumericDbVersionIdentifier(5));
            Assert.AreEqual(result.FullName, "");
            Assert.IsFalse(string.IsNullOrEmpty(result.Content));
        }
    }
}