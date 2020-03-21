using Mentor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.PickupUserRelated
{
    public class PickupUserViewModel
    {
        // get ->
        public IEnumerable<User> Users { get; set; }

        public IEnumerable<SelectListItem> FacultiesSelectItems { get; set; }
        public IEnumerable<SelectListItem> DepartmentSelectItems { get; set; }
        public IEnumerable<SelectListItem> GroupSelectItems { get; set; }


        // post <-
        public int ChoosenFacultyId { get; set; }
        public int ChoosenDepartmentId { get; set; }
        public int ChoosenGroupId { get; set; }

    }
}
