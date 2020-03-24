using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels.ChatRelated;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.SignalR
{
    public class ChatHub : Hub
    {

        private IChatService _chatService;

        public ChatHub(IChatService chatService)
        {
            _chatService = chatService;
        }

        public async System.Threading.Tasks.Task Send(MessageViewModel model)
        {
            // find corresponding chat
            Chat chat = _chatService.GetChat(model.CurrentUserId, model.OpositeUserId);

            // create message and save it to db
            await _chatService.AddMessage(chat, model);

            // resend to oposite (receivering) user
            await this.Clients.User(model.OpositeUserId).SendAsync("Send", model);
        }
    }
}
