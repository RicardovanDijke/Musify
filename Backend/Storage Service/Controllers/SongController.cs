using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Song_Service.Database;
using Song_Service.Entities;
using Song_Service.Sockets;

namespace Song_Service.Controllers
{
    [Route("api/songs")]
    [ApiController]
    public class SongController : ControllerBase
    {

        //TODO create AlbumService that has a SongRepository, ArtistRepository, AlbumRepository? same for artist/albumService?
        private readonly ISongRepository _songManager;

        public SongController(ISongRepository songManager)
        {
            _songManager = songManager;
        }

        [HttpGet]
        [Route("id")]
        public ActionResult<Song> GetOne(long id)
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var song = _songManager.Get(id);

            return new ActionResult<Song>(song);
        }

        [HttpPost]
        [Route("many")]
        public ActionResult<List<Song>> GetMany([FromBody]int[] ids)
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested with ids {ids}");

            var songs = ids.Select(id =>
            {
                var song = _songManager.Get(id);
                if (song.Album != null)
                {
                    song.Album.Songs = null;
                }
                return song;
            }).ToList();

            Debug.WriteLine($"returning {songs.Count} songs");
            return new ActionResult<List<Song>>(songs);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<Song>> GetAllSongs()
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var songs = _songManager.GetAll().ToList();

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

            _songManager.AddRange(songs);

            return new OkResult();
        }

        /// <summary>
        /// client sends request for websocket to be opened up to <paramref ipAdress/>
        /// todo: move to streamservice
        /// </summary>
        /// <param name="ipAdress"></param>
        /// <returns> adress which client needs to connect to to receive file</returns>
        [HttpPost]
        [Route("stream")]
        public ActionResult<string> GetSongStream(string ipAdress, long songId)
        {

            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested w/ params ipAdress: {ipAdress} and id: {songId}");

            Song s = _songManager.Get(songId);
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                SocketManager socketManager = new SocketManager();
                socketManager.AddSocket(s, ipAdress);
            }).Start();


            string localIp = "127.0.0.1";
            return new ActionResult<string>(s.Title + localIp);
        }

    }
}