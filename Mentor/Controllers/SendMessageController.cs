using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Models;
using Mentor.ViewModels.UserRelated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Mentor.Controllers
{
    public class SendMessageController : Controller
    {
        private readonly EmailService service;
        private readonly ILogger<HomeController> _logger;

        public SendMessageController(ILogger<HomeController> logger, EmailService service)
        {
            _logger = logger;
            this.service = service;
        }

        public IActionResult SendEmailDefault(UserMessageViewModel model)
        {
            service.SendEmailDefault(model.Email, model.Subject, model.TextMessage);
            return RedirectToAction("Index", "Home");
        }

        
    }
}