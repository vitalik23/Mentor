using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mentor.Controllers
{
    public class NewController : Controller
    {
        public DataBaseContext db;

        public NewController(DataBaseContext _db)
        {
            db = _db;
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNew(New model)
        {
            db.New.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult DeleteNew()
        {
            return View();
        }
    }
}