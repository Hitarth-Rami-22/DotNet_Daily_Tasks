using EF_Task_5.Models;
using Microsoft.EntityFrameworkCore;



namespace EF_Task_5.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Employee-Project (Many-to-Many)
            modelBuilder.Entity<EmployeeProject>()
                .HasKey(ep => new { ep.EmployeeId, ep.ProjectId }); // Composite Primary Key

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId);

            modelBuilder.Entity<EmployeeProject>()
                .HasOne(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId);

            // Department-Employee (One-to-Many)
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employees)
                .HasForeignKey(e => e.DepartmentId);

            // **Data Seeding**
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "IT" },
                new Department { DepartmentId = 2, DepartmentName = "HR" }
            );

            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, Name = "John Doe", Email = "john@example.com", DepartmentId = 1 },
                new Employee { EmployeeId = 2, Name = "Jane Smith", Email = "jane@example.com", DepartmentId = 2 }
            );

            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, ProjectName = "Website Development", StartDate = new DateTime(2024, 1, 1) },
                new Project { ProjectId = 2, ProjectName = "Mobile App", StartDate = new DateTime(2024, 2, 1) }
            );

            modelBuilder.Entity<EmployeeProject>().HasData(
                new EmployeeProject { EmployeeId = 1, ProjectId = 1, Role = "Developer" },
                new EmployeeProject { EmployeeId = 2, ProjectId = 2, Role = "Manager" }
            );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(); // Enable Lazy Loading
        }

    }

}
