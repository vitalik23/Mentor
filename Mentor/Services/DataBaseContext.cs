using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentor.Models
{
    public class DataBaseContext : IdentityDbContext<User>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DataBaseContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Department> Departament { get; set; }
        public DbSet<Faculty> Faculty { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<N_To_N_StudentSubject> N_To_N_StudentSubject { get; set; }
        public DbSet<N_To_N_TaskStudent> N_To_N_TaskStudent { get; set; }
        public DbSet<N_To_N_TeacherSubject> N_To_N_TeacherSubject { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Task> Task { get; set; }
        public DbSet<Teacher> Teacher { get; set; }
        public DbSet<Position> Position { get; set; }


    }
}
