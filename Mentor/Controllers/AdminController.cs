using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers
{
    public class AdminController : Controller
    {

        private IDatabaseWorker _databaseWorker;
        private IAuthentication _authentication;

        private DataBaseContext _baseContext;
        private readonly UserManager<User> _userManager;

        public AdminController(IAuthentication authentication, IDatabaseWorker databaseWorker, UserManager<User> userManager, DataBaseContext baseContext)
        {
            _authentication = authentication;
            _databaseWorker = databaseWorker;
            _userManager = userManager;
            _baseContext = baseContext;

        }

        [HttpGet]
        public async Task<ViewResult> Edit(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View();
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                //UserName = user.UserName,
                Surname = user.Surname,
                Name = user.Name,
                Patronymic = user.Patronymic,
                PhoneNumber = user.PhoneNumber,
                IsAccepted = user.IsAccepted
                
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return View("Error");
            }
            else
            {
                //user.UserName = model.UserName;
                user.Surname = model.Surname;
                user.Name = model.Name;
                user.Patronymic = model.Patronymic;
                user.PhoneNumber = model.PhoneNumber;
                user.IsAccepted = model.IsAccepted;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
                }

                return View(model);
            }
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