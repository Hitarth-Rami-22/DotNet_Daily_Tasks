namespace EF_Task_1
{
    public class ApplicationDbContext : DbContext
    {
          public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Add your DbSet properties here (Example)
        public DbSet<Student> Students { get; set; }
    }
}
