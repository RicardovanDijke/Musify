using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Musify_Desktop_App.Model;
using Newtonsoft.Json;

namespace Musify_Desktop_App.Service
{
    public class SongService
    {
        private const string GatewayApi = "https://localhost:44389/api/";

        public Playlist GetSongsInPlaylist(Playlist playlist)
        {
            foreach (var playlistSong in playlist.Songs)
            {
                playlistSong.Song = GetSongById(playlistSong.SongId);
            }

            return playlist;
        }

        public Song GetSongById(int songId)
        {
            return GetSongByIdTask(songId).Result;
        }

        private async Task<Song> GetSongByIdTask(int songId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");



            var httpTask = client.GetAsync(GatewayApi + $"songs/id?id={songId}");

            Song song;
            try
            {
                var msg = httpTask.Result;
                var content = await msg.Content.ReadAsStringAsync();
                Debug.Write(content);

                song = JsonConvert.DeserializeObject<Song>(content);
            }
            catch
            {
                //todo display "song not found" message
                song = null;

            }

            return song;
        }

        public List<Song> GetAllSongs()
        {
            return GetAllSongsTask().Result;
        }
        private async Task<List<Song>> GetAllSongsTask()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");



            var httpTask = client.GetAsync(GatewayApi + "songs/all");

            List<Song> songs;
            try
            {
                var msg = httpTask.Result;
                var content = await msg.Content.ReadAsStringAsync();
                Debug.Write(content);

                songs = JsonConvert.DeserializeObject<List<Song>>(content);

            }
            catch
            {
                //todo display "songs not found" message
                songs = null;

            }

            return songs;
        }


        public Album GetSongsInAlbum(Album album)
        {
            foreach (var playlistSong in album.Songs)
            {
                playlistSong.Song = GetSongById(playlistSong.SongId);
            }

            return album;
        }

        public Album GetAlbumBySong(Song selectedSong)
        {
            return GetAlbumBySongTask(selectedSong.SongId).Result;
        }

        private async Task<Album> GetAlbumBySongTask(int songId)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");



            var httpTask = client.GetAsync(GatewayApi + "albums?songId=" + songId);

            Album album;
            try
            {
                var msg = httpTask.Result;
                var content = await msg.Content.ReadAsStringAsync();
                Debug.Write(content);

                album = JsonConvert.DeserializeObject<Album>(content);

            }
            catch
            {
                //todo display "songs not found" message
                album = null;
            }

            return album;
        }


        public void RequestSocket(long songId)
        {
            WebClient webClient = new WebClient();

            webClient.QueryString.Add("ipAdress", "127.0.0.1");
            webClient.QueryString.Add("songID", songId.ToString());

            webClient.UploadValues(GatewayApi + "songs/stream", webClient.QueryString);
        }
    }
}
