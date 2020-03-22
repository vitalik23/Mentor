using Mentor.Interfaces;
using Mentor.Models;
using Mentor.ViewModels.PickupUserRelated;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Controllers
{

    public class PickupUserController : Controller
    {

        private IDatabaseWorker _databaseWorker;

        public PickupUserController(IDatabaseWorker databaseWorker) 
        {
            _databaseWorker = databaseWorker;
        }


       
      
        [HttpGet]
        public IActionResult Index(string name, string surname, int facultyId, int departmentId, int groupId, int selector) {

            int departmentSelected = departmentId;
            int groupSelected = groupId;

            switch (selector)
            {
                case 1:
                    departmentSelected = 0;
                    groupSelected = 0;

                    departmentId = 0;
                    groupId = 0;
                    break;
                case 2: 
                    groupSelected = 0;

                    groupId = 0;
                    break;

            }

            List<Faculty> faculties = _databaseWorker.GetFaculties().ToList();
            
            List<Department> departments = null;
            if (facultyId == 0)
            {
                departments = _databaseWorker.GetAllDepartments().ToList();
            } 
            else
            {
                departments = (List<Department>)_databaseWorker.GetDepartmentsByFaculty(facultyId);
            }
            

            List<Group> groups = null;
            
            if (facultyId == 0 && departmentId == 0)
            {
                groups = (List<Group>)_databaseWorker.GetAllGroups();
            }
            else
            {
                if (departmentId == 0)
                {
                    groups = (List<Group>)_databaseWorker.GetGroupsByFaculty(facultyId);
                }
                else
                {
                    groups = (List<Group>)_databaseWorker.GetGroupsByDepartment(departmentId);
                }
                
            }

            
            faculties.Insert(0, new Faculty { Name = "Все", Id = 0 });
            SelectList facultiesS = new SelectList(faculties, "Id", "Name");
            foreach (SelectListItem item in facultiesS)
            {
                if (item.Value == facultyId.ToString())
                {
                    item.Selected = true;
                }
            }

            groups.Insert(0, new Group { Name = "Все", Id = 0 });
            SelectList groupsS = new SelectList(groups, "Id", "Name");

            departments.Insert(0, new Department { Name = "Все", Id = 0 });
            SelectList departmentsS = new SelectList(departments, "Id", "Name");  

            foreach (SelectListItem item in departmentsS)
            {
                if (item.Value == departmentSelected.ToString())
                {
                    item.Selected = true;
                }
            }

            foreach (SelectListItem item in groupsS)
            {
                if (item.Value == groupSelected.ToString())
                {
                    item.Selected = true;
                }
            }

            IEnumerable<User> users = _databaseWorker.GetUsers(facultyId, departmentId, groupId);

            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }

            if (!String.IsNullOrEmpty(surname))
            {
                users = users.Where(p => p.Surname.Contains(surname));
            }

        //    users = users.Where(p => p.IsAccepted == true);

            PickupUserViewModel viewModel = new PickupUserViewModel
            {
                Users = users.ToList(),
                Faculties = facultiesS,
                Departments = departmentsS,
                Groups = groupsS,

                User = new User
                {
                    Name = name,
                    Surname = surname
                }
            };

            return View(viewModel);
        }



    }
}
