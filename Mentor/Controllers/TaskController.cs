using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels.TaskRelated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Controllers
{
    public class TaskController : Controller
    {

        private ITaskService _taskService;
        private IDatabaseWorker _databaseWorker;
        private IAuthentication _authentication;

        public TaskController(ITaskService taskService, IDatabaseWorker databaseWorker, IAuthentication authentication) 
        {
            _taskService = taskService;
            _databaseWorker = databaseWorker;
            _authentication = authentication;
        }

        [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
        [HttpGet]
        public async Task<IActionResult> IndexTeacher(int taskId)
        {

            // check task id
            Models.Task task = _databaseWorker.GetTaskById(taskId);
            IEnumerable<N_To_N_TaskStudent> n_to_ns = await _taskService.GetStudentTasks(task);

            StudentThatPassedViewModel model = new StudentThatPassedViewModel
            {
                TaskStudents = n_to_ns,
                Task = task
            };

            return View(model);
        }

        [Authorize(Roles = RoleInitializer.ROLE_STUDENT)]
        [HttpPost]
        public async Task<IActionResult> SendSolutionOnTask(StatusTaskViewModel model) {

            // check task id

            Student student = await _authentication.GetCurrentStudentAsync();
            Models.Task task = _databaseWorker.GetTaskById(model.TaskId);

            // create answer
            await _taskService.SendSolutionOnTask(student, task, model.UploadedFile);

            return RedirectToAction("IndexStudent", new { taskId = model.TaskId });
        }

        [Authorize(Roles = RoleInitializer.ROLE_STUDENT)]
        [HttpGet]
        public async Task<IActionResult> IndexStudent(int taskId)
        {

            Student student = await _authentication.GetCurrentStudentAsync();
            Models.Task task = _databaseWorker.GetTaskById(taskId);

            // check taskId

            N_To_N_TaskStudent taskStudent = _taskService.GetTaskStudent(task, student);
            StatusTaskViewModel model = new StatusTaskViewModel
            { 
                Task = task,
                TaskStudent = taskStudent,
                PresentTime = DateTime.Now,
            };

            return View(model);
        }

        [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
        [HttpGet]
        public IActionResult AddTask(int subjectId)
        {
            AddTaskViewModel model = new AddTaskViewModel { SubjectId = subjectId };
            return View(model);
        }

        [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
        [HttpPost]
        public IActionResult AddTask(AddTaskViewModel model)
        {
            _taskService.AddTaskToSubject(model);
            return RedirectToAction("IndexTeacher", "Subject", new { subjectId = model.SubjectId });
        }

        public IActionResult PageMark()
        {
            return View();
        }

    }
}