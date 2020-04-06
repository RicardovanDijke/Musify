using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Core
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.Entity<User>().HasData(new User
        //    //{
        //    //    ID = 1,
        //    //    UserName = "Uncle",
        //    //    DisplayName = "Bob"

        //    //}, new User
        //    //{
        //    //    ID = 2,
        //    //    UserName = "Uncle",
        //    //    DisplayName = "Bob"
        //    //});

        //    var artist = new Artist
        //    {
        //        ID = 1,
        //        Name = "Green Day"
        //    };

        //    modelBuilder.Entity<Artist>().HasData(artist);


        //    modelBuilder.Entity<Song>().HasData(new Song
        //    {
        //        ID = 1L,
        //        Title = "American Idiot",
        //        Album = null,
        //        DateUploaded = DateTime.Now,
        //        Duration = 180,
        //        ArtistID = artist.ID
        //    }
        //    );
        //}

        ////private void Create
    }
}
