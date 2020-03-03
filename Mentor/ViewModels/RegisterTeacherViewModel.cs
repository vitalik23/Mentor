using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels
{
    public class RegisterTeacherViewModel : RegisterUserViewModel
    {
        public int DepartmentId { get; set; } 
        public int PositionId { get; set; }

    }
}
