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
        Task<bool> CreateTeacherUser(string userId, int departmentId, int positionId);
        Task<bool> CreateStudentUser(string userId, int groupId);
        Task<IdentityResult> CreateUserAsync(RegisterUserViewModel model);
        Task<User> FindUserByEmail(string email);
        System.Threading.Tasks.Task SignInAsync(User user);
        Task<Microsoft.AspNetCore.Identity.SignInResult> SignInAsync(LoginUserViewModel model);
        System.Threading.Tasks.Task SignOutAsync();
        System.Threading.Tasks.Task DeleteUserAsync(string userId);
        System.Threading.Tasks.Task DeleteUserAsync(User user);
        Task<User> GetCurrentUser();
        Task<Teacher> GetCurrentTeacher();

        Department GetTeachersDepartment(Teacher teacher);
        Position GetTeachersPosition(Teacher teacher);

    }
}
