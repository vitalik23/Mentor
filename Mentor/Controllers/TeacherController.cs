using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mentor.Controllers
{
    [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
    public class TeacherController : Controller
    {


        IAuthentication _authentication;
        IDatabaseWorker _databaseWorker;
        ISubjectService _subjectService;

        public TeacherController(IAuthentication authentication, IDatabaseWorker databaseWorker, ISubjectService subjectService)
        {

         //   Console.WriteLine(" TEACHER CONTROLLER CONSTRUCTOR ");

            _authentication = authentication;
            _databaseWorker = databaseWorker;
            _subjectService = subjectService;

        }

        public async Task<IActionResult> Profile()
        {
            
            User currentUser = await _authentication.GetCurrentUserAsync();
            Teacher currentTeacher = await _authentication.GetCurrentTeacherAsync();
            Department department = _authentication.GetTeachersDepartment(currentTeacher);
            Position position = _authentication.GetTeachersPosition(currentTeacher);

            IEnumerable<Subject> subjects = _subjectService.GetSubjectsByTeacher(currentTeacher);

            TeacherProfileViewModel model = new TeacherProfileViewModel { User = currentUser, 
                                                                          Department = department, 
                                                                          Position = position,
                                                                          Subjects = subjects
            };

            return View(model);
        }

        public IActionResult AddSubject()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject(Subject subject) 
        {

            Teacher teacher = await _authentication.GetCurrentTeacherAsync();
            await _subjectService.AddSubject(subject, teacher.Id);

            return RedirectToAction("Profile");
        }

    }
}