using Mentor.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels
{
    public class RegisterStudentViewModel : RegisterUserViewModel
    {
        public int GroupId { get; set; }


        public List<SelectListItem> GroupItems { get; set; }
    }
}
