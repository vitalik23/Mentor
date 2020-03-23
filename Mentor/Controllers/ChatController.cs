﻿using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels.ChatRelated;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Controllers
{
    public class ChatController : Controller
    {

        private IAuthentication _authentication;
        private IChatService _chatService;

        public ChatController(IAuthentication authentication, IChatService chatService)
        {
            _authentication = authentication;
            _chatService = chatService;
        }

        public async Task<IActionResult> Index(string oppositeUserId)
        {
            User opositeUser = await _authentication.FindUserByIdAsync(oppositeUserId);
            User currentUser  = await _authentication.GetCurrentUserAsync();

            Chat chat = await _chatService.GetOrCreateChat(currentUser, opositeUser); // 93

            // get messages
            List<Message> messages = _chatService.GetMessagesByChat(chat);

            ChatHistoryViewModel model = new ChatHistoryViewModel 
            { 
                CurrentUser = currentUser,
                OpositeUser = opositeUser,
                Messages = messages
            };

            return View(model);
        }

  
    }
}
