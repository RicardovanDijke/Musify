using System.Collections.Generic;
using Auth_Service.Entities;
using Auth_Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace Auth_Service.Controllers
{
    [ApiController]
    [Route("api/userfollows")]
    public class FollowerController : ControllerBase
    {
        private readonly IUserService _userService;

        public FollowerController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("getFollowersByUserId/{userId}")]
        public List<User> GetFollowersByUser(long userId)
        {
            var followers = _userService.GetFollowersByUser(userId);

            return followers;
        }

        [HttpGet]
        [Route("getFollowingByUserId/{userId}")]
        public List<User> GetFollowingByUser(long userId)
        {
            var followers = _userService.GetFollowingByUser(userId);

            return followers;
        }
    }
}
