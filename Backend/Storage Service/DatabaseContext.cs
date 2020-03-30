using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Storage_Service
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasData(new User
            //{
            //    ID = 1,
            //    UserName = "Uncle",
            //    DisplayName = "Bob"

            //}, new User
            //{
            //    ID = 2,
            //    UserName = "Uncle",
            //    DisplayName = "Bob"
            //});
        }
    }
}
