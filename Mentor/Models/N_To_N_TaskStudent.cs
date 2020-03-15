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
        public string SolutionPath { get; set; }
        public DateTime LoadTime { get; set; }
        public DateTime CheckTime { get; set; }

        public int MarkValue { get; set; }
        public string MarkDescription { get; set; }

        public virtual Task Task { get; set; }
        public virtual Student Student { get; set; }

    }
}
