using Mentor.Interfaces;
using Mentor.Models;
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
        public IEnumerable<Group> GetAllGroups() => _dataBaseContext.Group;
        public IEnumerable<Position> GetAllPositions() => _dataBaseContext.Position;
        public IEnumerable<User> GetUsers() => _dataBaseContext.Users;
        public IEnumerable<Faculty> GetFaculties() => _dataBaseContext.Faculty;

        public Subject GetSubjectById(int id)
        {
            return _dataBaseContext.Subject.FirstOrDefault(p => p.Id == id);
        }

        public Models.Task GetTaskById(int id)
        {
            return _dataBaseContext.Task.FirstOrDefault(p => p.Id == id);
        }
    }
}
