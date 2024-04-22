using Microsoft.EntityFrameworkCore;
using TwitterLite.Server.Models;

namespace TwitterLite.Server.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Like> Likes { get; set; }

        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Tweets)
                .WithOne(e => e.Author)
                .HasForeignKey(e => e.AuthorId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Likes)
                .WithMany(e => e.LikedBy)
                .UsingEntity<Like>();
        }
    }
}
