﻿using Microsoft.EntityFrameworkCore;
using Playlist_Service.Entities;

namespace Playlist_Service.Database
{
    /// <summary>
    ///  add-migration Playlist_Service.DatabaseContext -Project "Playlist Service" -Context "Playlist_Service.Database.DatabaseContext"
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DbSet<Playlist> Playlists { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Playlist>(playlist =>
            {
                playlist.HasKey(p => p.PlaylistId);
                playlist.HasMany(p => p.Songs);
            });

            modelBuilder.Entity<PlaylistSong>(ps =>
            {
                ps.HasKey(x => x.PlaylistSongId);
            });
        }
    }
}
