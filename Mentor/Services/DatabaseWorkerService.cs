using Mentor.Interfaces;
using Mentor.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Services
{
    public class DatabaseWorkerService : IDatabaseWorker
    {

        private readonly DataBaseContext _dataBaseContext;

        public DatabaseWorkerService(DataBaseContext dataBaseContext) {
            _dataBaseContext = dataBaseContext;
        }

        public bool DepartmentExists(int departmentId)
        {
            return _dataBaseContext.Departament.FirstOrDefault(p => p.Id == departmentId) != null;
        }
        public bool GroupExists(int groupId)
        {
            return _dataBaseContext.Group.FirstOrDefault(p => p.Id == groupId) != null;
        }
        public bool PositionExists(int positionId)
        {
            return _dataBaseContext.Position.FirstOrDefault(p => p.Id == positionId) != null;
        }


        public IEnumerable<Department> GetAllDepartments() => _dataBaseContext.Departament;
        public IEnumerable<Group> GetAllGroups() => new List<Group>(_dataBaseContext.Group);
        public IEnumerable<Position> GetAllPositions() => _dataBaseContext.Position;
        public IEnumerable<User> GetUsers() => _dataBaseContext.Users;
        public IEnumerable<Faculty> GetFaculties() => _dataBaseContext.Faculty;
        public IEnumerable<New> GetNews() => _dataBaseContext.New;

        public IQueryable<Student> GetAllStudents() => _dataBaseContext.Student.Include(p => p.User);
        public IQueryable<Teacher> GetAllTeachers() => _dataBaseContext.Teacher.Include(p => p.User);

        public Subject GetSubjectById(int id) => _dataBaseContext.Subject.FirstOrDefault(p => p.Id == id);
        public Models.Task GetTaskById(int id) => _dataBaseContext.Task.FirstOrDefault(p => p.Id == id);

        public Faculty GetFacultyById(int facultyId) => _dataBaseContext.Faculty.FirstOrDefault(x => x.Id == facultyId);
        public Department GetDepartmentById(int departmentId) => _dataBaseContext.Departament.FirstOrDefault(x => x.Id == departmentId);
        public Group GetGroupId(int groupId) => _dataBaseContext.Group.FirstOrDefault(x => x.Id == groupId);

        public IEnumerable<Group> GetGroupsOfDepartments(IEnumerable<Department> departments)
        {
            List<IGrouping<int, Group>> groups = new List<IGrouping<int, Group>>();

            IEnumerable<Group> allGroups = GetAllGroups();
            IEnumerable<IGrouping<int, Group>> groupsByDepartment = allGroups.GroupBy(x => x.DepartamentId);

            foreach (Department department in departments)
            {
                groups.Add(groupsByDepartment.FirstOrDefault(x => x.Key == department.Id));
            }

            List<Group> foundedGroups = new List<Group>();
            foreach (IGrouping<int, Group> g in groups) 
            {
                foreach (Group gr in g) 
                {
                    foundedGroups.Add(gr);
                }
                
            }


            return foundedGroups;
        }

        public IEnumerable<Department> GetDepartmentsByFaculty(Faculty faculty) => GetDepartmentsByFaculty(faculty.Id);
        public IEnumerable<Department> GetDepartmentsByFaculty(int facultyId) =>
            new List<Department>(_dataBaseContext.Departament.Where(x => x.FacultyId == facultyId));

        public IEnumerable<Group> GetGroupsByDepartment(Department department) => GetGroupsByDepartment(department.Id);
        public IEnumerable<Group> GetGroupsByDepartment(int departmentId) =>
            new List<Group>(_dataBaseContext.Group.Where(x => x.DepartamentId == departmentId));

        public IEnumerable<User> GetUsers(int facultyId, int departmentId, int groupId)
        {

            IQueryable<Teacher> teachers = _dataBaseContext.Teacher.Include(p => p.Departament).Include(p => p.User);
            IQueryable<Student> students = _dataBaseContext.Student.Include(p => p.Group.Departament).Include(p => p.User);

            if (facultyId != 0)
            {
                teachers = teachers.Where(p => p.Departament.FacultyId == facultyId);
                students = students.Where(p => p.Group.Departament.FacultyId == facultyId);
            }

            if (departmentId != 0)
            {
                teachers = teachers.Where(p => p.DepartamentId == departmentId);
                students = students.Where(p => p.Group.DepartamentId == departmentId);
            }

            if (groupId != 0)
            {
                students = students.Where(p => p.GroupId == groupId);
            }

            List<User> usrs = new List<User>();

            foreach (Student student in students)
            {
                usrs.Add(student.User);
            }

            foreach (Teacher teacher in teachers)
            {
                usrs.Add(teacher.User);
            }

            return usrs;
        }

        public IEnumerable<Group> GetGroupsByFaculty(int facultyId)
        {
            IEnumerable<Group> groups = _dataBaseContext.Group.Include(x => x.Departament).Where(x => x.Departament.FacultyId == facultyId).ToList();
            return groups;
        }

        public IEnumerable<Group> GetGroupsByFaculty(Faculty faculty) => GetGroupsByFaculty(faculty.Id);
    }
}
