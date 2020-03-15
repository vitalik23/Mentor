using Mentor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.AdminsRelated
{
    public class DepartmentViewModel
    {
        public string Name { get; set; }

        public List<SelectListItem> FacultyItems { get; set; }

        public int FacultyId { get; set; }
    }
}
