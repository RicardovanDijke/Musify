using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Playlist_Service.Service;

namespace Playlist_Service.Controllers
{
    [Route("api/playlists")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {

        private readonly PlaylistService _playlistService;

        public PlaylistController(PlaylistService playlistService)
        {
            this._playlistService = playlistService;
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
        [Route("all")]
        public ActionResult<List<Playlist>> GetAlPlaylists()
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
                //    Creator =
            };


            _playlistService.Add(playlist);

            return new OkResult();
        }
    }
}