using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Song_Service.Controllers
{
    [Route("api/songs")]
    [ApiController]
    public class SongController : ControllerBase
    {
        private ISongRepository songManager;

        public SongController(ISongRepository songManager)
        {
            this.songManager = songManager;
        }

        [HttpGet]
        [Route("/id")]
        public ActionResult<Song> GetOne(long id)
        {
            var song = songManager.Get(id);
            
            return new ActionResult<Song>(song);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<Song>> GetAllSongs()
        {
            //TODO create actual Songmanager that has a SongRepository, ArtistRepository, AlbumRepository? same for artist/albumManager?
            var songs = songManager.GetAll().ToList();


            return new ActionResult<List<Song>>(songs);
        }

        [HttpPost]
        public ActionResult AddDefaultData()
        {


            var artist = new Artist
            {
                ArtistID = 1,
                Name = "Green Day"
            };

            var album = new Album()
            {
                AlbumID = 1,
                Artist = artist,
                Name = "American Idiot"
            };

            //var song = new Song
            //{
            //    SongID = 13L,
            //    Title = "Whatsername",
            //    Album = null,
            //    // DateUploaded = DateTime.Now,
            //    Duration = 180,
            //    Artist = artist
            //};

            //songManager.Add(song);

            //return new OkResult();



            var songs = new List<Song> {
                new Song
             {
                 SongID = 1L,
                 Title = "American Idiot",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 2L,
                 Title = "Jesus Of Suburbia",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 3L,
                 Title = "Holiday",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 4L,
                 Title = "Boulevard of Broken Dreams",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 5L,
                 Title = "Are We The Waiting",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 6L,
                 Title = "St. Jimmy",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 7L,
                 Title = "Give Me Novacaine",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 8L,
                 Title = "She's A Rebel",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 9L,
                 Title = "Extraordinary Girl",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 10L,
                 Title = "Letterbomb",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 11L,
                 Title = "Wake Me up When September Ends",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 12L,
                 Title = "Homecoming",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }, new Song
             {
                 SongID = 13L,
                 Title = "Whatsername",
                 Album = album,
                 DateUploaded = DateTime.Now,
                 Duration = 180,
                 Artist = artist
             }
            };


            album.Songs.AddRange(songs);

            songManager.AddRange(songs);

            return new OkResult();
        }

    }
}