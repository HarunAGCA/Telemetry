using Futek.Telemetry.Hardware.Abstract;
using Futek.Telemetry.Hardware.Concrete;
using Futek.Telemetry.MvcWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;

namespace Futek.Telemetry.MvcWebUI.Controllers
{
    [Route("[controller]")]
    public class ConfigurationController : Controller
    {

        [HttpGet]
        [Route("[action]")]
        public IActionResult Configure()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Configure(ConfigurationViewModel configurationViewModel)
        {
            return RedirectToAction("configure", "Connection",new { portName = configurationViewModel.PortName, baudRate = configurationViewModel.BaudRate });
        }

    }
}
