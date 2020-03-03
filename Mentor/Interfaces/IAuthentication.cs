using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface IAuthentication
    {
        void ConnectTeacherToUser(string userId, int departmentId);
        void ConnectStudentToUser(string userId, int groupId);
    }
}
