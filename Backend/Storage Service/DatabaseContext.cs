using Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Song_Service
{
    /// <summary>
    ///  add-migration Song_Service.DatabaseContext -Project "Song Service" -Context "Song_Service.DatabaseContext"
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Artist>(artist =>
            {
                artist.HasKey(a => a.ArtistID);
            });
            
            modelBuilder.Entity<Song>(song =>
            {
                song.HasKey(a => a.SongID);
                song.HasOne(a => a.Artist)
                    .WithMany(p => p.Songs); 
                song.HasOne(a => a.Album)
                    .WithMany(p => p.Songs);
            });

            modelBuilder.Entity<Album>(album =>
            {
                album.HasKey(a => a.AlbumID);
                album.HasOne(a => a.Artist)
                    .WithMany(p => p.Albums);
                album.HasMany(a => a.Songs)
                    .WithOne(p => p.Album);
            });

        }
        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

           // var artist = new Artist
           // {
           //     ArtistID = 1,
           //     Name = "Green Day"
           // };

           // var album = new Album()
           // {
           //     AlbumID = 1,
           //     ArtistID = artist.ArtistID,
           //     Name = "American Idiot"
           // };

           // modelBuilder.Entity<Artist>().HasData(artist);

           // var songs = new List<Song> {
           // new Song
           // {
           //     SongID = 1L,
           //     Title = "American Idiot",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 2L,
           //     Title = "Jesus Of Suburbia",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 3L,
           //     Title = "Holiday",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 4L,
           //     Title = "Boulevard of Broken Dreams",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 5L,
           //     Title = "Are We The Waiting",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 6L,
           //     Title = "St. Jimmy",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 7L,
           //     Title = "Give Me Novacaine",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 8L,
           //     Title = "She's A Rebel",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 9L,
           //     Title = "Extraordinary Girl",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 10L,
           //     Title = "Letterbomb",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 11L,
           //     Title = "Wake Me up When September Ends",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 12L,
           //     Title = "Homecoming",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }, new Song
           // {
           //     SongID = 13L,
           //     Title = "Whatsername",
           //     AlbumID = album.AlbumID,
           //     DateUploaded = DateTime.Now,
           //     Duration = 180,
           //     ArtistID = artist.ArtistID
           // }
           //     };
           // //modelBuilder.Entity<Album>().HasData(album);



           // modelBuilder.Entity<Song>().HasData(songs);


           //// album.Songs.AddRange(songs);



           // //modelBuilder.Entity<Album>().HasData(songs);






           // //var artist = new Artist
           // //{
           // //    ArtistID = 1,
           // //    Name = "Green Day"
           // //};

           // //modelBuilder.Entity<Artist>().HasData(artist);


           // //modelBuilder.Entity<Song>().HasData(new Song
           // //{
           // //    SongID = 1L,
           // //    Title = "American Idiot",
           // //    Album = null,
           // //    DateUploaded = DateTime.Now,
           // //    Duration = 180,
           // //    ArtistID = artist.ArtistID
           // //}
           // //);
        }


    */
    }
}
