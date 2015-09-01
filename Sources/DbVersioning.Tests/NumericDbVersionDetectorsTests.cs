#region Usings
using AGrynCo.Lib.ResourcesUtils;
using NUnit.Framework;
#endregion

namespace DbVersioning.Tests
{
    [TestFixture]
    public class NumericDbVersionDetectorsTests
    {
        [Test]
        public void DetectNewDbVersion()
        {
            string content = ResourceReader.ReadAsString(GetType(), Constants.DbMigrations.NUMERIC);
            NewNumericDbVersionDetector target = new NewNumericDbVersionDetector();
            NumericDbVersionIdentifier actual = target.Detect(null, content);
            Assert.AreEqual(new NumericDbVersionIdentifier(5), actual);
        }
    }
}