using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.TaskRelated
{
    public class AddTaskViewModel
    {
        public int SubjectId { get; set; }
        public string TaskName { get; set; }
        public DateTime Deadline { get; set; }
        public IFormFile UploadedFile { get; set; }

    }
}
