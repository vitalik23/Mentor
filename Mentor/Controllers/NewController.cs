using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Interfaces;
using Mentor.Models;
using Mentor.Services;
using Mentor.ViewModels; // пространство имен моделей представлений
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mentor.Controllers
{
    [Authorize(Roles = RoleInitializer.ROLE_ADMIN)]
    public class NewController : Controller
    {
        private DataBaseContext _db;
        private IDatabaseWorker _databaseWorker;
        private INewService _newService;
        public NewController(DataBaseContext db, IDatabaseWorker databaseWorker, INewService newService)
        {
            _db = db;
            _databaseWorker = databaseWorker;
            _newService = newService;
        }

        [HttpGet]
        public IActionResult AddNew()
        {
            return View();
        }

        public async Task<IActionResult> AddNew(New nw)
        {
            nw.TimeNew = DateTime.Now;
            await _newService.AddNew(nw);
            return RedirectToAction("AllNew", "New");
        }
        
        public IActionResult DeleteNew(int id)
        {
            _newService.DeleteNew(id);
            return RedirectToAction("AllNew", "New");
        }

        public IActionResult AllNew()
        {
      
            return View(_db.New.ToList());
        }

    }
}