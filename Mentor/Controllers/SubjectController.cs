using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers
{
    public class SubjectController : Controller
    {
        public IActionResult AllCourse()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}