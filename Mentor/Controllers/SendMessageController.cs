using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Models;
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

        //public IActionResult SendEmailDefault()
        //{
        //    service.SendEmailDefault();
        //    return RedirectToAction("Index","Home");
        //}

        //[HttpPost]
        //public async Task<IActionResult> SendMessage()
        //{
        //    try
        //    {
        //        EmailService emailService = new EmailService();
        //        await emailService.SendEmailAsync(/*Емайл кому отправляете*/"saminindima113@gmail.com", /*Тема сообщения*/"Hello", /*"Тест письма: тест!"*/ "Hello");
        //        return RedirectToAction("Index","Home");
        //    }
        //    catch
        //    {
        //        return RedirectToAction("Index","Home");
        //    }

        //}
    }
}