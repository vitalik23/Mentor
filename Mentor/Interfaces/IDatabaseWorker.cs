using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface IDatabaseWorker
    {

        IEnumerable<Group> GetAllGroups();
        IEnumerable<Department> GetAllDepartments();
        IEnumerable<Position> GetAllPositions();
        IEnumerable<User> GetUsers();
        IEnumerable<Faculty> GetFaculties();
        bool GroupExists(int groupId);
        bool DepartmentExists(int departmentId);
        bool PositionExists(int positionId);


    }
}
