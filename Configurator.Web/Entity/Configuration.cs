using System;
using System.Collections.Generic;

namespace Configurator.Web.Entity
{
    public class Configuration
    {
        public int Id { get; set; }
        public string ConfigurationName { get; set; }
        public string ConfigType { get; set; }
        public string Config { get; set; }
    }
}
