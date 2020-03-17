using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels;
using Mentor.ViewModels.AdminsRelated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mentor.Controllers
{
    [Authorize(Roles = RoleInitializer.ROLE_ADMIN)]
    public class AdminController : Controller
    {

        private IDatabaseWorker _databaseWorker;
        private IAuthentication _authentication;

        private readonly DataBaseContext _baseContext;
        private readonly UserManager<User> _userManager;

        public AdminController(IAuthentication authentication, IDatabaseWorker databaseWorker, UserManager<User> userManager, DataBaseContext baseContext)
        {
            _authentication = authentication;
            _databaseWorker = databaseWorker;
            _userManager = userManager;
            _baseContext = baseContext;

        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult FacultyOrDepartment()
        {
            return View();
        }

        public ViewResult Error()
        {
            return View();
        }

        ///--------------------------------------------------------------------
        private List<SelectListItem> PopulateFaculties()
        {
            IEnumerable<Faculty> faculties = _databaseWorker.GetFaculties();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Faculty faculty in faculties)
            {
                items.Add(new SelectListItem { Text = faculty.Name, Value = faculty.Id.ToString() });
            }

            return items;
        }
        private List<SelectListItem> PopulateDepartments()
        {
            IEnumerable<Department> departments = _databaseWorker.GetAllDepartments();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Department department in departments)
            {
                items.Add(new SelectListItem { Text = department.Name, Value = department.Id.ToString() });
            }

            return items;
        }
        ///--------------------------------------------------------------------

        ///Methods for Groups
        public ViewResult Groups()
        {
            IEnumerable<Group> groups = new List<Group>(_databaseWorker.GetAllGroups());

            foreach (Group group in groups)
            {
                group.Departament = _baseContext.Departament.FirstOrDefault(x => x.Id == group.DepartamentId);
            }

            return View(groups);
        }

        [HttpGet]
        public ViewResult CreateGroup()
        {
            GroupViewModel model = new GroupViewModel { DepartmentItems = PopulateDepartments() };
            return View(model);
        }

        [HttpPost, ActionName("CreateGroupConfirmed")]
        public ActionResult CreateGroup(GroupViewModel group)
        {
            if(group.DepartmentId == 0)
            {
                return RedirectToAction("Error");
            }

            Group newGroup = new Group
            {
                Name = group.Name,
                DepartamentId = group.DepartmentId
            };

            _baseContext.Group.Add(newGroup);
            _baseContext.SaveChanges();

            return RedirectToAction("Groups");
        }

        [HttpGet]
        public ActionResult DeleteGroup(int id)
        {
            Group group = _baseContext.Group.FirstOrDefault(i => i.Id == id);
            _baseContext.Group.Remove(group);
            _baseContext.SaveChanges();

            return RedirectToAction("Groups");
        }
        ///--------------------------------------------------------------------

        //Methods for Departments
        [HttpGet]
        public ViewResult Departments()
        {
            IEnumerable<Department> departments = new List<Department>(_databaseWorker.GetAllDepartments());

            foreach (Department department in departments)
            {
                department.Faculty = _baseContext.Faculty.FirstOrDefault(x => x.Id == department.FacultyId);
            }

            return View(departments);
        }

        [HttpGet]
        public ViewResult СreateDepartment()
        {
            DepartmentViewModel model = new DepartmentViewModel { FacultyItems = PopulateFaculties() };
            return View(model);
        }

        [HttpPost, ActionName("CreateDepartmentConfirmed")]
        public ActionResult CreateDepartment(DepartmentViewModel department)
        {
            if(department.FacultyId == 0)
            {
                return RedirectToAction("Error");
            }
            Department newDepartment = new Department
            {
                Name = department.Name,
                FacultyId = department.FacultyId
            };
            _baseContext.Departament.Add(newDepartment);
            _baseContext.SaveChanges();

            return RedirectToAction("Departments");
        }

        [HttpGet]
        public IActionResult DeleteDepartment(int id)
        {
            Department department = _baseContext.Departament.FirstOrDefault(i => i.Id == id);
            _baseContext.Departament.Remove(department);
            _baseContext.SaveChanges();

            return RedirectToAction("Departments");
        }
        ///--------------------------------------------------------------------

        //Methods for Faculties
        [HttpGet]
        public ViewResult Faculties()
        {
            IEnumerable<Faculty> faculties = _databaseWorker.GetFaculties();

            var allFaculties = new FacultyViewModel
            {
                AllFaculties = faculties
            };
            return View(allFaculties);
        }

        [HttpGet]
        public ViewResult СreateFaculty()
        {
            return View();
        }

        [HttpPost, ActionName("СreateFacultyConfirmed")]
        public ActionResult СreateFaculty(Faculty faculty)
        {
            _baseContext.Faculty.Add(faculty);
            _baseContext.SaveChanges();

            return RedirectToAction("Faculties");
        }

        [HttpGet]
        public IActionResult DeleteFaculty(int id)
        {
            var faculty = _baseContext.Faculty.FirstOrDefault(i => i.Id == id);
            _baseContext.Faculty.Remove(faculty);
            _baseContext.SaveChanges();

            return RedirectToAction("Faculties");
        }
        ///--------------------------------------------------------------------

        //Methods for Positions
        [HttpGet]
        public ViewResult Positions()
        {
            IEnumerable<Position> positions = _databaseWorker.GetAllPositions();
            return View(positions);
        }

        [HttpGet]
        public ViewResult CreatePosition()
        {
            return View();
        }

        [HttpPost, ActionName("СreatePositionConfirmed")]
        public ActionResult CreatePosition(Position position) 
        {
            _baseContext.Position.Add(position);
            _baseContext.SaveChanges();

            return RedirectToAction("Positions");
        }

        [HttpGet]
        public IActionResult DeletePosition(int id)
        {
            Position position = _baseContext.Position.FirstOrDefault(i =>i.Id == id);
            _baseContext.Position.Remove(position);
            _baseContext.SaveChanges();

            return RedirectToAction("Positions");
        }
        ///--------------------------------------------------------------------

        //Methods for Users
        [HttpGet]
        public ViewResult Users()
        {
            IEnumerable<User> users = _databaseWorker.GetUsers();
            var allUsers = new UsersViewModel
            {
                AllUsers = users
            };

            return View(allUsers);
        }

        [HttpGet]
        public async Task<ViewResult> EditUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return View();
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                Surname = user.Surname,
                Name = user.Name,
                Patronymic = user.Patronymic,
                PhoneNumber = user.PhoneNumber,
                IsAccepted = user.IsAccepted

            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if (user == null)
            {
                return View("Error");
            }
            else
            {

                user.Surname = model.Surname;
                user.Name = model.Name;
                user.Patronymic = model.Patronymic;
                user.PhoneNumber = model.PhoneNumber;
                user.IsAccepted = model.IsAccepted;

                var result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Users");
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id != null)
            {
                await _authentication.DeleteUserAsync(id);
            }

            return RedirectToAction("Users");
        }
        ///--------------------------------------------------------------------
    }
}