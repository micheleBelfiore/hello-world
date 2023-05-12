using ContosoUniversity.Models;
using ContosoUniversity.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Diagnostics.Metrics;
using System.Reflection;
using System.Transactions;

namespace ContosoUniversity.Data
{
    public class MyTempTable
    {
        public int Id { get; set; }
       


    }
    public class SchoolContext : DbContext        
    {
        
        public SchoolContext(DbContextOptions<SchoolContext> options ) : base(options)
        {
            
        }

        public DbSet<Course> Courses { get; set; }
        //public DbSet<MyTempTable> MyTempTables { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }
        public DbSet<CourseAssignment> CourseAssignments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Instructor>().ToTable("Instructor");
            modelBuilder.Entity<OfficeAssignment>().ToTable("OfficeAssignment");
            modelBuilder.Entity<CourseAssignment>().ToTable("CourseAssignment");

            modelBuilder.Entity<CourseAssignment>()
                .HasKey(c => new { c.CourseID, c.InstructorID });// dichiaration key composed

            modelBuilder.Entity<MyTempTable>()
                  .HasNoKey()
                  .ToView("#MyTempTable");

        }

        // get TemptableHandler the temporany tables' handler
        public TempTableHandler GetTempTableHandler() { 
        
            return new TempTableHandler(this,Model,Database,Set<MyTempTable>);
        }
        protected virtual void Dispose(bool disposing)
        {

        }

    }

   





}

