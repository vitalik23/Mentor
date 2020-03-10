using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        private readonly SignInManager<User> _signInManager;

        public AccountController(IAuthentication authentication, UserManager<User> userManager, SignInManager<User> signInManager) 
        {
            _authentication = authentication;
            _userManager =    userManager;
            _signInManager =  signInManager;
        }

        [HttpGet]
        public IActionResult ChooseUserType() => View();
        [HttpGet]
        public IActionResult StudentRegister() {

            RegisterStudentViewModel model = new RegisterStudentViewModel { Groups = _databaseWorker.GetAllGroups()};
            return View(model);
        }
        [HttpGet]
        public IActionResult TeacherRegister() {

            RegisterTeacherViewModel model = new RegisterTeacherViewModel { Departments = _databaseWorker.GetAllDepartments(),
                                                                            Positions = _databaseWorker.GetAllPositions()};

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
             
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
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

            if (ModelState.IsValid)
            {

                Console.WriteLine("is valid");

                User user = new User
                {
                    Email = registerUserViewModel.Email,
                    UserName = registerUserViewModel.Email,
                    Name     = registerUserViewModel.Name,
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
                    if (!_authentication.CreateStudentUser(us.Id, registerUserViewModel.GroupId)) {

                        IdentityResult identityResult = await _userManager.DeleteAsync(us);
                        ModelState.AddModelError(string.Empty, "Choosen group does not exists");

                        return View(registerUserViewModel);
                    }

                    // установка куки
                    await _signInManager.SignInAsync(user, false);

                    Console.WriteLine(" Succeeded");

                    return RedirectToAction("Index", "Home");
                    //   


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
        public async Task<IActionResult> TeacherRegister(RegisterTeacherViewModel registerUserViewModel)
        {
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
                    await _signInManager.SignInAsync(user, false);

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
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
