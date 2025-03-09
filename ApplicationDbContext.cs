namespace EF_Task_1
{
    public class ApplicationDbContext : AddDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Define DbSets (tables)
    public DbSet<Product> Products { get; set; }
}

