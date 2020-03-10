using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface IDatabaseWorker
    {

        public IEnumerable<Group> GetAllGroups();
        public IEnumerable<Department> GetAllDepartments();
        public IEnumerable<Position> GetAllPositions();

    }
}
