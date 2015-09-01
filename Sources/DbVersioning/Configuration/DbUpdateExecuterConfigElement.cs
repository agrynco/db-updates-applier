#region Usings
using System.Configuration;
#endregion

namespace DbVersioning.Configuration
{
    public class DbUpdateExecuterConfigElement : TypeConfigElement
    {
        #region Properties (public)
        [ConfigurationProperty("assemblyName", IsRequired = true)]
        public string AssemblyName
        {
            get { return base["assemblyName"] as string; }
            set { base["assemblyName"] = value; }
        }

        [ConfigurationProperty("executionTimeOut", IsRequired = true)]
        public int? ExecutionTimeOut
        {
            get { return base["executionTimeOut"] as int?; }
            set { base["executionTimeOut"] = value; }
        }

        [ConfigurationProperty("typeOfDbVersionIdentifier", IsRequired = true)]
        public string TypeOfDbVersionIdentifier
        {
            get { return base["typeOfDbVersionIdentifier"] as string; }
            set { base["typeOfDbVersionIdentifier"] = value; }
        }
        #endregion
    }
}