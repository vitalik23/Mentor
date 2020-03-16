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

        private DataBaseContext _dataBaseContext;
        private IAuthentication _authentication;
        private IDatabaseWorker _databaseWorker;
        private ITaskService _taskService;

        public SubjectService(DataBaseContext dataBaseContext, IAuthentication authentication, IDatabaseWorker databaseWorker, ITaskService taskService) 
        {
            _dataBaseContext = dataBaseContext;
            _authentication = authentication;
            _databaseWorker = databaseWorker;
            _taskService = taskService;
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


        public IEnumerable<Subject> GetSubjectsByTeacher(Teacher teacher)
        {

            IEnumerable<N_To_N_TeacherSubject> ns = _dataBaseContext.N_To_N_TeacherSubject.Where(c => c.TeacherId == teacher.Id).ToList();
            List<Subject> subjects = new List<Subject>();

            foreach (N_To_N_TeacherSubject n in ns) 
            {
                subjects.Add(_dataBaseContext.Subject.FirstOrDefault(c => c.Id == n.SubjectId));
            }

            return subjects;
        }

        public IEnumerable<Subject> GetSubjectsByStudent(Student student) {
            IEnumerable<N_To_N_StudentSubject> ns = _dataBaseContext.N_To_N_StudentSubject.Where(c => c.StudentId == student.Id).ToList();
            List<Subject> subjects = new List<Subject>();

            foreach (N_To_N_StudentSubject n in ns)
            {
                subjects.Add(_dataBaseContext.Subject.FirstOrDefault(c => c.Id == n.SubjectId));
            }

            return subjects;
        }


        public bool DoesSubjectBelongsToTeacher(int subjectId, int teacherId)
        {
            return _dataBaseContext.N_To_N_TeacherSubject.FirstOrDefault(x => x.SubjectId == subjectId && x.TeacherId == teacherId) != null;
        }

        public bool DoesSubjectBelongsToStudent(int subjectId, int studentId) {
            return _dataBaseContext.N_To_N_StudentSubject.FirstOrDefault(x => x.SubjectId == subjectId && x.StudentId == studentId) != null;
        }

        public async Task<IEnumerable<Teacher>> GetTeachersBySubject(Subject subject)
        {
            IEnumerable<N_To_N_TeacherSubject> list = _dataBaseContext.N_To_N_TeacherSubject.Where(x => x.SubjectId == subject.Id);

            List<N_To_N_TeacherSubject> na = new List<N_To_N_TeacherSubject>(list);
            List<Teacher> teachers = new List<Teacher>();

            foreach (N_To_N_TeacherSubject n in na)
            {
                teachers.Add(_dataBaseContext.Teacher.FirstOrDefault(h => h.Id == n.TeacherId));
            }

            foreach (Teacher teacher in teachers)
            {
                teacher.User = await _authentication.FindUserByIdAsync(teacher.UserId);
                teacher.Departament = _dataBaseContext.Departament.FirstOrDefault(k => k.Id == teacher.DepartamentId);
                teacher.Position = _dataBaseContext.Position.FirstOrDefault(m => m.Id == teacher.PositionId);
            }

            return teachers;
        }

        public async Task<IEnumerable<Student>> GetStudentsBySubject(Subject subject)
        {

            IEnumerable<N_To_N_StudentSubject> list = _dataBaseContext.N_To_N_StudentSubject.Where(x => (x.SubjectId == subject.Id));

            List<N_To_N_StudentSubject> na = new List<N_To_N_StudentSubject>(list);
            List<Student> students = new List<Student>();

            foreach (N_To_N_StudentSubject n in na)
            {
                students.Add(_dataBaseContext.Student.FirstOrDefault(h => h.Id == n.StudentId));
            }

            foreach (Student student in students)
            {
                student.User = await _authentication.FindUserByIdAsync(student.UserId);
                student.Group = _dataBaseContext.Group.FirstOrDefault(x => x.Id == student.GroupId);
            }

            return students;
        }

        public async Task<IEnumerable<Teacher>> GetTeachersWithoutThisSubject(Subject subject)
        {
            List<Teacher> allTeachers = new List<Teacher>();
            allTeachers.AddRange(_dataBaseContext.Teacher);

            List<Teacher> teachersAttachedToSubject = (List<Teacher>) await GetTeachersBySubject(subject);

            foreach (Teacher teacher in teachersAttachedToSubject)
            {
                allTeachers.Remove(teacher);
            }

            foreach (Teacher teacher in allTeachers) {
                if (teacher.User == null || teacher.Departament == null || teacher.Position == null) 
                {
                    teacher.User = await _authentication.FindUserByIdAsync(teacher.UserId);
                    teacher.Departament = _dataBaseContext.Departament.FirstOrDefault(k => k.Id == teacher.DepartamentId);
                    teacher.Position = _dataBaseContext.Position.FirstOrDefault(m => m.Id == teacher.PositionId);
                }
            }


            return allTeachers;
        }

        public async Task<IEnumerable<Student>> GetStudentsWithoutThisSubject(Subject subject)
        {

            List<Student> allStudents = new List<Student>();
            allStudents.AddRange(_dataBaseContext.Student);

            List<Student> studentsAttachedToSubject = (List<Student>) await GetStudentsBySubject(subject);

            foreach (Student student in studentsAttachedToSubject)
            {
                allStudents.Remove(student);
            }

            foreach (Student student in allStudents)
            {
                if (student.User == null || student.Group == null)
                {
                    student.User = await _authentication.FindUserByIdAsync(student.UserId);
                    student.Group = _dataBaseContext.Group.FirstOrDefault(k => k.Id == student.GroupId);
                }
            }

            return allStudents;
        }

        public void DeleteTeacherFromSubject(int subjectId, int teacherId)
        {
            N_To_N_TeacherSubject m = _dataBaseContext.N_To_N_TeacherSubject.FirstOrDefault(x => x.SubjectId == subjectId && x.TeacherId == teacherId);

            if (m != null) 
            {
                _dataBaseContext.N_To_N_TeacherSubject.Remove(m);
                _dataBaseContext.SaveChanges();
            }

        }

        public void DeleteStudentFromSubject(int subjectId, int studentId)
        {
            N_To_N_StudentSubject m = _dataBaseContext.N_To_N_StudentSubject.FirstOrDefault(x => x.SubjectId == subjectId && x.StudentId == studentId);


            if (m != null)
            {
                _dataBaseContext.N_To_N_StudentSubject.Remove(m);
                _dataBaseContext.SaveChanges();
            }
        }

        public void DeleteSubject(int subjectId)
        {
            Subject subject = _databaseWorker.GetSubjectById(subjectId);
            DeleteSubject(subject);
        }

        // should be with id (fetch from db)
        public void DeleteSubject(Subject subject)
        {
            IEnumerable<Models.Task> tasks = new List<Models.Task>(_dataBaseContext.Task.Where(x => x.SubjectId == subject.Id));
            
            foreach(Models.Task task in tasks)
            {
                _taskService.DeleteTask(task);
            }

            _dataBaseContext.Subject.Remove(subject);
            _dataBaseContext.SaveChanges();

        }
    }
}
