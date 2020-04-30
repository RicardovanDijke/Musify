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
                user.HasKey(u => u.UserID);
                user.HasMany(u => u.Playlists);
            });


        }
    }
}
