using Mentor.Models;
using System.Collections.Generic;

namespace Mentor.ViewModels.SubjectRelated
{
    public class StudentSubjectViewModel
    {
        public Subject Subject { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
