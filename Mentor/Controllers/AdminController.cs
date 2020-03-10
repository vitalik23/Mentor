using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers
{
    public class AdminController : Controller
    {

        private IDatabaseWorker _databaseWorker;
        private IAuthentication _authentication;

        public AdminController(IAuthentication authentication, IDatabaseWorker databaseWorker)
        {
            _authentication = authentication;
            _databaseWorker = databaseWorker;
        }

        [HttpGet]
        public ViewResult Users()
        {
            IEnumerable<User> users = _databaseWorker.GetUsers();
            var allUsers = new UsersViewModel
            {
                AllUsers = users
            };

            return View(allUsers);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id != null) {
                await _authentication.DeleteUserAsync(id);
            }

            return RedirectToAction("Users");
        }
    }
}