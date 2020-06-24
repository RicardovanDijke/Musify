using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Song_Service.Database;
using Song_Service.Entities;
using Song_Service.Service;

namespace Song_Service.Controllers
{
    [ApiController]
    [Route("api/songs/storage/")]
    public class StorageController : ControllerBase
    {
        private readonly SongService _songService;

        public StorageController(SongService songService)
        {
            _songService = songService;
        }


        [HttpPost]
        [Route("uploadMany")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var success = true;
            foreach (var file in files)
            {
                if (!await _songService.AddSong(file))
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
            if (await _songService.AddSong(file))
            {
                return new OkResult();
            }

            return StatusCode(500);
        }


        [HttpPost]
        [Route("test")]
        public ActionResult<string> Test()
        {
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


            var song = new Song
            {
                SongId = 1L,
                Title = "American Idiot",
                Album = album,
                DateUploaded = DateTime.Now,
                Duration = 180,
                Artist = artist
            };

            song.SetFilePath();


            return Ok(song.FilePath);
        }
    }
}
