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
        public ActionResult<List<Song>> GetAllSongs()
        {
            var songs = songManager.GetAll().ToList();


            return new ActionResult<List<Song>>(songs);
        }

    }
}