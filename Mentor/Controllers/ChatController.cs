using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Controllers
{
    public class ChatController : Controller
    {

        public ChatController() 
        { 

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
