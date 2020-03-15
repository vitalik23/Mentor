using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels;
using Mentor.ViewModels.SubjectRelated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers
{
    public class SubjectController : Controller
    {

        private IDatabaseWorker _databaseWorker;
        private IAuthentication _authentication;
        private ISubjectService _subjectService;

        public SubjectController(IDatabaseWorker databaseWorker, IAuthentication authentication, ISubjectService subjectService) {
            _databaseWorker = databaseWorker;
            _authentication = authentication;
            _subjectService = subjectService;

            Console.WriteLine(" SUBJECT CONTROLLER CONSTRUCTOR ");
        }

        public IActionResult AllCourse()
        {
            return View();
        }

        [Authorize(Roles = RoleInitializer.ROLE_STUDENT)]
        public async Task<IActionResult> IndexStudent(int subjectId)
        {

            Student student = await _authentication.GetCurrentStudentAsync();

            if (!_subjectService.DoesSubjectBelongsToStudent(subjectId, student.Id))
            {
                return RedirectToAction("Profile", "Student");
            }

            Subject subject = _databaseWorker.GetSubjectById(subjectId);
            SubjectInfoViewModel model = new SubjectInfoViewModel { Subject = subject };

            return View(model);
        }

        [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
        public async Task<IActionResult> IndexTeacher(int subjectId)
        {

            Teacher teacher = await _authentication.GetCurrentTeacherAsync();

            if (!_subjectService.DoesSubjectBelongsToTeacher(subjectId, teacher.Id)) {
                return RedirectToAction("Profile", "Teacher");
            }

            Subject subject = _databaseWorker.GetSubjectById(subjectId);
            SubjectInfoViewModel model = new SubjectInfoViewModel { Subject = subject };

            return View(model);
        }

        [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
        public async Task<IActionResult> AddStudentToSubject(int subjectId, int studentId = -1)
        {

            if (studentId != -1) 
            {

                // studentId checking

                Console.WriteLine(" idddd = " + studentId);
                await _subjectService.AddStudentToSubject(subjectId, studentId);
            }

            Teacher teacher = await _authentication.GetCurrentTeacherAsync();

            if (!_subjectService.DoesSubjectBelongsToTeacher(subjectId, teacher.Id))
            {
                return RedirectToAction("IndexTeacher");
            }

            Subject subject = _databaseWorker.GetSubjectById(subjectId);
            IEnumerable<Student> availableStudents = await _subjectService.GetStudentsWithoutThisSubject(subject);

            StudentSubjectViewModel model = new StudentSubjectViewModel { Subject = subject, Students = availableStudents };

            return View(model);
        }

        [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
        public async Task<IActionResult> AddTeacherToSubject(int subjectId, int teacherId = -1)
        {

            if (teacherId != -1)
            {

                // studentId checking

                Console.WriteLine(" idddd Teacher = " + teacherId);
                await _subjectService.AddTeacherToSubject(subjectId, teacherId);
            }

            Teacher teacher = await _authentication.GetCurrentTeacherAsync();

            if (!_subjectService.DoesSubjectBelongsToTeacher(subjectId, teacher.Id))
            {
                return RedirectToAction("IndexTeacher");
            }

            Subject subject = _databaseWorker.GetSubjectById(subjectId);
            IEnumerable<Teacher> availableTeachers = await _subjectService.GetTeachersWithoutThisSubject(subject);

            TeacherSubjectViewModel model = new TeacherSubjectViewModel { Subject = subject, Teachers = availableTeachers };

            return View(model);
        }



        public async Task<IActionResult> ListTeacher(int subjectId)
        {
            Teacher teacher = await _authentication.GetCurrentTeacherAsync();

            if (!_subjectService.DoesSubjectBelongsToTeacher(subjectId, teacher.Id))
            {
                return RedirectToAction("IndexTeacher");
            }

            Subject subject = _databaseWorker.GetSubjectById(subjectId);

            TeacherSubjectViewModel model = new TeacherSubjectViewModel { Subject = subject, Teachers = await _subjectService.GetTeachersBySubject(subject) };

            return View(model);
        }

        public async Task<IActionResult> ListStudent(int subjectId)
        {
            Teacher teacher = await _authentication.GetCurrentTeacherAsync();

            if (!_subjectService.DoesSubjectBelongsToTeacher(subjectId, teacher.Id))
            {
                return RedirectToAction("IndexTeacher");
            }

            Subject subject = _databaseWorker.GetSubjectById(subjectId);

            StudentSubjectViewModel model = new StudentSubjectViewModel { Subject = subject, Students = await _subjectService.GetStudentsBySubject(subject) };

            return View(model);
        }


        [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
        public IActionResult DeleteTeacherFromSubject(int subjectId, int teacherId) 
        {
            _subjectService.DeleteTeacherFromSubject(subjectId, teacherId);
            return RedirectToAction("ListTeacher", new { subjectId = subjectId});
        }

        [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
        public IActionResult DeleteStudentFromSubject(int subjectId, int studentId)
        {
            _subjectService.DeleteStudentFromSubject(subjectId, studentId);
            return RedirectToAction("ListStudent", new { subjectId = subjectId });
        }

    }
}