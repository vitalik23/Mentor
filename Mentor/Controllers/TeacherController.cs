using Mentor.Interfaces;
using Mentor.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mentor.Controllers
{
    [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
    public class TeacherController : Controller
    {
        IAuthentication _authentication;
        IDatabaseWorker _databaseWorker;

        public TeacherController(IAuthentication authentication,IDatabaseWorker databaseWorker)
        {
            _authentication = authentication;
            _databaseWorker = databaseWorker;

        }

        public async Task<IActionResult> TeacherProfile()
        {
            User currentUser = await _authentication.GetCurrentUser();
            
            return View(currentUser);
        }

        public IActionResult AddCourse()
        {
            return View();
        }

    }
}