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
            ViewData["Title"] = "Кафедры и факультеты";
            return View();
        }

        public ViewResult Error()
        {
            return View();
        }
        ///------------------------Create_users_from_Admin---------------------
        [HttpGet]
        public IActionResult CreateStudent()
        {

            RegisterStudentViewModel model = new RegisterStudentViewModel
            {
                GroupItems = PopulateGroups(),
                Birthday = DateTime.Now
            };
            ViewData["Title"] = "Добавление студента";

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateTeacher()
        {

            RegisterTeacherViewModel model = new RegisterTeacherViewModel
            {
                DeparmentItems = PopulateDepartments(),
                PositionItems = PopulatePositions(),
                Birthday = DateTime.Now
            };
            ViewData["Title"] = "Добавление преподавателя";

            return View(model);
        }

        [HttpGet]
        public IActionResult CreateAdmin()
        {
            ViewData["Title"] = "Добавление администратора";
            return View();
        }

        [HttpPost, ActionName("CreateAdminConfirmed")]
        public async Task<ActionResult> CreateAdmin(RegisterUserViewModel registerUserViewModel)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _authentication.CreateUserAsync(registerUserViewModel);

                if (result.Succeeded)
                {
                    User user = await _authentication.FindUserByEmailAsync(registerUserViewModel.Email);

                    if (!await _authentication.CreateAdminUserAsync(user.Id))
                    {
                        await _authentication.DeleteUserAsync(user);
                        return View("CreateAdmin", registerUserViewModel);
                    }

                    return RedirectToAction("Users");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Выбирете группу для студента");
            }

            return View("CreateAdmin", registerUserViewModel);
        }

        [HttpPost, ActionName("CreateStudentConfirmed")]
        public async Task<ActionResult> CreateStudent(RegisterStudentViewModel registerUserViewModel)
        {
            registerUserViewModel.GroupItems = PopulateGroups();

            if (_databaseWorker.GroupExists(registerUserViewModel.GroupId))
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await _authentication.CreateUserAsync(registerUserViewModel);

                    if (result.Succeeded)
                    {
                        User user = await _authentication.FindUserByEmailAsync(registerUserViewModel.Email);

                        if (!await _authentication.CreateStudentUserAsync(user.Id, registerUserViewModel.GroupId))
                        {
                            await _authentication.DeleteUserAsync(user);
                            return View("CreateStudent", registerUserViewModel);
                        }

                        return RedirectToAction("Users");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Выбирете группу для студента");
            }

            return View("CreateStudent", registerUserViewModel);
        }

        [HttpPost, ActionName("CreateTeacherConfirmed")]
        public async Task<IActionResult> CreateTeacher(RegisterTeacherViewModel registerUserViewModel)
        {

            registerUserViewModel.DeparmentItems = PopulateDepartments();
            registerUserViewModel.PositionItems = PopulatePositions();

            if (_databaseWorker.DepartmentExists(registerUserViewModel.DepartmentId) && _databaseWorker.PositionExists(registerUserViewModel.PositionId))
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await _authentication.CreateUserAsync(registerUserViewModel);


                    if (result.Succeeded)
                    {

                        User user = await _authentication.FindUserByEmailAsync(registerUserViewModel.Email);

                        if (!await _authentication.CreateTeacherUserAsync(user.Id, registerUserViewModel.DepartmentId, registerUserViewModel.PositionId))
                        {
                            await _authentication.DeleteUserAsync(user);
                            return View("CreateTeacher", registerUserViewModel);
                        }

                        return RedirectToAction("Users");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Выбирете кафедру для преподователя");
            }

            return View("CreateTeacher", registerUserViewModel);
        }
        ///--------------------------------------------------------------------


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

        private List<SelectListItem> PopulatePositions()
        {
            IEnumerable<Position> positions = _databaseWorker.GetAllPositions();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Position position in positions)
            {
                items.Add(new SelectListItem { Text = position.Name, Value = position.Id.ToString() });
            }

            return items;
        }

        private List<SelectListItem> PopulateGroups()
        {
            IEnumerable<Group> groups = _databaseWorker.GetAllGroups();
            List<SelectListItem> items = new List<SelectListItem>();

            foreach (Group group in groups)
            {
                items.Add(new SelectListItem { Text = group.Name, Value = group.Id.ToString() });
            }

            return items;
        }
        ///--------------------------------------------------------------------

        ///Methods for Groups
        public ViewResult Groups(GroupViewModel model)
        {
            IEnumerable<Group> groups = new List<Group>(_databaseWorker.GetAllGroups());

            foreach (Group group in groups)
            {
                group.Departament = _baseContext.Departament.FirstOrDefault(x => x.Id == group.DepartamentId);
            }

            model.AllGroups = groups;
            model.DepartmentItems = PopulateDepartments();

            ViewData["Title"] = "Группы";

            return View(model);
        }

        [HttpPost, ActionName("CreateGroupConfirmed")]
        public ActionResult CreateGroup(GroupViewModel group)
        {
            if (ModelState.IsValid)
            {
                Group newGroup = new Group
                {
                    Name = group.Name,
                    DepartamentId = group.DepartmentId
                };

                _baseContext.Group.Add(newGroup);
                _baseContext.SaveChanges();

                return RedirectToAction("Groups");
            }

            group.AllGroups = _databaseWorker.GetAllGroups();
            group.DepartmentItems = PopulateDepartments();

            return View("Groups", group);
            
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
        public ViewResult Departments(DepartmentViewModel model)
        {
            IEnumerable<Department> departments = new List<Department>(_databaseWorker.GetAllDepartments());

            foreach (Department department in departments)
            {
                department.Faculty = _baseContext.Faculty.FirstOrDefault(x => x.Id == department.FacultyId);
            }
            ViewData["Title"] = "Кафедры";

            model.AllDepartments = departments;
            model.FacultyItems = PopulateFaculties();

            return View(model);
        }

        [HttpPost, ActionName("CreateDepartmentConfirmed")]
        public ActionResult CreateDepartment(DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                Department newDepartment = new Department
                {
                    Name = department.Name,
                    FacultyId = department.FacultyId
                };
                _baseContext.Departament.Add(newDepartment);
                _baseContext.SaveChanges();

                return RedirectToAction("Departments");
            }

            department.AllDepartments = _databaseWorker.GetAllDepartments();
            department.FacultyItems = PopulateFaculties();

            return View("Departments", department);
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
            ViewData["Title"] = "Факультеты";
            return View(allFaculties);
        }

        [HttpPost, ActionName("СreateFacultyConfirmed")]
        public ActionResult СreateFaculty(FacultyViewModel model)
        {
            Faculty faculty = new Faculty { Name = model.Name };
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
        public ViewResult Positions(PositionViewModel model)
        {
            ViewData["Title"] = "Должности";
            model.AllPosition = _databaseWorker.GetAllPositions();
            return View(model);
        }

        [HttpPost, ActionName("СreatePositionConfirmed")]
        public ActionResult CreatePosition(PositionViewModel model)
        {
            Position position = new Position { Name = model.Name };
            _baseContext.Position.Add(position);
            _baseContext.SaveChanges();

            return RedirectToAction("Positions");
        }

        [HttpGet]
        public IActionResult DeletePosition(int id)
        {
            Position position = _baseContext.Position.FirstOrDefault(i => i.Id == id);
            _baseContext.Position.Remove(position);
            _baseContext.SaveChanges();

            return RedirectToAction("Positions");
        }
        ///--------------------------------------------------------------------

        //Methods for Users
        [HttpGet]
        public ViewResult Users(string surname, string name, string patronymic)
        {
            ViewData["Title"] = "Все пользователи";
            IEnumerable<User> users = _databaseWorker.GetUsers();

            if (surname != null)
            {
                users = users.Where(u => u.Surname.Contains(surname));

                ViewData["Title"] = "Найденые пользователи";
            }

            if (name != null)
            {
                users = users.Where(u => u.Name.Contains(name));

                ViewData["Title"] = "Найденые пользователи";
            }

            if (patronymic != null)
            {
                users = users.Where(u => u.Patronymic.Contains(patronymic));

                ViewData["Title"] = "Найденые пользователи";
            }

            var allUsers = new UsersViewModel
            {
                AllUsers = users.ToList()
            };

            return View(allUsers);

        }

        [HttpGet]
        public async Task<RedirectToActionResult> Info(string id)
        {
            User user = await _userManager.FindByIdAsync(id);

            Teacher teacher = _baseContext.Teacher.FirstOrDefault(i => i.UserId == user.Id);
            Student student = _baseContext.Student.FirstOrDefault(i => i.UserId == user.Id);

            if (teacher != null)
            {
                Department department = _baseContext.Departament.FirstOrDefault(i => i.Id == teacher.DepartamentId);
                Position position = _baseContext.Position.FirstOrDefault(i => i.Id == teacher.PositionId);

                var model = new TeacherViewModel
                {
                    Name = user.Name,
                    Surname = user.Surname,
                    Patronymic = user.Patronymic,
                    AvatarPath = user.AvatarPath,
                    PhoneNumber = user.PhoneNumber,
                    Email = user.Email,
                    Department = department.Name,
                    Position = position.Name
                };

                return RedirectToAction("TeacherInfo", model);

            }
            if (student != null)
            {
                Group group = _baseContext.Group.FirstOrDefault(i => i.Id == student.GroupId);

                var model = new StudentViewModel
                {

                    Name = user.Name,
                    Surname = user.Surname,
                    Patronymic = user.Patronymic,
                    AvatarPath = user.AvatarPath,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    GroupName = group.Name
                };

                return RedirectToAction("StudentInfo", model);
            }
            return RedirectToAction("Users");
        }

        [HttpGet]
        public ViewResult StudentInfo(StudentViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public ViewResult TeacherInfo(TeacherViewModel model)
        {
            return View(model);
        }

        [HttpGet]
        public async Task<RedirectToActionResult> EditUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);

            Teacher teacher = _baseContext.Teacher.FirstOrDefault(i => i.UserId == user.Id);
            Student student = _baseContext.Student.FirstOrDefault(i => i.UserId == user.Id);

            if (teacher != null)
            {
                var model = new TeacherViewModel
                {
                    Email = user.Email,
                    Surname = user.Surname,
                    Name = user.Name,
                    Patronymic = user.Patronymic,
                    PhoneNumber = user.PhoneNumber,
                    IsAccepted = user.IsAccepted
                };

                return RedirectToAction("EditTeacher", model);
            }
            if (student != null)
            {
                var model = new StudentViewModel
                {
                    Email = user.Email,
                    Surname = user.Surname,
                    Name = user.Name,
                    Patronymic = user.Patronymic,
                    PhoneNumber = user.PhoneNumber,
                    IsAccepted = user.IsAccepted
                };

                return RedirectToAction("EditStudent", model);
            }

            return RedirectToAction("Users");
        }

        [HttpGet]
        public ViewResult EditTeacher(TeacherViewModel model)
        {
            model.DeparmentItems = PopulateDepartments();
            model.PositionItems = PopulatePositions();
            return View(model);
        }

        [HttpGet]
        public ViewResult EditStudent(StudentViewModel model)
        {
            model.GroupItems = PopulateGroups();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditStudentConfirmed(StudentViewModel model)
        {
            model.GroupItems = PopulateGroups();

            if (model.GroupId == 0)
            {
                ModelState.AddModelError(string.Empty, "Выбирете группу для студента");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (ModelState.IsValid)
            {
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
                        Student student = _baseContext.Student.FirstOrDefault(i => i.UserId == user.Id);
                        student.GroupId = model.GroupId;
                        _baseContext.SaveChanges();

                        return RedirectToAction("Users");
                    }

                    return View(model);
                }
            }
            return View("EditStudent", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTeacherConfirmed(TeacherViewModel model)
        {
            model.DeparmentItems = PopulateDepartments();
            model.PositionItems = PopulatePositions();

            if (model.DepartmentId == 0)
            {
                ModelState.AddModelError(string.Empty, "Выбирете кафедру для преподавателя");
            }

            if (model.PositionId == 0)
            {
                ModelState.AddModelError(string.Empty, "Выбирете должность для преподавателя");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (ModelState.IsValid)
            {
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
                        Teacher teacher = _baseContext.Teacher.FirstOrDefault(i => i.UserId == user.Id);
                        teacher.DepartamentId = model.DepartmentId;
                        teacher.PositionId = model.PositionId;
                        _baseContext.SaveChanges();

                        return RedirectToAction("Users");
                    }

                    return View(model);
                }
            }
            return View("EditTeacher", model);

        }

        [HttpGet]
        public async Task<IActionResult> DeleteUser(string id)
        {
            Student student = _baseContext.Student.FirstOrDefault(i => i.UserId == id);
            Teacher teacher = _baseContext.Teacher.FirstOrDefault(i => i.UserId == id);

            if (student != null)
            {
                _baseContext.Student.Remove(student);
                _baseContext.SaveChanges();
            }
            if (teacher != null)
            {
                _baseContext.Teacher.Remove(teacher);
                _baseContext.SaveChanges();
            }

            if (id != null)
            {
                await _authentication.DeleteUserAsync(id);
            }

            return RedirectToAction("Users");
        }
        ///--------------------------------------------------------------------
    }
}