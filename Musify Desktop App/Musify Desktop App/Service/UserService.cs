using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using Musify_Desktop_App.Model;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Musify_Desktop_App.Service
{
    internal class UserService
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