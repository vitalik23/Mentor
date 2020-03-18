using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface IAuthentication
    {
        Task<bool> CreateTeacherUserAsync(string userId, int departmentId, int positionId);
        Task<bool> CreateStudentUserAsync(string userId, int groupId);
        Task<bool> CreateAdminUserAsync(string userId);
        Task<IdentityResult> CreateUserAsync(RegisterUserViewModel model);
        Task<User> FindUserByEmailAsync(string email);
        Task<User> FindUserByIdAsync(string userId);
        System.Threading.Tasks.Task SignInAsync(User user);
        Task<Microsoft.AspNetCore.Identity.SignInResult> SignInAsync(LoginUserViewModel model);
        System.Threading.Tasks.Task SignOutAsync();
        System.Threading.Tasks.Task DeleteUserAsync(string userId);
        System.Threading.Tasks.Task DeleteUserAsync(User user);
        Task<User> GetCurrentUserAsync();
        Task<Teacher> GetCurrentTeacherAsync();
        Task<Student> GetCurrentStudentAsync();

        Department GetTeachersDepartment(Teacher teacher);
        Position GetTeachersPosition(Teacher teacher);

        Task<bool> IsInRole(User user, string role);

    }
}
