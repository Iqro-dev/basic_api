using Microsoft.EntityFrameworkCore;

namespace basic_api.Database.Models
{
    public class BasicApiContext : DbContext
    {
        public BasicApiContext(DbContextOptions<BasicApiContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
    }
}
