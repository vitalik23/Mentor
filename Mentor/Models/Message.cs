using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Text { get; set; }
        public DateTime CreationTime { get; set; }
        public bool Direction { get; set; }

        public virtual Chat Chat { get; set; }
    }
}
