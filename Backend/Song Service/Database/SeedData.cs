using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Song_Service.Entities;

namespace Song_Service.Database
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new DatabaseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DatabaseContext>>());
            // Look for any movies.
            if (context.Songs.Any())
            {
                return; // DB has been seeded
            }

            var artist = new Artist
            {
                ArtistId = 1,
                Name = "Green Day"
            };

            var album = new Album()
            {
                AlbumId = 1,
                Artist = artist,
                Name = "American Idiot"
            };

            var songs = new List<Song> {
                new Song
             {
                 SongId = 1L,
                 Title = "American Idiot",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 2L,
                 Title = "Jesus Of Suburbia",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 3L,
                 Title = "Holiday",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 4L,
                 Title = "Boulevard of Broken Dreams",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 5L,
                 Title = "Are We The Waiting",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 6L,
                 Title = "St. Jimmy",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 7L,
                 Title = "Give Me Novacaine",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 8L,
                 Title = "She's A Rebel",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 9L,
                 Title = "Extraordinary Girl",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 10L,
                 Title = "Letterbomb",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 11L,
                 Title = "Wake Me up When September Ends",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 12L,
                 Title = "Homecoming",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongId = 13L,
                 Title = "Whatsername",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }
            };

            foreach (Song s in songs)
            {
                s.SetFilePath();
            }

            album.Songs.AddRange(songs);

            context.Songs.AddRange(songs);
            context.SaveChanges();
        }
    }
}
