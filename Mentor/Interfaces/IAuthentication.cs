using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface IAuthentication
    {
        bool CreateTeacherUser(string userId, int departmentId, int positionId);
        bool CreateStudentUser(string userId, int groupId);
    }
}
