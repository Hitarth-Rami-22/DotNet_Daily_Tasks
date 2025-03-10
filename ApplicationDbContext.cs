using Microsoft.EntityFrameworkCore;
namespace EF_Task_1
{
    public class ApplicationDbContext : AddDbContext
    {
          protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=YOUR_SERVER;Database=EFCoreDB;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        // public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

         // Define DbSets (tables)
        public DbSet<Product> Products { get; set; }
    }

}