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

        private readonly DataBaseContext _baseContext;
        private readonly UserManager<User> _userManager;

        public AdminController(IAuthentication authentication, IDatabaseWorker databaseWorker, UserManager<User> userManager, DataBaseContext baseContext)
        {
            _authentication = authentication;
            _databaseWorker = databaseWorker;
            _userManager = userManager;
            _baseContext = baseContext;

        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult FacultyOrDepartment()
        {
            return View();
        }

        //Methods for Faculties
        [HttpGet]
        public ViewResult Faculties()
        {
            IEnumerable<Faculty> faculties = _databaseWorker.GetFaculties();

            var allFaculties = new FacultyViewModel
            {
                AllFaculties = faculties
            };
            return View(allFaculties);
        }

        [HttpGet]
        public ViewResult AddFaculty()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddFaculcty(AddFacultyViewModel model)
        {
            Faculty faculty = new Faculty
            {
                Name = model.Name
            };

            _baseContext.Faculty.Add(faculty);
            _baseContext.SaveChanges();

            return RedirectToAction("Faculties");
        }

        [HttpGet]
        public IActionResult DeleteFaculty(int id)
        {
            var faculty = _baseContext.Faculty.FirstOrDefault(i => i.Id == id);
            _baseContext.Faculty.Remove(faculty);
            _baseContext.SaveChanges();

            return RedirectToAction("Faculties");
        }


        //Methods for Users
        [HttpGet]
        public async Task<ViewResult> EditUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View();
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Surname = user.Surname,
                Name = user.Name,
                Patronymic = user.Patronymic,
                PhoneNumber = user.PhoneNumber,
                IsAccepted = user.IsAccepted
                
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return View("Error");
            }
            else
            {

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