using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Musify_Desktop_App.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Musify_Desktop_App.Service
{
    public interface IUserService
    {
        public User Login(string username, string password);
        public List<User> GetFollowersByUser(long userId);
        public List<User> GetFollowingByUser(long userId);
        void AddFollowing(long followeeId, long followerId);
        void RemoveFollowing(long followeeId, long followerId);
        User UpdateUser(string propertyName, User user);
    }

    internal class UserService : IUserService
    {
        private const string GatewayApi = "https://localhost:44389/api/";

        public User Login(string username, string password)
        {
            var user = LoginTask(username, password).Result;
            if (user != null)
            {
                user.Followers = GetFollowersByUser(user.UserId);
                user.Following = GetFollowingByUser(user.UserId);
            }

            return user;
        }

        private async Task<User> LoginTask(string username, string password)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var payload = JsonSerializer.Serialize(new
            {
                username,
                password,
            });

            var stringTask = client.PostAsync(GatewayApi + "user/auth/login", new StringContent(payload, Encoding.UTF8, "application/json"));

            try
            {
                var msg = stringTask.Result;
                var content = await msg.Content.ReadAsStringAsync();

                var user = JsonConvert.DeserializeObject<User>(content);
                Debug.Write(content);
                return user;
            }
            catch
            {
                //todo display LoginUnsuccesfull message
                return null;
            }

        }


        public User UpdateUser(string propertyName, User user)
        {
            return UpdateUserTask(propertyName, user).Result;
        }

        private async Task<User> UpdateUserTask(string propertyName, User user)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var patch = new JsonPatchDocument();

            var propertyValue = user.GetType().GetProperty(propertyName).GetValue(user);
            patch.Add(propertyName, propertyValue);

            var payload = JsonConvert.SerializeObject(patch);

            var stringTask = client.PatchAsync(GatewayApi + $"user/auth/update/{user.UserId}", new StringContent(payload, Encoding.UTF8, "application/json"));

            try
            {
                var msg = stringTask.Result;
                var content = await msg.Content.ReadAsStringAsync();

                var updatedUser = JsonConvert.DeserializeObject<User>(content);
                Debug.Write(content);
                return updatedUser;
            }
            catch
            {
                return user;
            }
        }


        public List<User> GetFollowersByUser(long userId)
        {
            return GetFollowersByUserTask(userId).Result;
        }

        private async Task<List<User>> GetFollowersByUserTask(long userId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetAsync(GatewayApi + $"user/follows/getFollowersByUserId/{userId}");

            try
            {
                var msg = stringTask.Result;
                var content = await msg.Content.ReadAsStringAsync();

                var followers = JsonConvert.DeserializeObject<List<User>>(content);
                Debug.Write(content);
                return followers;
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }

        public List<User> GetFollowingByUser(long userId)
        {
            return GetFollowingByUserTask(userId).Result;

        }
        public void AddFollowing(long followeeId, long followerId)
        {
            AddFollowingTask(followeeId, followerId).Wait();
        }

        private async Task AddFollowingTask(long followeeId, long followerId)
        {
            try
            {
                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = httpClient.PostAsync(GatewayApi + $"user/follows/addFollower/{followeeId}/{followerId}", null).Result;
            }
            catch (Exception ex)
            {

            }
        }

        public void RemoveFollowing(long followeeId, long followerId)
        {
            RemoveFollowingTask(followeeId, followerId).Wait();
        }

        private async Task RemoveFollowingTask(long followeeId, long followerId)
        {
            try
            {
                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = httpClient.PostAsync(GatewayApi + $"user/follows/removeFollower/{followeeId}/{followerId}", null).Result;
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<List<User>> GetFollowingByUserTask(long userId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetAsync(GatewayApi + $"user/follows/getFollowingByUserId/{userId}");

            try
            {
                var msg = stringTask.Result;
                var content = await msg.Content.ReadAsStringAsync();

                var followers = JsonConvert.DeserializeObject<List<User>>(content);
                Debug.Write(content);
                return followers;
            }
            catch (Exception ex)
            {
                return new List<User>();
            }
        }
    }
}