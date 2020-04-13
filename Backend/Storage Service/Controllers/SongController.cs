using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Song_Service.Sockets;

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
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var song = songManager.Get(id);
            
            return new ActionResult<Song>(song);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<Song>> GetAllSongs()
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            //TODO create actual Songmanager that has a SongRepository, ArtistRepository, AlbumRepository? same for artist/albumManager?
            var songs = songManager.GetAll().ToList();

            Debug.WriteLine($"returning {songs.Count} songs");
            return new ActionResult<List<Song>>(songs);
        }

        [HttpPost]
        [Route("addDefaultData")]
        public ActionResult AddDefaultData()
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");


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

            foreach (Song s in songs)
            {
                s.SetFilePath();
            }

            album.Songs.AddRange(songs);

            songManager.AddRange(songs);

            return new OkResult();
        }


        /// <summary>
        /// client sends request for websocket to be opened up to <paramref ipAdress/>
        /// </summary>
        /// <param name="ipAdress"></param>
        /// <returns> adress which client needs to connect to to receive file</returns>
        [HttpPost]
        [Route("stream")]
        public ActionResult<string> GetSongStream(string ipAdress, long songID)
        {

            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested w/ params ipAdress: {ipAdress} and id: {songID}");

            Song s = songManager.Get(songID);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                SocketManager socketManager = new SocketManager();
                socketManager.AddSocket(s, ipAdress);
            }).Start();

            
            string localIP ="127.0.0.1";
            return new ActionResult<string>(s.Title + localIP);
        }

    }
}