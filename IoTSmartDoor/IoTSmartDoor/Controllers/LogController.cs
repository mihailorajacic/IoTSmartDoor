using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTSmartDoor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTSmartDoor.Controllers
{
    [Authorize]
    public class LogController : Controller
    {
        private ILogRepository _logRepository;

        public LogController(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public IActionResult List()
        {
            var logs = new List<Log>();
            return View(logs);
        }

        [HttpPost]
        public IActionResult DeviceLog()
        {

            return Ok();
        }
    }
}