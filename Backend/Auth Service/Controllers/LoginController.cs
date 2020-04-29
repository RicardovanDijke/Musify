using System.Text.Json;
using System.Threading.Tasks;
using Auth_Service.Helpers;
using Auth_Service.Service;
using Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        //[HttpPost]
        //[Route("login")]
        //public ActionResult Login([FromBody]JsonElement data)
        //{
        //    var type = data.GetType();
        //    var username = data.GetProperty("username").GetString();
        //    var password = data.GetProperty("password").GetString();

        //    return Ok(userRepository.Login(username,password));

        //}

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _loginService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = _loginService.GetAll();
            return Ok(users);
        }


        [HttpGet]
        [Route("find")]
        public User findUser(int id)
        {
            //return userRepository.Get(id);
            return null;
        }

        //https://localhost:44321/api/add
        //body:
        /*
         {
        "id": 3,
        "displayName": null,
        "username": "Yordi66",
        "password": "password"
        }
        */
        [HttpPost]
        [Route("add")]
        public ActionResult addUser(User user)
        {
            // userRepository.Add(user);
            return Ok();
        }
    }
}
