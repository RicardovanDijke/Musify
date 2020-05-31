using Microsoft.EntityFrameworkCore;
using Song_Service.Entities;

namespace Song_Service.Database
{
    /// <summary>
    ///  add-migration Song_Service.Database.DatabaseContext -Project "Song Service" -Context "Song_Service.Database.DatabaseContext"
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
                artist.HasKey(a => a.ArtistId);
            });
            
            modelBuilder.Entity<Song>(song =>
            {
                song.HasKey(a => a.SongId);
                song.HasOne(a => a.Artist)
                    .WithMany(p => p.Songs); 
                song.HasOne(a => a.Album)
                    .WithMany(p => p.Songs);
            });

            modelBuilder.Entity<Album>(album =>
            {
                album.HasKey(a => a.AlbumId);
                album.HasOne(a => a.Artist)
                    .WithMany(p => p.Albums);
                album.HasMany(a => a.Songs)
                    .WithOne(p => p.Album);
            });

        }
    }
}
