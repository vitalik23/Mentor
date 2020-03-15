using Mentor.Interfaces;
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

        public TaskController(ITaskService taskService) 
        {
            _taskService = taskService;
        }

        [Authorize(Roles = RoleInitializer.ROLE_TEACHER)]
        [HttpGet]
        public IActionResult IndexTeacher(int taskId)
        {
            
            return View();

        }

        [Authorize(Roles = RoleInitializer.ROLE_STUDENT)]
        [HttpGet]
        public IActionResult IndexStudent(int taskId)
        {

            return View();
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

    }
}
