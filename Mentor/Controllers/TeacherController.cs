using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult TeacherProfile()
        {
            return View();
        }


    }
}