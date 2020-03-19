using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mentor.Models;

namespace Mentor.Controllers
{
    public class HomeController : Controller
    {
        private DataBaseContext db;

        public HomeController(DataBaseContext db)
        {
            this.db = db;
        }

        //Home page
        public IActionResult Index()
        {
            return View(db.New.ToList());
        }



    }
}
