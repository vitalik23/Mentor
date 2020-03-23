using Mentor.Interfaces;
using Mentor.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Controllers
{
    public class UserController : Controller
    {
        private IAuthentication _authentication;

        public UserController(IAuthentication authentication)
        {
            _authentication = authentication;
        }


        public async Task<IActionResult> Index(string userId) 
        {
            User givenUser = await _authentication.FindUserByIdAsync(userId);
            return View(givenUser);
        }
    }
}
