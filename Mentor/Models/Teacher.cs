using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public int DepartamentId { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public virtual Departament Departament { get; set; }
        public virtual User User { get; set; }
    }
}
