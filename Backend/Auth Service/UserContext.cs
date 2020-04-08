using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Auth_Service
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                UserID=1,
                UserName = "Ricardo1184",
                Password = "password"

            }, new User
            {
                UserID = 2,
                UserName = "Remy561",
                Password = "password"
            });
        }
    }
}
