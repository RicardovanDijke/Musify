using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using User_Service.Entities;
using User_Service.Helpers;
using User_Service.Service;

namespace User_Service.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/user/auth")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested, userName = {model.Username}, pass = {model.Password}");

            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            var users = _userService.GetAll();
            return Ok(users);
        }


        [HttpGet("{id}")]
        public IActionResult GetUserbyId(long id)
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested");

            // only allow admins to access other user records
            var currentUserId = int.Parse(User.Identity.Name);
            if (id != currentUserId && !User.IsInRole(Role.Admin))
                return Forbid();

            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        //https://localhost:44321/api/
        //body:
        /*
         {
        "id": 1,
        "displayName": Kenai,
        "username": "Yordi66",
        "password": "password",
        "role": "Admin"
        }
        */
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            _userService.Add(user);
            return Ok();
        }

        [HttpPatch("update/{id}")]
        public ActionResult<User> Patch(long id, [FromBody]JsonPatchDocument<User> userPatch)
        {
            var user = _userService.GetById(id);
            userPatch.ApplyTo(user);
            _userService.Update(user);
            return Ok(user);
        }
    }
}
