using System;
using System.Collections.Generic;
using Configurator.Web.Entity;

namespace Configurator.Web.Interfaces
{
    public interface IConfigService
    {
        IList<Configuration> GetConfigs();
        void AddConfig(Configuration newConfig);
        void DeleteConfig(int Id);
        Configuration GetConfig(int Id);
    }
}
