using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using User_Service.Entities;
using User_Service.Service;

namespace User_Service.Controllers
{
    [ApiController]
    [Route("api/user/follows")]
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

        [HttpPost]
        [Route("addFollower/{followeeId}/{followerId}")]
        public ActionResult AddFollower(long followeeId, long followerId)
        {
            _userService.AddFollower(followeeId, followerId);
            return Ok();
        }

        [HttpPost]
        [Route("removeFollower/{followeeId}/{followerId}")]
        public ActionResult RemoveFollower(long followeeId, long followerId)
        {
            _userService.RemoveFollower(followeeId, followerId);
            return Ok();
        }
    }
}
