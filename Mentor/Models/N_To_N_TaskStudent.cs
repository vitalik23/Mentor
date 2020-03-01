using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Models
{
    public class N_To_N_TaskStudent
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int StudentId { get; set; }
        public string Reference_To_Answer { get; set; }
        public int Mark { get; set; }

        public virtual Task Task { get; set; }
        public virtual Student Student { get; set; }

    }
}
