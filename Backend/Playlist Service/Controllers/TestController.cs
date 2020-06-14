using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Playlist_Service.Entities;

namespace Playlist_Service.Controllers
{
    [ApiController]
    [Route("api/playlists/test")]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        [HttpGet("test")]
        [AllowAnonymous]
        public Playlist Test()
        {
            var playlist = new Playlist(){CreatorName = "Green Day"};
            return playlist;
        }
    }
}
