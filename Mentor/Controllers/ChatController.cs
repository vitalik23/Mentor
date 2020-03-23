using Mentor.Interfaces;
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

        public ChatController(IAuthentication authentication)
        {
            _authentication = authentication;
        }

        public async Task<IActionResult> Index(string oppositeUserId)
        {
            User oppositeUser = await _authentication.FindUserByIdAsync(oppositeUserId);
            User currentUser  = await _authentication.GetCurrentUserAsync();

            ChatHistoryViewModel model;

            return View();
        }
    }
}
