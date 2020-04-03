using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.ViewModels.UserRelated
{
    public class UserMessageViewModel
    {
        public User User { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string TextMessage { get; set; }


    }
}
