using System;
using System.Collections.Generic;
using Configurator.Web.Entity;

namespace Configurator.Web.Models
{
    public class ConfigurationViewModel
    {
        public int Id { get; set; }
        public ConfigurationViewModel()
        {
            this.Config = new Dictionary<string, string>();
        }
        public string ConfigurationName { get; set; }

        public string ConfigType { get; set; }

        public Dictionary<string, string> Config { get; set; }

        public IList<Configuration> SavedConfigs { get; set; }
    }
}
