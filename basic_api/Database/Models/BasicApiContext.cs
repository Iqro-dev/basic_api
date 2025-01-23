using Microsoft.EntityFrameworkCore;
using basic_api.Database.Models;

namespace basic_api.Database.Models
{
    public class BasicApiContext : DbContext
    {
        public BasicApiContext(DbContextOptions<BasicApiContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Group> Groups { get; set; } = default!;
    }
}
