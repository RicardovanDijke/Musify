using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Song_Service.Database;
using Song_Service.Service;

namespace Song_Service.Controllers
{
    [ApiController]
    [Route("api/storage/")]
    public class StorageController : ControllerBase
    {
        private SongService songManager;
        private IAlbumRepository albumManager;
        private IArtistRepository artistManager;


        public StorageController(SongService songManager, IAlbumRepository albumManager, IArtistRepository artistManager)
        {
            this.songManager = songManager;
            this.albumManager = albumManager;
            this.artistManager = artistManager;
        }

        // GET: api/Storage
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        // GET: api/Storage/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Storage
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Storage/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        //todo add endpoint /uploadMany

        [HttpPost]
        [Route("uploadMany")]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var success = true;
            foreach (var file in files)
            {
                if (!await songManager.AddSong(file))
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
            if (await songManager.AddSong(file))
            {
                return new OkResult();
            }

            return StatusCode(500);
        }
    }
}
