using Microsoft.EntityFrameworkCore;

namespace TaxCalculator.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Threshold> Thresholds { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
        
    }
}