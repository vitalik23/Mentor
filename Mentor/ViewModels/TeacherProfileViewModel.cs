using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels
{
    public class TeacherProfileViewModel
    {
        public User User { get; set; }
        public Department Department { get; set; }
        public Position Position { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }
    }
}
