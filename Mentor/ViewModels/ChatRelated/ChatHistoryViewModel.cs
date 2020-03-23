using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.ChatRelated
{
    public class ChatHistoryViewModel
    {
        public User CurrentUser { get; set; }
        public User OpositeUser { get; set; }
        
        public List<Message> Messages { get; set; }

    }
}
