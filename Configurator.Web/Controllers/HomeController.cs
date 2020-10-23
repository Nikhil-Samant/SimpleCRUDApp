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

        public IActionResult Index()
        {
            var model = new ConfigurationViewModel();
            model.SavedConfigs = _service.GetConfigs();
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
            var newConfig = new Configuration(){
                ConfigurationName = model.ConfigurationName,
                ConfigType = model.ConfigType,
                Config = JsonSerializer.Serialize(model.Config)
            };
            _service.AddConfig(newConfig);
            return View("Index");
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
