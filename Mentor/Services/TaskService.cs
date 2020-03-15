using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels.TaskRelated;
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

        public TaskService(DataBaseContext dataBaseContext, IFileService fileService)
        {
            _dataBaseContext = dataBaseContext;
            _fileService = fileService;
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
    }
}
