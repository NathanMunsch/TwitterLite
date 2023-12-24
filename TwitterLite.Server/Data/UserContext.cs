using Microsoft.EntityFrameworkCore;
using TwitterLite.Server.Models;

namespace TwitterLite.Server.Data
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(user => user.Username).IsUnique();
            });
        }
    }
}
