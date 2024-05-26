
namespace ITI_API_Learn.Models
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee>  Employees{ get; set; }
        
    }
}
