using Mentor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.AdminsRelated
{
    public class GroupViewModel
    {
        public string Name { get; set; }

        public IEnumerable<Group> AllGroups { get; set; }

        public List<SelectListItem> DepartmentItems { get; set; }

        public int DepartmentId { get; set; }
    }
}
