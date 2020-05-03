
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Musify_Desktop_App.Model;
using Newtonsoft.Json;

namespace Musify_Desktop_App.Service
{
    internal class PlaylistService
    {
        private const string PlaylistServiceApi = "https://localhost:44331/api/";


        public List<Playlist> GetFollowedPlaylistsByUserId(long userId)
        {
            return GetAllFollowedPlaylistsByUserIdTask(userId).Result;
        }


        private async Task<List<Playlist>> GetAllFollowedPlaylistsByUserIdTask(long userId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetAsync(PlaylistServiceApi + $"playlists/getFollowedPlaylistsByUserId?id={userId}");

            try
            {
                var msg = stringTask.Result;
                var content = await msg.Content.ReadAsStringAsync();

                var playlists = JsonConvert.DeserializeObject<List<Playlist>>(content);
                Debug.Write(content);
                return playlists;
            }
            catch (Exception ex)
            {
                return new List<Playlist>();
            }

        }
    }
}
