using Microsoft.EntityFrameworkCore;


namespace EF_Task_1.Data
{
    public class ApplicationDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

      
        public DbSet<Student> Students { get; set; } // Changed from Products to Students
    }

    // Rename the class from Students to Student
    public class Student
    {
        public int Id { get; set; } // Primary Key

        public string FirstName { get; set; } // First Name of the Student

        public string Email { get; set; } // Email Address of the Student
    }
}

