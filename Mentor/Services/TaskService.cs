using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels.TaskRelated;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Services
{
    public class TaskService : ITaskService
    {

        private DataBaseContext _dataBaseContext;
        private IFileService _fileService;
        private IAuthentication _authentication;

        public TaskService(DataBaseContext dataBaseContext, IFileService fileService, IAuthentication authentication)
        {
            _dataBaseContext = dataBaseContext;
            _fileService = fileService;
            _authentication = authentication;
        }

        public void AddTaskToSubject(AddTaskViewModel model)
        {
            Models.Task task = new Models.Task { Name = model.TaskName, SubjectId = model.SubjectId};
            _dataBaseContext.Task.Add(task);
            _dataBaseContext.SaveChanges();

            _fileService.UploadTaskFile(task, model.UploadedFile);

        }

        public void DeleteTask()
        {
            throw new NotImplementedException();
        }


        
        public IEnumerable<Models.Task> GetTasksRelatedToSubject(int subjectId)
        {
            IEnumerable<Models.Task> tasks = _dataBaseContext.Task.Where(x => x.SubjectId == subjectId);
            return tasks;
        }

        public IEnumerable<Models.Task> GetTasksRelatedToSubject(Subject subject)
        {
            return GetTasksRelatedToSubject(subject.Id);
        }

        public N_To_N_TaskStudent GetTaskStudent(Models.Task task, Student student)
        {
            return GetTaskStudent(task.Id, student.Id);
        }

        public N_To_N_TaskStudent GetTaskStudent(int taskId, int studentId)
        {
            return _dataBaseContext.N_To_N_TaskStudent.FirstOrDefault(x => x.TaskId == taskId && x.StudentId == studentId);
        }



        public async System.Threading.Tasks.Task SendSolutionOnTask(Student student, Models.Task task, IFormFile uploadedFile)
        {

            N_To_N_TaskStudent model = new N_To_N_TaskStudent 
            {
                MarkValue = -1,
                StudentId = student.Id,
                TaskId = task.Id,
                LoadTime = DateTime.Now
                
            };

            RemoveSolutionOnTask(model);

            _dataBaseContext.N_To_N_TaskStudent.Add(model);
            _dataBaseContext.SaveChanges();

            await _fileService.UploadTaskSolutionFile(model, uploadedFile);

        }


        public async Task<IEnumerable<N_To_N_TaskStudent>> GetStudentTasks(Models.Task task)
        {
            return await GetStudentTasks(task.Id);
        }
        // for teacher
        public async Task<IEnumerable<N_To_N_TaskStudent>> GetStudentTasks(int taskId)
        {
            IEnumerable<N_To_N_TaskStudent> models
                = new List<N_To_N_TaskStudent>(_dataBaseContext.N_To_N_TaskStudent.Where(x => x.TaskId == taskId));

            foreach (N_To_N_TaskStudent model in models)
            {
                model.Student = _dataBaseContext.Student.FirstOrDefault(c => c.Id == model.StudentId);
                model.Student.Group = _dataBaseContext.Group.FirstOrDefault(v => v.Id == model.Student.GroupId);
                model.Student.User = await _authentication.FindUserByIdAsync(model.Student.UserId);

            }

            return models;
        }

        public void RemoveSolutionOnTask(N_To_N_TaskStudent __model)
        {
            N_To_N_TaskStudent model = 
                _dataBaseContext.N_To_N_TaskStudent.FirstOrDefault(x => x.TaskId == __model.TaskId 
                                                                           && x.StudentId == __model.StudentId);

            if (model != null) 
            {
                _fileService.DeleteFile(model.SolutionPath);
                _dataBaseContext.Remove(model);
                _dataBaseContext.SaveChanges();
            }

        }
    }
}
