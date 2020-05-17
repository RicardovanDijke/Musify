using Auth_Service.Entities;
using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Auth_Service.Database
{
    /// <summary>
    ///  add-migration Auth_Service.DatabaseContext -Project "Auth Service" -Context "Auth_Service.Database.DatabaseContext"
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.UserId);
                user.HasMany(u => u.Following).WithOne().HasForeignKey(uf => uf.UserFollowId);
                user.HasMany(u => u.Followers).WithOne().HasForeignKey(uf => uf.UserFollowId);
            });

            modelBuilder.Entity<UserFollow>(userFollow =>
            {
                userFollow.HasKey(uf => uf.UserFollowId);

                userFollow.HasOne(uf => uf.Follower).WithMany(u => u.Following).HasForeignKey(uf => uf.FollowerId);
                userFollow.HasOne(uf => uf.Followee).WithMany(u => u.Followers).HasForeignKey(uf => uf.FolloweeId);
            });
        }
    }
}
