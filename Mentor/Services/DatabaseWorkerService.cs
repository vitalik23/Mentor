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

        public IEnumerable<Department> GetAllDepartments() => _dataBaseContext.Departament;
        public IEnumerable<Group> GetAllGroups() => _dataBaseContext.Group;
        public IEnumerable<Position> GetAllPositions() => _dataBaseContext.Position;
        public IEnumerable<User> GetUsers() => _dataBaseContext.Users;
    }
}
