using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Song_Service.Database;
using Song_Service.Service;

namespace Song_Service.Controllers
{
    [ApiController]
    [Route("api/songs/storage/")]
    public class StorageController : ControllerBase
    {
        private readonly SongService _songManager;
        private readonly IAlbumRepository _albumManager;
        private IArtistRepository _artistManager;


        public StorageController(SongService songManager, IAlbumRepository albumManager, IArtistRepository artistManager)
        {
            _songManager = songManager;
            _albumManager = albumManager;
            _artistManager = artistManager;
        }


        [HttpPost]
        [Route("uploadMany")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var success = true;
            foreach (var file in files)
            {
                if (!await _songManager.AddSong(file))
                {
                    success = false;
                }
            }

            if (success)
            {
                return new OkResult();
            }
            return StatusCode(500);

        }

        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (await _songManager.AddSong(file))
            {
                return new OkResult();
            }

            return StatusCode(500);
        }
    }
}
