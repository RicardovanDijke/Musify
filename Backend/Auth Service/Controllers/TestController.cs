using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User_Service.Entities;

namespace User_Service.Controllers
{
    [ApiController]
    [Route("api/user/test")]
    [AllowAnonymous]
    public class TestController : ControllerBase
    {
        [HttpGet("test")]
        [AllowAnonymous]
        public User Test()
        {
            var user = new User {DisplayName = "Musify"};
            return user;
        }
    }
}
