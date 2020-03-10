using Mentor.Interfaces;
using Mentor.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Services
{
    public class AuthenticationService : IAuthentication
    {

        private readonly DataBaseContext _dataBaseContext;

        public AuthenticationService(DataBaseContext dataBaseContext) {
            _dataBaseContext = dataBaseContext;
        }

        public bool CreateStudentUser(string userId, int groupId)
        {

            Group group = _dataBaseContext.Group.FirstOrDefault(p => p.Id == groupId);

            if (group == null)
            {
                // Console.WriteLine(" Group is null");
                return false;
            }
            else 
            {
                Student student = new Student { UserId = userId, GroupId = groupId };
                _dataBaseContext.Student.Add(student);
                _dataBaseContext.SaveChanges();

                return true;

              //  Console.WriteLine(" Group is not null");
            }

    
        }

        public bool CreateTeacherUser(string userId, int departmentId, int positionId)
        {

            Department department = _dataBaseContext.Departament.FirstOrDefault(p => p.Id == departmentId);
            Position position = _dataBaseContext.Position.FirstOrDefault(p => p.Id == positionId);

            if (department == null || position == null)
            {
                return false;
            }
            else 
            {
                Teacher teacher = new Teacher { UserId = userId, DepartamentId = departmentId, PositionId = positionId };

                _dataBaseContext.Teacher.Add(teacher);
                _dataBaseContext.SaveChanges();

                return true;
            }

        }
    }
}
