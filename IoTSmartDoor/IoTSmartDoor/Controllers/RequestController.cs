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
    public class RequestController : Controller
    {
        private IRequestRepository _requestRpository;

        public RequestController(IRequestRepository requestRepository)
        {
            _requestRpository = requestRepository;
        }

        public IActionResult List()
        {
            return View();
        }
    }
}