using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels.StudentRelated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Controllers
{

    [Authorize(Roles = RoleInitializer.ROLE_STUDENT)]
    public class StudentController : Controller
    {

        private IAuthentication _authentication;
        private ISubjectService _subjectService;

        public StudentController(IAuthentication authentication, ISubjectService subjectService) {
            _authentication = authentication;
            _subjectService = subjectService;
        }

        public async Task<IActionResult> Profile()
        {
            Student currentStudent = await _authentication.GetCurrentStudentAsync();
            IEnumerable<Subject> subjects = _subjectService.GetSubjectsByStudent(currentStudent);

            StudentProfileViewModel model = new StudentProfileViewModel { Student = currentStudent, Subjects = subjects };
            return View(model);
        }

    }
}
