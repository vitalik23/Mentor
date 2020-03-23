using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.ChatRelated
{
    public class Message
    {
        public string Text { get; set; }
        public DateTime SendingTime { get; set; }
        public bool IsMy { get; set; }
    }
}
