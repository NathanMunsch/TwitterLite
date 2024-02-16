using Microsoft.EntityFrameworkCore;
using TwitterLite.Server.Models;

namespace TwitterLite.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Tweet> Tweets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tweet>()
                .HasOne(t => t.Author)
                .WithMany(u => u.Tweets)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Likes)
                .WithMany(t => t.LikedBy)
                .UsingEntity(j => j.ToTable("UserLikedTweets"));
        }
    }
}
