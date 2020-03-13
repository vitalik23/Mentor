using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface ISubjectService
    {
        System.Threading.Tasks.Task AddSubject(Subject subject, int teacherId);
        System.Threading.Tasks.Task AddSubject(string subjectName, int teacherId);
        System.Threading.Tasks.Task RemoveSubject(int subjectId);
        System.Threading.Tasks.Task AddTeacherToSubject(int subjectId, int teacherId);
        System.Threading.Tasks.Task AddStudentToSubject(int subjectId, int studentId);

        IEnumerable<Subject> GetTeachersSubjects(Teacher teacher);

        // this list will be continued
    }
}
