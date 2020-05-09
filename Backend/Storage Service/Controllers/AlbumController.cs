using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Song_Service.Database;
using Song_Service.Service;
using Song_Service.Sockets;

namespace Song_Service.Controllers
{
    [Route("api/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService _albumService;

        public AlbumController(AlbumService albumService)
        {
            this._albumService = albumService;
        }


        [HttpGet]
        public ActionResult<Album> GetAlbumBySong(long songId)
        {
            var album = _albumService.GetBySong(songId);

            return album;
        }

    }
}