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

        public User User;

        public SelectList Faculties { get; set; }
        public SelectList Departments { get; set; }
        public SelectList Groups { get; set; }

        

    }
}
