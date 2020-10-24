using System;
using System.Collections.Generic;
using Configurator.Web.Entity;

namespace Configurator.Web.Interfaces
{
    public interface IConfigService
    {
        IList<Configuration> GetConfigs();
        void AddConfig(Configuration newConfig);
        void UpdateConfig(Configuration updatedConfig);
        void DeleteConfig(int Id);
        Configuration GetConfig(int Id);
    }
}
