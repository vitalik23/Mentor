using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers
{
    public class AdminController : Controller
    {

        private IDatabaseWorker _databaseWorker;

        public AdminController(IDatabaseWorker databaseWorker)
        {
            _databaseWorker = databaseWorker;
        }

        [HttpGet]
        public ViewResult Users()
        {
            IEnumerable<User> users = _databaseWorker.GetUsers();
            var allUsers = new UsersViewModel
            {
                GetAllUsers = users
            };

            return View(allUsers);
        }
    }
}