
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Musify_Desktop_App.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Musify_Desktop_App.Service
{
    interface IPlaylistService
    {
        List<Playlist> GetFollowedPlaylistsByUserId(long userId);

        void AddSongsToPlaylist(Playlist playlist, List<Song> song);

        bool CheckSongsInPlaylist(Playlist playlist, List<Song> song);
    }

    //todo add base Service class with 1 httpClient
    internal class PlaylistService : IPlaylistService
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


        public void AddSongsToPlaylist(Playlist playlist, List<Song> songs)
        {
            _ = AddSongsToPlaylistTask(playlist, songs);
        }

        public bool CheckSongsInPlaylist(Playlist playlist, List<Song> songs)
        {
            var playlistSongIds = playlist.Songs.Select(x => (int)x.SongId).ToList();

            return songs.Any(song => playlistSongIds.Contains(song.SongId));
        }

        private async Task AddSongsToPlaylistTask(Playlist playlist, List<Song> songs)
        {
            var songIds = songs.Select(x => (long)x.SongId).ToArray();

            JArray paramList = new JArray();

            paramList.Add(JsonConvert.SerializeObject(playlist.PlaylistId));
            paramList.Add(JsonConvert.SerializeObject(songIds));


            try
            {
                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(PlaylistServiceApi + $"playlists/addSongsToPlaylist", paramList);
                // response.EnsureSuccessStatusCode();

                string data = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }
        }

        //private async Task AddSongsToPlaylistTask(Playlist playlist, List<Song> songs)
        //{
        //    var client = new HttpClient();
        //    client.DefaultRequestHeaders.Accept.Clear();
        //    client.DefaultRequestHeaders.Accept.Add(
        //        new MediaTypeWithQualityHeaderValue("application/json"));
        //    client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

        //    var songIds = songs.Select(x => (int)x.SongId).ToArray();
        //    var a = songIds.ToString();
        //    var parameters = new Dictionary<string, string> { { "playlistId", playlist.PlaylistId.ToString() }, { "songIds", songIds.ToString() } };


        //    var stringTask = client.PostAsync(
        //        "",
        //        new StringContent(
        //            songIds.ToString(),
        //            Encoding.UTF8,
        //            "application/json"));

        //    try
        //    {
        //        var msg = stringTask.Result;
        //        var content = await msg.Content.ReadAsStringAsync();

        //        Debug.Write(content);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    }
}
