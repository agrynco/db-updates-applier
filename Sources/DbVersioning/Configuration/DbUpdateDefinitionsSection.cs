#region Usings
using System.Configuration;
#endregion

namespace DbVersioning.Configuration
{
    public class DbUpdateDefinitionsConfigSection : ConfigurationSection
    {
        #region Properties (public)
        [ConfigurationProperty("items", IsRequired = true)]
        public DbUpdateDefinitionCollection DbUpdateDefinitionElements
        {
            get { return base["items"] as DbUpdateDefinitionCollection; }
        }
        #endregion

        #region Static Methods (public)
        public static DbUpdateDefinitionsConfigSection GetConfig()
        {
            return (DbUpdateDefinitionsConfigSection) ConfigurationManager.GetSection("dbUpdateDefinitions");
        }
        #endregion
    }
}