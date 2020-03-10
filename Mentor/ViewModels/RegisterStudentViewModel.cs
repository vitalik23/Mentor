using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels
{
    public class RegisterStudentViewModel : RegisterUserViewModel
    {
        public int GroupId { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
