using System;
using System.ComponentModel.DataAnnotations;

namespace EF_Task_2.Models
{
    public class Order
    {
        
        [Key]
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        // Foreign Key for Customer (One-to-Many)
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        // Many-to-Many Relationship with Product
        public List<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    }
}
