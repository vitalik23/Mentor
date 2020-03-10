using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Controllers
{
    public class AccountController : Controller
    {

        private IAuthentication _authentication;
        private IDatabaseWorker _databaseWorker;

        private readonly UserManager<User> _userManager;

        public AccountController(IAuthentication authentication, IDatabaseWorker databaseWorker, UserManager<User> userManager) 
        {
            _authentication = authentication;
            _userManager =    userManager;
            _databaseWorker = databaseWorker;
        }

        private List<SelectListItem> populateGroups() {
            IEnumerable<Group> groups = _databaseWorker.GetAllGroups();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Group group in groups)
            {
                items.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
            }

            return items;
        }
        private List<SelectListItem> populateDepartments() {
            IEnumerable<Department> departments = _databaseWorker.GetAllDepartments();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Department department in departments)
            {
                items.Add(new SelectListItem { Text = department.Name, Value = department.Id.ToString() });
            }

            return items;
        }
        private List<SelectListItem> populatePositions()
        {
            IEnumerable<Position> positions = _databaseWorker.GetAllPositions();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Position position in positions)
            {
                items.Add(new SelectListItem { Text = position.Name, Value = position.Id.ToString() });
            }

            return items;
        }


        [HttpGet]
        public IActionResult ChooseUserType() => View();
        [HttpGet]
        public IActionResult StudentRegister() {

            RegisterStudentViewModel model = new RegisterStudentViewModel { GroupItems = populateGroups()};
            return View(model);
        }

        [HttpGet]
        public IActionResult TeacherRegister() {

            RegisterTeacherViewModel model = new RegisterTeacherViewModel { DeparmentItems = populateDepartments(),
                                                                            PositionItems = populatePositions()};

            return View(model);
        } 

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginUserViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            if (ModelState.IsValid)
            {

                Microsoft.AspNetCore.Identity.SignInResult result = await _authentication.SignInAsync(model);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> StudentRegister(RegisterStudentViewModel registerUserViewModel)
        {

            Console.WriteLine("GroupId = " + registerUserViewModel.GroupId);
            Console.WriteLine("Name = " + registerUserViewModel.Name);

            registerUserViewModel.GroupItems = populateGroups();

            if (_databaseWorker.GroupExists(registerUserViewModel.GroupId))
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await _authentication.CreateUserAsync(registerUserViewModel);

                    if (result.Succeeded)
                    {
                        User user = await _authentication.FindUserByEmail(registerUserViewModel.Email);
                        if (!_authentication.CreateStudentUser(user.Id, registerUserViewModel.GroupId))
                        {
                            await _authentication.DeleteUserAsync(user);
                            return View(registerUserViewModel);
                        }

                        // установка куки
                        await _authentication.SignInAsync(user);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Choosen Group does not exists!");
            }
           

            Console.WriteLine("is not valid or upper else group does not exists!");

            return View(registerUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> TeacherRegister(RegisterTeacherViewModel registerUserViewModel)
        {

            registerUserViewModel.DeparmentItems = populateDepartments();
            registerUserViewModel.PositionItems = populatePositions();

            if (ModelState.IsValid)
            {

                Console.WriteLine("is valid");

                User user = new User
                {
                    Email = registerUserViewModel.Email,
                    UserName = registerUserViewModel.Email,
                    Name = registerUserViewModel.Name,
                    Surname = registerUserViewModel.Surname,
                    RegistrationDate = DateTime.Now,
                    //      Birthday = registerUserViewModel.Birthday,
                    IsAccepted = false
                };
                

                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, registerUserViewModel.Password);
                if (result.Succeeded)
                {

                    User us = await _userManager.FindByEmailAsync(user.Email);
                    _authentication.CreateTeacherUser(us.Id, registerUserViewModel.DepartmentId, registerUserViewModel.PositionId);

                    // установка куки
                    await _authentication.SignInAsync(user);

                    Console.WriteLine(" Succeeded");

                    return RedirectToAction("Index", "Home");

                }

                else
                {
                    Console.WriteLine(" not Succeeded");
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            Console.WriteLine("is not valid");
            return View(registerUserViewModel);

        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _authentication.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
