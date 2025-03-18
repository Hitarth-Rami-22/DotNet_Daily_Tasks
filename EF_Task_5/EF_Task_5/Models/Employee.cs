using System.Collections.Generic;


namespace EF_Task_5.Models
{
    public class Employee
    { public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Foreign Key
        public int DepartmentId { get; set; }

        // Navigation Properties
        public Department Department { get; set; }
        public virtual ICollection<EmployeeProject> EmployeeProjects { get; set; } = new List<EmployeeProject>();
    }
}
