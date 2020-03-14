
using System.Collections.Generic;
using Mentor.Models;

namespace Mentor.ViewModels.SubjectRelated
{
    public class TeacherSubjectViewModel
    {
        public Subject Subject { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}
