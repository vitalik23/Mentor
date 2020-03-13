using Mentor.Interfaces;
using Mentor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Services
{
    public class SubjectService : ISubjectService
    {

        DataBaseContext _dataBaseContext;

        public SubjectService(DataBaseContext dataBaseContext) {
            _dataBaseContext = dataBaseContext;
        }

        public async System.Threading.Tasks.Task AddSubject(string subjectName, int teacherId)
        {
            Subject subject = new Subject { Name = subjectName };

            await AddSubject(subject, teacherId);
        }

        public async System.Threading.Tasks.Task AddSubject(Subject subject, int teacherId)
        {
            await _dataBaseContext.Subject.AddAsync(subject);
            await _dataBaseContext.SaveChangesAsync();

            await AddTeacherToSubject(subject.Id, teacherId);
        }

        public async System.Threading.Tasks.Task AddStudentToSubject(int subjectId, int studentId)
        {
            N_To_N_StudentSubject record = new N_To_N_StudentSubject { StudentId = studentId, SubjectId = subjectId};

            await _dataBaseContext.N_To_N_StudentSubject.AddAsync(record);
            await _dataBaseContext.SaveChangesAsync();

        }

        public async System.Threading.Tasks.Task AddTeacherToSubject(int subjectId, int teacherId)
        {
            N_To_N_TeacherSubject record = new N_To_N_TeacherSubject { TeacherId = teacherId, SubjectId = subjectId };

            await _dataBaseContext.N_To_N_TeacherSubject.AddAsync(record);
            await _dataBaseContext.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task RemoveSubject(int subjectId)
        {
            Subject subject = _dataBaseContext.Subject.FirstOrDefault(p => p.Id == subjectId);

            _dataBaseContext.Subject.Remove(subject);
            await _dataBaseContext.SaveChangesAsync();

        }

        public IEnumerable<Subject> GetTeachersSubjects(Teacher teacher)
        {

            IEnumerable<N_To_N_TeacherSubject> ns = _dataBaseContext.N_To_N_TeacherSubject.Where(c => c.TeacherId == teacher.Id).ToList();
            List<Subject> subjects = new List<Subject>();

            foreach (N_To_N_TeacherSubject n in ns) 
            {
                subjects.Add(_dataBaseContext.Subject.FirstOrDefault(c => c.Id == n.SubjectId));
            }

            return subjects;
        }
    }
}
