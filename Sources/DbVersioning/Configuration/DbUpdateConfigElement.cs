#region Usings
using System.Configuration;
#endregion

namespace DbVersioning.Configuration
{
    public class DbUpdateConfigElement : TypeConfigElement
    {
        #region Properties (public)
        [ConfigurationProperty("typeOfDbVersionIdentifier", IsRequired = true)]
        public string TypeOfDbVersionIdentifier
        {
            get { return base["typeOfDbVersionIdentifier"] as string; }
            set { base["typeOfDbVersionIdentifier"] = value; }
        }
        #endregion
    }
}