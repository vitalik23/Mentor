using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers
{
    [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
    public class TeacherController : Controller
    {
        public IActionResult TeacherProfile()
        {
            return View();
        }
        

    }
}