using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.StudentRelated
{
    public class StudentProfileViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }

    }
}
