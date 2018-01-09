using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTSmartDoor.Controllers
{
    [Authorize(Roles = "Host")]
    public class GuestController : Controller
    {
        public IActionResult List()
        {
            return View();
        }
    }
}