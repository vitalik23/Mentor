using Mentor.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.TaskRelated
{
    public class StatusTaskViewModel
    {

        // for get
        public N_To_N_TaskStudent TaskStudent { get; set; }
        public Models.Task Task { get; set; }
        public DateTime PresentTime { get; set; }


        // for post
        public IFormFile UploadedFile { get; set; }
        public int TaskId { get; set; }
        public int SubjectId { get; set; }

    }
}
