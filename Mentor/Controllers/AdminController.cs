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

        //[HttpGet]
        //public IActionResult DeleteUser(string? id)
        //{
        //    if (id == null)
        //    {
        //        return View("Users");
        //    }
        //    var user = _databaseWorker.GetUsers()
        //        .FirstOrDefault(u => u.Id == id);

        //    return View(user);
        //}

        //[HttpPost, ActionName("DeleteUser")]
        //public RedirectToActionResult DeleteConfirmed(string id)
        //{
        //    var user = _databaseWorker.GetUsers().FirstOrDefault(u => u.Id == id);
        //    _dataBaseContext.Remove(user);
        //    _dataBaseContext.SaveChanges();
        //    return RedirectToAction("Users");
            
        //}
    }
}