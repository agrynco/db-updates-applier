#region Usings
using Lib.Utils.ResourcesUtils;

using NUnit.Framework;
#endregion

namespace Lib.Data.DbVersioning.Tests
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