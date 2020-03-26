using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels
{
    public class UsersViewModel
    {
        public List<User> AllUsers { get; set; }

        public string Name { get; set; }
        
        public string Surname { get; set; }

        public string Patronymic { get; set; }
    }
}
