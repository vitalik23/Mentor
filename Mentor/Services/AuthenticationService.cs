using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mentor.Services
{
    public class AuthenticationService : IAuthentication
    {

        private readonly DataBaseContext _dataBaseContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _httpControllerAccessor;

        public AuthenticationService(IHttpContextAccessor httpControllerAccessor, DataBaseContext dataBaseContext, UserManager<User> userManager, SignInManager<User> signInManager) {
            _httpControllerAccessor = httpControllerAccessor;
            _dataBaseContext = dataBaseContext;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public async Task<bool> CreateAdminUserAsync(string userId)
        {
            if(userId == null)
            {
                return false;
            }
            else
            {
                User user = await _userManager.FindByIdAsync(userId);
                await _userManager.AddToRoleAsync(user, RoleInitializer.ROLE_ADMIN);
                
                return true;
            }
        }

        public async Task<bool> CreateStudentUserAsync(string userId, int groupId)
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

                // attach role to this user
                User user = await _userManager.FindByIdAsync(userId);
                await _userManager.AddToRoleAsync(user, RoleInitializer.ROLE_STUDENT);

                return true;
            }

    
        }

        public async Task<bool> CreateTeacherUserAsync(string userId, int departmentId, int positionId)
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

                // attach role to this user
                User user = await _userManager.FindByIdAsync(userId);
                await _userManager.AddToRoleAsync(user, RoleInitializer.ROLE_TEACHER);

                return true;
            }

        }

        public async Task<IdentityResult> CreateUserAsync(RegisterUserViewModel model)
        {
            User user = new User
            {
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                Name = model.Name,
                Surname = model.Surname,
                Patronymic = model.Patronymic,
                RegistrationDate = DateTime.Now,
                IsAccepted = false,
                AvatarPath = "",
                Birthday = model.Birthday
                
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

        public async Task<User> FindUserByIdAsync(string userId) {
            return await _userManager.FindByIdAsync(userId);
        }

        public async System.Threading.Tasks.Task DeleteUserAsync(User user) {
            await _userManager.DeleteAsync(user);
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<Teacher> GetCurrentTeacherAsync()
        {
            User user = await GetCurrentUserAsync();
            Teacher teacher = _dataBaseContext.Teacher.FirstOrDefault(x => x.UserId == user.Id);
            teacher.User = user;

            return teacher;

        }

        public async Task<Student> GetCurrentStudentAsync()
        {
            User user = await GetCurrentUserAsync();
            Student student = _dataBaseContext.Student.FirstOrDefault(x => x.UserId == user.Id);
            student.User = user;
            student.Group = _dataBaseContext.Group.FirstOrDefault(x => x.Id == student.GroupId);

            return student;

        }

        public async Task<User> GetCurrentUserAsync()
        {
            string userId = _httpControllerAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(userId); 

            return user;
        }


        public Department GetTeachersDepartment(Teacher teacher)
        {
            return _dataBaseContext.Departament.FirstOrDefault(x=> x.Id == teacher.DepartamentId);
        }

        public Position GetTeachersPosition(Teacher teacher)
        {
            return _dataBaseContext.Position.FirstOrDefault(x => x.Id == teacher.PositionId);
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

        public async Task<bool> IsInRole(User user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }
    }
}
