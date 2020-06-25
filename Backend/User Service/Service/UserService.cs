using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User_Service.Database;
using User_Service.Entities;
using User_Service.Helpers;
using User_Service.Message;

namespace User_Service.Service
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
        User GetById(long id);
        User Add(User user);
        List<User> GetFollowersByUser(long userId);
        List<User> GetFollowingByUser(long userId);
        void AddFollower(long followeeId, long followerId);
        void RemoveFollower(long followeeId, long followerId);
        void Update(User user);
        void Delete(User user);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserUpdateSender _userUpdateSender;
        private readonly AppSettings _appSettings;

        public UserService(IUserRepository userRepository, IUserUpdateSender userUpdateSender)
        {
            _userRepository = userRepository;
            _userUpdateSender = userUpdateSender;
        }
        public UserService(IUserRepository userRepository, IUserUpdateSender userUpdateSender, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _userUpdateSender = userUpdateSender;
            _appSettings = appSettings.Value;

        }
        public User Authenticate(string username, string password)
        {
            username = username.ToLower();
            password = password.ToLower();

            var user = _userRepository.GetAll().SingleOrDefault(x => x.UserName == username && x.Password == password);

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll().WithoutPasswords();
        }

        public User GetById(long id)
        {
            return _userRepository.Get(id);
        }

        public User Add(User user)
        {
            return _userRepository.Add(user);
        }

        public List<User> GetFollowersByUser(long userId)
        {
            return _userRepository.GetFollowersByUser(userId);
        }

        public List<User> GetFollowingByUser(long userId)
        {
            return _userRepository.GetFollowingByUser(userId);
        }

        public void AddFollower(long followeeId, long followerId)
        {
            var followee = GetById(followeeId);
            var follower = GetById(followerId);

            followee.AddFollower(follower);
            _userRepository.Update(followee);
        }

        public void RemoveFollower(long followeeId, long followerId)
        {
            var followee = GetById(followeeId);

            followee.Followers.RemoveAll(x => x.FollowerId == followerId);
            _userRepository.Update(followee);
        }

        public void Update(User user)
        {
            _userRepository.Update(user);
            _userUpdateSender.SendUpdate("User.DisplayName", new User { UserId = user.UserId, DisplayName = user.DisplayName });
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
            _userUpdateSender.SendUpdate("User.Deleted", new User { UserId = user.UserId });
        }
    }
}
