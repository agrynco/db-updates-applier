#region Usings
using System.Configuration;
#endregion

namespace Lib.Data.DbVersioning.Configuration
{
    public class DbUpdateDefinitionsConfigSection : ConfigurationSection
    {
        #region Static Methods (public)
        public static DbUpdateDefinitionsConfigSection GetConfig()
        {
            return (DbUpdateDefinitionsConfigSection) ConfigurationManager.GetSection("dbUpdateDefinitions");
        }
        #endregion

        #region Properties (public)
        [ConfigurationProperty("items", IsRequired = true)]
        public DbUpdateDefinitionCollection DbUpdateDefinitionElements
        {
            get { return base["items"] as DbUpdateDefinitionCollection; }
        }
        #endregion
    }
}