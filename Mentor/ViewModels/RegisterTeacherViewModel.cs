using Mentor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels
{
    public class RegisterTeacherViewModel : RegisterUserViewModel
    {

        public List<SelectListItem> DeparmentItems { get; set; }
        public List<SelectListItem> PositionItems { get; set; }

        public int DepartmentId { get; set; } 
        public int PositionId { get; set; }

    }
}
