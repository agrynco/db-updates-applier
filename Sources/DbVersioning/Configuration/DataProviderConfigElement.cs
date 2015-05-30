#region Usings
using System.Configuration;
#endregion

namespace Lib.Data.DbVersioning.Configuration
{
    public class DataProviderConfigElement : TypeConfigElement
    {
        #region Properties (public)
        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return base["connectionString"] as string; }
            set { base["connectionString"] = value; }
        }

        [ConfigurationProperty("assemblyName", IsRequired = true)]
        public string AssemblyName
        {
            get { return base["assemblyName"] as string; }
            set { base["assemblyName"] = value; }
        }
        #endregion
    }
}