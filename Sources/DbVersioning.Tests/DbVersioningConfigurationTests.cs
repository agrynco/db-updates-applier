#region Usings
using Lib.Data.DbVersioning.Configuration;
using NUnit.Framework;
#endregion

namespace Lib.Data.DbVersioning.Tests
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