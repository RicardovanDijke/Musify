using Microsoft.EntityFrameworkCore;
using System;
using User_Service.Entities;

namespace User_Service.Database
{
    /// <summary>
    ///  add-migration User_Service.DatabaseContext -Project "User Service" -Context "User_Service.Database.DatabaseContext"
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }

        public DatabaseContext() { }
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
