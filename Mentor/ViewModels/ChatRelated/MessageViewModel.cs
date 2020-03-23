using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.ChatRelated
{
    public class MessageViewModel
    {
        public string CurrentUserId { get; set; }
        public string OpositeUserId { get; set; }

        public string Message { get; set; }

    }
}
