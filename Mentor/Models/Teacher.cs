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
        public int PositionId { get; set; }
        public string UserId { get; set; }
        public bool IsAdmin { get; set; }
        public virtual Department Departament { get; set; }
        public virtual User User { get; set; }
        public virtual Position Position { get; set; }
    }


}
