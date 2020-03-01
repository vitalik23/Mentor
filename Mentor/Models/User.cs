using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Models
{
    public class User : IdentityUser
    {
        public string Surname { get; set; }
        public string Patronymic { get; set; }
    }
}
