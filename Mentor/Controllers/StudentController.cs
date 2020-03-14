using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Controllers
{

    //[Authorize(Roles = RoleInitializer.ROLE_STUDENT)]
    public class StudentController : Controller
    {
        public IActionResult StudentProfile()
        {
            return View();
        }

    }
}
