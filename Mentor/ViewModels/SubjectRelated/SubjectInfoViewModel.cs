using Mentor.Models;
using System.Collections.Generic;

namespace Mentor.ViewModels.SubjectRelated
{
    public class SubjectInfoViewModel
    {
        public Models.Subject Subject { get; set; }
        public IEnumerable<Task> Tasks { get; set; }
        
    }
}
