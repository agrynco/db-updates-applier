﻿#region Usings
using System.Configuration;
#endregion

namespace Lib.Data.DbVersioning.Configuration
{
    public class CurrentDbVersionDetectorConfigElement : TypeConfigElement
    {
        #region Properties (public)
        [ConfigurationProperty("params")]
        public KeyValueConfigurationCollection Params
        {
            get { return (KeyValueConfigurationCollection) base["params"]; }
        }
        #endregion
    }
}