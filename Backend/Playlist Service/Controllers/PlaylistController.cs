using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Playlist_Service.Entities;
using Playlist_Service.Service;

namespace Playlist_Service.Controllers
{
    [Route("api/playlists")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        [HttpGet]
        [Route("/id")]
        public ActionResult<Playlist> GetOne(int id)
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var playlist = _playlistService.GetById(id);

            return new ActionResult<Playlist>(playlist);
        }

        [HttpGet]
        [Route("getFollowedPlaylistsByUserId")]
        public ActionResult<List<Playlist>> GetAllFollowedPlaylistsByUserId(int id)
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var playlists = _playlistService.GetFollowedPlaylistsByUserId(id);

            return new ActionResult<List<Playlist>>(playlists);
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<Playlist>> GetAllPlaylists()
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var playlists = _playlistService.GetAll().ToList();

            Debug.WriteLine($"returning {playlists.Count} playlists");
            return new ActionResult<List<Playlist>>(playlists);
        }

        [HttpPost]
        [Route("addDefaultData")]
        public ActionResult AddDefaultData()
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var playlist = new Playlist
            {
                PlaylistId = 1,
                Name = "American Idiot Reversed",
                Private = false,
                CreatorUserID = 1
            };

            var playlistSongs = new[]{
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 13,
                    Number = 0

                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 12,
                    Number = 1
                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 11,
                    Number = 2
                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 10,
                    Number = 3
                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 9,
                    Number = 4
                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 8,
                    Number = 5
                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 7,
                    Number = 6
                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 6,
                    Number = 7
                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 5,
                    Number = 8
                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 4,
                    Number = 9
                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 3,
                    Number = 10
                },
                new PlaylistSong()
                {
                    Playlist =playlist,
                    DateAdded = DateTime.Now,
                    PlaylistId = playlist.PlaylistId,
                    SongId = 2,
                    Number = 11
                }
                }.ToList();

            playlist.Songs = playlistSongs;

            _playlistService.Add(playlist);

            return new OkResult();
        }
    }
}