using Mentor.ViewModels.PickupUserRelated;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Controllers
{
    public class PickupUserController : Controller
    {


        [HttpGet]
        public IActionResult Index() {

            return View();
        }

        [HttpPost]
        public IActionResult Index(PickupUserViewModel model)
        {

            return View();
        }
    }
}
