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

        private IAuthentication authentication;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(IAuthentication authentication, UserManager<User> userManager, SignInManager<User> signInManager) 
        {
            this.authentication = authentication;
            this.userManager    =    userManager;
            this.signInManager  =  signInManager;
        }

        [HttpGet]
        public IActionResult Register(string whoIsRegistering) => View(whoIsRegistering);

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if (ModelState.IsValid)
            {

                User user = new User { 
                    Email = registerUserViewModel.Email,
                    UserName = registerUserViewModel.Email,
                    Surname = registerUserViewModel.Surname,
                    Birthday = registerUserViewModel.Birthday,
                    IsAccepted = false
                };
                
                // добавляем пользователя
                var result = await this.userManager.CreateAsync(user, registerUserViewModel.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    await this.signInManager.SignInAsync(user, false);

                    User us = await userManager.FindByEmailAsync(user.Email);

                    if (registerUserViewModel is RegisterStudentViewModel)
                    {
                        RegisterStudentViewModel rsvm = (RegisterStudentViewModel) registerUserViewModel;
                        this.authentication.ConnectStudentToUser(us.Id, rsvm.GroupId);

                    }
                    else if (registerUserViewModel is RegisterTeacherViewModel)
                    {
                        RegisterTeacherViewModel rtvm = (RegisterTeacherViewModel) registerUserViewModel;


                    }
                    else
                    { 
                     /// error
                    }

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

            return View(registerUserViewModel);
        }

    }
}
