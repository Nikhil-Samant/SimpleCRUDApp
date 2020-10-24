using System;
using System.Collections.Generic;
using System.Linq;
using Configurator.Web.Data;
using Configurator.Web.Entity;
using Configurator.Web.Interfaces;

namespace Configurator.Web.Services
{
    public class ConfigService : IConfigService
    {
        private readonly ConfigurationDbContext _context;
        public ConfigService(ConfigurationDbContext context)
        {
            _context = context;
        }
        public void AddConfig(Configuration newConfig)
        {
            try
            {
                _context.Configurations.Add(newConfig);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void UpdateConfig(Configuration updatedConfig)
        {
            try
            {
                var configToBeUpdated = _context.Configurations.Where(c => c.Id == updatedConfig.Id).SingleOrDefault();
                configToBeUpdated.ConfigurationName = updatedConfig.ConfigurationName;
                configToBeUpdated.ConfigType = updatedConfig.ConfigType;
                configToBeUpdated.Config = updatedConfig.Config;
                _context.Configurations.Update(configToBeUpdated);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DeleteConfig(int Id)
        {
            try
            {
                var configToBeDeleted = _context.Configurations.Where(c => c.Id == Id).SingleOrDefault();
                if (configToBeDeleted != null)
                {
                    _context.Configurations.Remove(configToBeDeleted);
                    _context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Configuration GetConfig(int Id)
        {
            return _context.Configurations.Where(c => c.Id == Id).SingleOrDefault();
        }

        public IList<Configuration> GetConfigs()
        {
            return _context.Configurations.ToList();
        }
    }
}
