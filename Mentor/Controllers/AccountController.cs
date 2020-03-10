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
        public IActionResult ChooseUserType() => View();


        public IActionResult StudentRegister() => View();
        public IActionResult TeacherRegister() => View();

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
             
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
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
                var result = await this.userManager.CreateAsync(user, registerUserViewModel.Password);
                if (result.Succeeded)
                {
                    // установка куки

                    await this.signInManager.SignInAsync(user, false);

                    Console.WriteLine(" Succeeded");

                    return RedirectToAction("Index", "Home");
                    //   User us = await userManager.FindByEmailAsync(user.Email);


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
                var result = await this.userManager.CreateAsync(user, registerUserViewModel.Password);
                if (result.Succeeded)
                {
                    // установка куки

                    await this.signInManager.SignInAsync(user, false);

                    Console.WriteLine(" Succeeded");

                    return RedirectToAction("Index", "Home");
                    //   User us = await userManager.FindByEmailAsync(user.Email);


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
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
