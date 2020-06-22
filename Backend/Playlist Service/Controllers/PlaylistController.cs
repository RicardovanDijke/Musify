using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            //Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var playlist = _playlistService.GetById(id);

            return new ActionResult<Playlist>(playlist);
        }

        [HttpGet]
        [Route("getFollowedPlaylistsByUserId")]
        public ActionResult<List<Playlist>> GetAllFollowedPlaylistsByUserId(int id)
        {
            //Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var playlists = _playlistService.GetFollowedPlaylistsByUserId(id);

            //Debug.WriteLine($"returning {playlists.Count} playlists");
            return new ActionResult<List<Playlist>>(playlists);
        }

        [HttpGet]
        [Route("getPublicCreatedPlaylistsByUserId")]
        public ActionResult<List<Playlist>> GetPublicCreatedPlaylistsByUserId(int id)
        {
            //Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var playlists = _playlistService.GetPublicCreatedPlaylistsByUserId(id);

            //Debug.WriteLine($"returning {playlists.Count} playlists");
            return new ActionResult<List<Playlist>>(playlists);
        }

        [HttpPost]
        [Route("addSongsToPlaylist")]
        //public ActionResult AddSongsToPlaylist(int playlistId, List<int> songIds)
        public ActionResult AddSongsToPlaylist(JArray paramList)
        {
            if (paramList.Count > 0)
            {
                var playlistId = JsonConvert.DeserializeObject<long>(paramList[0].ToString());
                var songIds = new List<long>(JsonConvert.DeserializeObject<long[]>(paramList[1].ToString()));

                _playlistService.AddSongsToPlaylist(playlistId, songIds);

                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<List<Playlist>> GetAllPlaylists()
        {
            //Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var playlists = _playlistService.GetAll().ToList();

            //Debug.WriteLine($"returning {playlists.Count} playlists");
            return new ActionResult<List<Playlist>>(playlists);
        }

        [HttpPost]
        [Route("addmany")]
        public ActionResult addMany(List<Playlist> playlists)
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");
            
            _playlistService.AddRange(playlists);

            return new OkResult();
        }
    }
}