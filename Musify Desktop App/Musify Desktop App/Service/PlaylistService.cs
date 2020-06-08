
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Musify_Desktop_App.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Musify_Desktop_App.Service
{
    interface IPlaylistService
    {
        List<Playlist> GetFollowedPlaylistsByUserId(long userId);

        List<Playlist> GetPublicCreatedPlaylistsByUserId(long userId);

        void AddSongsToPlaylist(Playlist playlist, List<Song> song);

        bool CheckSongsInPlaylist(Playlist playlist, List<Song> song);
    }

    //todo add base Service class with 1 httpClient
    internal class PlaylistService : IPlaylistService
    {
        private const string GatewayApi = "https://localhost:44389/api/";

        private List<Playlist> _followedPlaylistsByUser;


        private Dictionary<long, List<Playlist>> _createdPlaylistsByUser = new Dictionary<long, List<Playlist>>();

        public List<Playlist> GetFollowedPlaylistsByUserId(long userId)
        {
            return _followedPlaylistsByUser ?? GetAllFollowedPlaylistsByUserIdTask(userId).Result;
        }

        private async Task<List<Playlist>> GetAllFollowedPlaylistsByUserIdTask(long userId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetAsync(GatewayApi + $"playlists/getFollowedPlaylistsByUserId?id={userId}");

            try
            {
                var msg = stringTask.Result;
                var content = await msg.Content.ReadAsStringAsync();

                var playlists = JsonConvert.DeserializeObject<List<Playlist>>(content);
                //Debug.Write(content);
                _followedPlaylistsByUser = playlists;
                return playlists;
            }
            catch (Exception ex)
            {
                return new List<Playlist>();
            }

        }


        public List<Playlist> GetPublicCreatedPlaylistsByUserId(long userId)
        {
            return _createdPlaylistsByUser.GetValueOrDefault(userId) ?? GetPublicCreatedPlaylistsByUserIdTask(userId).Result;
        }

        private async Task<List<Playlist>> GetPublicCreatedPlaylistsByUserIdTask(long userId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = client.GetAsync(GatewayApi + $"playlists/getPublicCreatedPlaylistsByUserId?id={userId}");

            try
            {
                var msg = stringTask.Result;
                var content = await msg.Content.ReadAsStringAsync();

                var playlists = JsonConvert.DeserializeObject<List<Playlist>>(content);
                //Debug.Write(content);
                _followedPlaylistsByUser = playlists;
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

            var paramList = new JArray
            {
                JsonConvert.SerializeObject(playlist.SongListId),
                JsonConvert.SerializeObject(songIds)
            };



            try
            {
                var httpClient = new HttpClient(new HttpClientHandler());
                HttpResponseMessage response = await httpClient.PostAsJsonAsync(GatewayApi + $"playlists/addSongsToPlaylist", paramList);
                // response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
