using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels
{
    public class UsersViewModel
    {
        public IEnumerable<User> AllUsers { get; set; }
    }
}
