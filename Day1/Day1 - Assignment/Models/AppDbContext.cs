namespace ITI_API_Learn.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<Employee>  Employees{ get; set; }
    }
}
