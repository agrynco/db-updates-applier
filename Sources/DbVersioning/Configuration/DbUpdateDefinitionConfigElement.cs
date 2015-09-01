#region Usings
using System.Configuration;
#endregion

namespace DbVersioning.Configuration
{
    public class DbUpdateDefinitionConfigElement : ConfigurationElement
    {
        #region Properties (public)
        [ConfigurationProperty("dataProvider", IsRequired = true)]
        public DataProviderConfigElement DataProviderConfigElement
        {
            get { return base["dataProvider"] as DataProviderConfigElement; }
        }

        [ConfigurationProperty("dbUpdateExecuter", IsRequired = true)]
        public DbUpdateExecuterConfigElement DbUpdateExecuterConfigElement
        {
            get { return base["dbUpdateExecuter"] as DbUpdateExecuterConfigElement; }
        }

        [ConfigurationProperty("dbUpdatesScanner", IsRequired = true)]
        public DbUpdatesScannerConfigElement DbUpdatesScannerConfigElement
        {
            get { return base["dbUpdatesScanner"] as DbUpdatesScannerConfigElement; }
        }

        [ConfigurationProperty("currentDbVersionDetector", IsRequired = true)]
        public CurrentDbVersionDetectorConfigElement CurrentDbVersionDetectorConfigElement
        {
            get { return base["currentDbVersionDetector"] as CurrentDbVersionDetectorConfigElement; }
        }

        [ConfigurationProperty("dbUpdateBuilder", IsRequired = true)]
        public TypeConfigElement DbUpdateBuilder
        {
            get { return base["dbUpdateBuilder"] as TypeConfigElement; }
        }

        [ConfigurationProperty("dbUpdateLoader", IsRequired = true)]
        public TypeConfigElement DbUpdateLoader
        {
            get { return base["dbUpdateLoader"] as TypeConfigElement; }
        }

        [ConfigurationProperty("databaseManager", IsRequired = true)]
        public DatabaseManagerConfigElement DatabaseManager
        {
            get { return base["databaseManager"] as DatabaseManagerConfigElement; }
        }

        [ConfigurationProperty("dbUpdate", IsRequired = true)]
        public DbUpdateConfigElement DbUpdate
        {
            get { return base["dbUpdate"] as DbUpdateConfigElement; }
        }

        [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
        public string Key
        {
            get { return base["key"] as string; }
            set { base["key"] = value; }
        }
        #endregion
    }
}