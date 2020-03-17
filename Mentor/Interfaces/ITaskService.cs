using Mentor.Models;
using Mentor.ViewModels.TaskRelated;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task AddTaskToSubject(AddTaskViewModel model);
        void DeleteTask(int taskId);
        void DeleteTask(Models.Task task);

        IEnumerable<Models.Task> GetTasksRelatedToSubject(int subjectId);
        IEnumerable<Models.Task> GetTasksRelatedToSubject(Subject subject);

        N_To_N_TaskStudent GetTaskStudent(Models.Task task, Student student);
        N_To_N_TaskStudent GetTaskStudent(int taskId, int studentId);
        Task<IEnumerable<N_To_N_TaskStudent>> GetStudentTasks(Models.Task task);
        Task<IEnumerable<N_To_N_TaskStudent>> GetStudentTasks(int taskId);

        System.Threading.Tasks.Task SendSolutionOnTask(Student student, Models.Task task, IFormFile uploadedFile);

        void RemoveSolutionOnTask(N_To_N_TaskStudent model);

        void SaveMarkOnTaskOfStudent(N_To_N_TaskStudent model);

        void DeleteTaskMark(int taskId, int studentId);

    }
}
