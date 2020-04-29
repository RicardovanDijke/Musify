using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Musify_Desktop_App.Model;

namespace Musify_Desktop_App.Service
{
    class UserService
    {
        private const string UserServiceApi = "https://localhost:44321/api/";


        public User Login(string username, string password)
        {
            return LoginTask(username, password).Result;
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

            var stringTask = client.PostAsync(UserServiceApi + "authenticate", new StringContent(payload, Encoding.UTF8, "application/json"));

            try
            {
                var msg = stringTask.Result;
                var content = await msg.Content.ReadAsStringAsync();

                var user = (User)JsonSerializer.Deserialize<object>(content);
                Debug.Write(content);
                return user;
            }
            catch
            {
                //todo display LoginUnsuccesfull message
                return null;
            }

        }
    }
}
