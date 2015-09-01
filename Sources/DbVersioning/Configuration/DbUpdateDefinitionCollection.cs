#region Usings
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
#endregion

namespace DbVersioning.Configuration
{
    [ConfigurationCollection(typeof (DbUpdateDefinitionConfigElement), AddItemName = "dbUpdateDefinition")]
    public class DbUpdateDefinitionCollection : ConfigurationElementCollection, IEnumerable<DbUpdateDefinitionConfigElement>
    {
        #region Properties (public)
        public DbUpdateDefinitionConfigElement this[int index]
        {
            get { return BaseGet(index) as DbUpdateDefinitionConfigElement; }
        }
        #endregion

        #region IEnumerable<DbUpdateDefinitionConfigElement> Methods
        public IEnumerator<DbUpdateDefinitionConfigElement> GetEnumerator()
        {
            return (from i in Enumerable.Range(0, Count) select this[i]).GetEnumerator();
        }
        #endregion

        #region Methods (protected)
        protected override ConfigurationElement CreateNewElement()
        {
            return new DbUpdateDefinitionConfigElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            var dbUpdateDefinitionConfigElement = element as DbUpdateDefinitionConfigElement;

            if (dbUpdateDefinitionConfigElement != null)
            {
                return dbUpdateDefinitionConfigElement.Key;
            }

            return null;
        }
        #endregion
    }
}