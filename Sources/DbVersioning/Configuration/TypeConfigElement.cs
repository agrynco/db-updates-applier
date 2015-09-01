#region Usings
using System.Configuration;
#endregion

namespace DbVersioning.Configuration
{
    public class TypeConfigElement : ConfigurationElement
    {
        [ConfigurationProperty("typeName", IsRequired = true)]
        public string TypeName
        {
            get { return base["typeName"] as string; }
            set { base["typeName"] = value; }
        }
    }
}