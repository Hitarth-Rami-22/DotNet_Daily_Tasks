using System.Collections.Generic;

namespace EF_Task_5.Models
{
    public class Department
    {
            public int DepartmentId { get; set; }
            public string DepartmentName { get; set; }

            // Navigation Property
            public ICollection<Employee> Employees { get; set; }
    }

}
