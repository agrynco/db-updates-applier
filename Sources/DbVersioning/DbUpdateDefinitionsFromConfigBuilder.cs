#region Usings
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using AGrynCo.Lib;
using AGrynCo.Lib.Data.DataProviders;
using DbVersioning.Configuration;
#endregion

namespace DbVersioning
{
    public class DbUpdateDefinitionsFromConfigBuilder
    {
        #region Methods (public)
        public IEnumerable<DbUpdateSourceDefinition> BuildDbUpdateSourceDefinitions()
        {
            DbUpdateDefinitionsConfigSection dbUpdateDefinitionsConfigSectionGroup = DbUpdateDefinitionsConfigSection.GetConfig();
            DbUpdateSourceDefinition[] definitions = new DbUpdateSourceDefinition[dbUpdateDefinitionsConfigSectionGroup.DbUpdateDefinitionElements.Count];
            for (int i = 0; i < definitions.Length; i++)
            {
                definitions[i] = BuildDefinition(dbUpdateDefinitionsConfigSectionGroup.DbUpdateDefinitionElements[i]);
            }

            return definitions;
        }
        #endregion

        #region Static Methods (private)
        private static ICurrentDbVersionDetector BuildCurrentDbVersionDetector(CurrentDbVersionDetectorConfigElement currentDbVersionDetectorConfigElement, IDataProvider dataProvider)
        {
            Type type = AssemblyScanner.Search(currentDbVersionDetectorConfigElement.TypeName);

            List<object> parameters = new List<object>(new object[] {dataProvider});

            foreach (KeyValueConfigurationElement keyValueConfigurationElement in currentDbVersionDetectorConfigElement.Params)
            {
                parameters.Add(keyValueConfigurationElement.Value);
            }

            ICurrentDbVersionDetector result = (ICurrentDbVersionDetector) Activator.CreateInstance(type, parameters.ToArray());

            return result;
        }

        private static IDataProvider BuildDataProvider(DataProviderConfigElement dataProviderConfigElement)
        {
            Assembly.Load(dataProviderConfigElement.AssemblyName);
            Type type = AssemblyScanner.Search(dataProviderConfigElement.TypeName);
            IDataProvider result = (IDataProvider) Activator.CreateInstance(type, dataProviderConfigElement.ConnectionString);
            return result;
        }

        private static IDbUpdateBuilder BuildDbUpdateBuilder(TypeConfigElement typeConfigElement)
        {
            Type type = AssemblyScanner.Search(typeConfigElement.TypeName);
            return (IDbUpdateBuilder) Activator.CreateInstance(type);
        }

        private static IDbUpdateExecutor BuildDbUpdateExecuter(DbUpdateExecuterConfigElement dbUpdateExecuterConfigElement, IDataProvider dataProvider)
        {
            Assembly.Load(dbUpdateExecuterConfigElement.AssemblyName);
            Type type = AssemblyScanner.Search(dbUpdateExecuterConfigElement.TypeName);
            Type typeOfDbVersionIdentifier = AssemblyScanner.Search(dbUpdateExecuterConfigElement.TypeOfDbVersionIdentifier);

            Type genericType = type.MakeGenericType(typeOfDbVersionIdentifier);

            IDbUpdateExecutor result = (IDbUpdateExecutor) Activator.CreateInstance(genericType, dataProvider);
            result.ExecutionTimeOut = dbUpdateExecuterConfigElement.ExecutionTimeOut;

            return result;
        }

        private static IDbUpdateLoader BuildDbUpdateLoader(TypeConfigElement typeConfigElement)
        {
            Type type = AssemblyScanner.Search(typeConfigElement.TypeName);
            return (IDbUpdateLoader) Activator.CreateInstance(type);
        }

        private static IDBUpdatesScanner BuildDbUpdatesScanner(DbUpdatesScannerConfigElement dbUpdatesScannerConfigElement)
        {
            Type type = AssemblyScanner.Search(dbUpdatesScannerConfigElement.TypeName);

            List<object> parameters = new List<object>();

            foreach (KeyValueConfigurationElement keyValueConfigurationElement in dbUpdatesScannerConfigElement.Params)
            {
                parameters.Add(keyValueConfigurationElement.Value);
            }

            IDBUpdatesScanner result = (IDBUpdatesScanner) Activator.CreateInstance(type, parameters.ToArray());

            return result;
        }

        private static IDatabaseManager BuildDatabaseManager(DatabaseManagerConfigElement databaseManagerConfigElement, IDataProvider dataProvider)
        {
            Type type = AssemblyScanner.Search(databaseManagerConfigElement.TypeName);

            IDatabaseManager databaseManager = (IDatabaseManager) Activator.CreateInstance(type, dataProvider.Connection.ConnectionString);
            return databaseManager;
        }

        private static DbUpdateSourceDefinition BuildDefinition(DbUpdateDefinitionConfigElement dbUpdateDefinitionConfigElement)
        {
            Type typeOfDbUpdate = AssemblyScanner.Search(dbUpdateDefinitionConfigElement.DbUpdate.TypeName);
            Type typeOfDbVersionIdentifier = AssemblyScanner.Search(dbUpdateDefinitionConfigElement.DbUpdate.TypeOfDbVersionIdentifier);
            typeOfDbUpdate = typeOfDbUpdate.MakeGenericType(typeOfDbVersionIdentifier);

            IDataProvider dataProvider = BuildDataProvider(dbUpdateDefinitionConfigElement.DataProviderConfigElement);
            ICurrentDbVersionDetector currentDbVersionDetector = BuildCurrentDbVersionDetector(dbUpdateDefinitionConfigElement.CurrentDbVersionDetectorConfigElement, dataProvider);
            IDBUpdatesScanner dbUpdatesScanner = BuildDbUpdatesScanner(dbUpdateDefinitionConfigElement.DbUpdatesScannerConfigElement);
            IDbUpdateLoader dbUpdateLoader = BuildDbUpdateLoader(dbUpdateDefinitionConfigElement.DbUpdateLoader);
            IDbUpdateExecutor dbUpdateExecutor = BuildDbUpdateExecuter(dbUpdateDefinitionConfigElement.DbUpdateExecuterConfigElement, dataProvider);
            IDbUpdateBuilder dbUpdateBuilder = BuildDbUpdateBuilder(dbUpdateDefinitionConfigElement.DbUpdateBuilder);

            IDatabaseManager databaseManager = BuildDatabaseManager(dbUpdateDefinitionConfigElement.DatabaseManager, dataProvider);

            DbUpdateSourceDefinition result = new DbUpdateSourceDefinition(
                typeOfDbUpdate,
                currentDbVersionDetector,
                dbUpdatesScanner,
                dbUpdateLoader,
                dbUpdateBuilder,
                dbUpdateExecutor,
                databaseManager);
            return result;
        }
        #endregion
    }
}