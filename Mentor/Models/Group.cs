using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DepartamentId { get; set; }

        public virtual Department Departament { get; set; }


    }
}
