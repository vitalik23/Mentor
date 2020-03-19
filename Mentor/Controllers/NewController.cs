using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Interfaces;
using Mentor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentor.Controllers
{
    public class NewController : Controller
    {
        private DataBaseContext db;

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

            return RedirectToAction("AllNew","New");
        }

        [HttpGet]
        [ActionName("DeleteNew")]
        public async Task<IActionResult> ConfirmDeleteNew(int? id)
        {
            if (id != null)
            {
                New nw = await db.New.FirstOrDefaultAsync(p => p.Id == id);
                if (nw != null)
                    return View(nw);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteNew(int? id)
        {
            if (id != null)
            {
                New nw = await db.New.FirstOrDefaultAsync(p => p.Id == id);
                if (nw != null)
                {
                    db.New.Remove(nw);
                    await db.SaveChangesAsync();
                    return RedirectToAction("AllNew","New");
                }
            }
            return NotFound();
        }

        public IActionResult AllNew()
        {
            return View(db.New.ToList());
        }

    }
}