using Mentor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mentor.Interfaces
{
    public interface ISubjectService
    {

        bool DoesSubjectBelongsToTeacher(int subjectId, int teacherId);
        bool DoesSubjectBelongsToStudent(int subjectId, int studentId);

        System.Threading.Tasks.Task AddSubject(Subject subject, int teacherId);
        System.Threading.Tasks.Task AddSubject(string subjectName, int teacherId);
        System.Threading.Tasks.Task RemoveSubject(int subjectId);
        System.Threading.Tasks.Task AddTeacherToSubject(int subjectId, int teacherId);
        System.Threading.Tasks.Task AddStudentToSubject(int subjectId, int studentId);

        IEnumerable<Subject> GetSubjectsByTeacher(Teacher teacher);
        IEnumerable<Subject> GetSubjectsByStudent(Student student);

        Task<IEnumerable<Teacher>> GetTeachersBySubject(Subject subject);
        Task<IEnumerable<Student>> GetStudentsBySubject(Subject subject);

        Task<IEnumerable<Teacher>> GetTeachersWithoutThisSubject(Subject subject);
        Task<IEnumerable<Student>> GetStudentsWithoutThisSubject(Subject subject);

        void DeleteTeacherFromSubject(int subjectId, int teacherId);
        void DeleteStudentFromSubject(int subjectId, int studentId);

        // this list will be continued
    }
}
