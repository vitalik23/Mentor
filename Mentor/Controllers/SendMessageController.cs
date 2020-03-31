using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers
{
    public class SendMessageController : Controller
    {
        private readonly User user;
        public SendMessageController(User user)
        {
            this.user = user;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string email,string subject,string textMessage)
        {
            try
            {
                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(/*Емайл кому отправляете*/email, /*Тема сообщения*/subject, /*"Тест письма: тест!"*/ textMessage);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index","Home");
            }
            
        }
    }
}