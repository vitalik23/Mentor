using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }

        public virtual User User1 { get; set; }
        public virtual User User2 { get; set; }

    }
}
