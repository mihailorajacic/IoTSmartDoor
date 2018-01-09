using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoTSmartDoor.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IoTSmartDoor.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}