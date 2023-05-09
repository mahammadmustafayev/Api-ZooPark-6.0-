using Microsoft.EntityFrameworkCore;
using Zoopark.Models;

namespace Zoopark.DAL
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options) { }
        public DbSet<Animal> Animals { get; set; }
    }
}
