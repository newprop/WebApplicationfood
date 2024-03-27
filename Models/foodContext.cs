using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class foodContext:DbContext
    {
        public DbSet<food> foods { get; set; }
        public DbSet<foodItem> foodItems { get; set; }

        public foodContext(DbContextOptions opt) : base(opt)
        {

        }
    }
}
