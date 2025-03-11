using System.ComponentModel.DataAnnotations;

namespace EF_Task_2.Models
{
    public class Customer
    {
        [Key] // Primary Key
        public int CustomerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // One-to-Many Relationship: A Customer can have multiple Orders
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
