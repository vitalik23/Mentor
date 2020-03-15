using Mentor.Models;
using Mentor.ViewModels.TaskRelated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface ITaskService
    {
        void AddTaskToSubject(AddTaskViewModel model);
        void DeleteTask();

        IEnumerable<Models.Task> GetTasksRelatedToSubject(int subjectId);
        IEnumerable<Models.Task> GetTasksRelatedToSubject(Subject subject);

    }
}
