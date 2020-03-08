using Mentor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Services
{
    public class AuthenticationService : IAuthentication
    {
        public void ConnectStudentToUser(string userId, int groupId)
        {
            throw new NotImplementedException();
        }

        public void ConnectTeacherToUser(string userId, int departmentId)
        {
            throw new NotImplementedException();
        }
    }
}
