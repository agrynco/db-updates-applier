#region Usings
using DbVersioning.Configuration;
using NUnit.Framework;
#endregion

namespace DbVersioning.Tests
{
    [TestFixture]
    public class DbVersioningConfigurationTests
    {
        [Test]
        public void Test()
        {
            DbUpdateDefinitionsConfigSection dbUpdateDefinitionsConfigSection = DbUpdateDefinitionsConfigSection.GetConfig();
            Assert.True(true);
        }
    }
}