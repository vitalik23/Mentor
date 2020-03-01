using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Reference_To_Theory { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }

    }
}
