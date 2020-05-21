using Microsoft.AspNetCore.Mvc;
using Song_Service.Entities;
using Song_Service.Service;

namespace Song_Service.Controllers
{
    [Route("api/songs/albums")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService _albumService;

        public AlbumController(AlbumService albumService)
        {
            _albumService = albumService;
        }


        [HttpGet]
        public ActionResult<Album> GetAlbumBySong(long songId)
        {
            var album = _albumService.GetBySong(songId);

            return album;
        }

    }
}