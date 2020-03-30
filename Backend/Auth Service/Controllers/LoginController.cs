using System.Text.Json;
using Core;
using Core.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Service.Controllers
{
    [ApiController]
    [Route("api/")]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public LoginController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody]JsonElement data)
        {
            var type = data.GetType();
            var username = data.GetProperty("username").GetString();
            var password = data.GetProperty("password").GetString();
            
            return Ok(userRepository.Login(username,password));

        }

        [HttpGet]
        [Route("find")]
        public User findUser(int id)
        {
            return userRepository.Get(id);
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
            userRepository.Add(user);
            return Ok();
        }
    }
}
