namespace EF_Task_5.Models
{
    public class EmployeeProject
    {
        // Composite Primary Key
        public int EmployeeId { get; set; }
        public int ProjectId { get; set; }

        // Additional Field
        public string Role { get; set; }

        // Navigation Properties
        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}
