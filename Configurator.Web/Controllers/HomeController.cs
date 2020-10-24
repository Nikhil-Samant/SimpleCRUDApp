using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Configurator.Web.Models;
using Configurator.Web.Entity;
using Configurator.Web.Interfaces;
using System.Text.Json;

namespace Configurator.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfigService _service;
        public HomeController(ILogger<HomeController> logger, IConfigService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index(int messageType = 0, string message = "")
        {
            var model = new ConfigurationViewModel();
            model.SavedConfigs = _service.GetConfigs();
            ViewBag.MessageResult = "";
            if(messageType == 1){
                ViewBag.MessageResult = "success";
            }
            else if (messageType == 2){
                ViewBag.MessageResult = "failed";
            }
            ViewBag.Message = message;
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NewConfiguration()
        {
            var model = new ConfigurationViewModel();
            return View(model);
        }

        public IActionResult AddConfiguration(ConfigurationViewModel model)
        {
            try
            {
                var newConfig = new Configuration()
                {
                    ConfigurationName = model.ConfigurationName,
                    ConfigType = model.ConfigType,
                    Config = JsonSerializer.Serialize(model.Config)
                };
                _service.AddConfig(newConfig);
                return RedirectToAction("Index", new { messageType = 1, message = $"{newConfig.ConfigurationName}.{newConfig.ConfigType} saved successfully to database." });
            }
            catch (Exception)
            {
                return RedirectToAction("Index",new { messageType = 2, message = $"Something went wrong while inserting into the DB" });
            }
        }

        public IActionResult EditConfiguration(int id)
        {
            var config = _service.GetConfig(id);
            var model = new ConfigurationViewModel()
            {
                Id = config.Id,
                ConfigurationName = config.ConfigurationName,
                ConfigType = config.ConfigType,
                Config = JsonSerializer.Deserialize<Dictionary<string, string>>(config.Config)
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult EditConfiguration(ConfigurationViewModel model)
        {
            try
            {
                var updatedConfig = new Configuration()
                {
                    Id = model.Id,
                    ConfigurationName = model.ConfigurationName,
                    ConfigType = model.ConfigType,
                    Config = JsonSerializer.Serialize(model.Config)
                };
                _service.UpdateConfig(updatedConfig);
                return RedirectToAction("Index", new { messageType = 1, message = $"{updatedConfig.ConfigurationName}.{updatedConfig.ConfigType} edited successfully to database." });
            }
            catch (Exception)
            {
                return RedirectToAction("Index", new { messageType = 2, message = $"Something went wrong while updating the DB"});
            }

        }

        public IActionResult Delete(int id)
        {
            _service.DeleteConfig(id);
            return RedirectToAction("Index", new { messageType = 1, message = "Configuration Deleted Successfully."});
        }

        public IActionResult GenerateReport()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
