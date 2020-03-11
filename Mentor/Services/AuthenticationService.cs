using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Services
{
    public class AuthenticationService : IAuthentication
    {

        private readonly DataBaseContext _dataBaseContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthenticationService(DataBaseContext dataBaseContext, UserManager<User> userManager, SignInManager<User> signInManager) {
            _dataBaseContext = dataBaseContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public bool CreateStudentUser(string userId, int groupId)
        {

            Group group = _dataBaseContext.Group.FirstOrDefault(p => p.Id == groupId);

            if (group == null)
            {
                return false;
            }
            else 
            {
                Student student = new Student { UserId = userId, GroupId = groupId };
                _dataBaseContext.Student.Add(student);
                _dataBaseContext.SaveChanges();

                return true;
            }

    
        }

        public bool CreateTeacherUser(string userId, int departmentId, int positionId)
        {

            Department department = _dataBaseContext.Departament.FirstOrDefault(p => p.Id == departmentId);
            Position position = _dataBaseContext.Position.FirstOrDefault(p => p.Id == positionId);

            if (department == null || position == null)
            {
                return false;
            }
            else 
            {
                Teacher teacher = new Teacher { UserId = userId, DepartamentId = departmentId, PositionId = positionId };

                _dataBaseContext.Teacher.Add(teacher);
                _dataBaseContext.SaveChanges();

                return true;
            }

        }

        public async Task<IdentityResult> CreateUserAsync(RegisterUserViewModel model)
        {
            User user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                Name = model.Name,
                Surname = model.Surname,
                RegistrationDate = DateTime.Now,
                IsAccepted = false
            };

            // добавляем пользователя
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            return result;
        }

        public async System.Threading.Tasks.Task DeleteUserAsync(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
        }

        public async System.Threading.Tasks.Task DeleteUserAsync(User user) {
            await _userManager.DeleteAsync(user);
        }

        public async Task<User> FindUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async System.Threading.Tasks.Task SignInAsync(User user)
        {
            await _signInManager.SignInAsync(user, false);
        }

        public async Task<SignInResult> SignInAsync(LoginUserViewModel model)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            return result;
        }

        public async System.Threading.Tasks.Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
