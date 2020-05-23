using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Musify_Desktop_App.Model;
using Newtonsoft.Json;

namespace Musify_Desktop_App.Service
{
    public interface ISongService
    {
        public SongList GetSongsInSongList(SongList songList);
        public Song GetSongById(int songId);
        public List<Song> GetSongsByIds(List<int> songIds);
        public List<Song> GetAllSongs();
        public void RequestSocket(long songId);


    }

    public class SongService : ISongService
    {
        private const string GatewayApi = "https://localhost:44389/api/";

        public SongList GetSongsInSongList(SongList songList)
        {
            var ids = songList.Songs.Select(song => song.SongId).ToList();
            var songs = GetSongsByIds(ids);

            foreach (var playlistSong in songList.Songs)
            {
                var newSong = songs.First(x => x.SongId == playlistSong.SongId);
                playlistSong.Song = newSong;
            }

            return songList;
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
            
            var httpTask = client.GetAsync(GatewayApi + $"songs/id/{songId}");

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

        public List<Song> GetSongsByIds(List<int> songIds)
        {
            return GetSongsByIdsTask(songIds).Result;
        }

        private async Task<List<Song>> GetSongsByIdsTask(List<int> songIds)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var listString = "[" + String.Join(",", songIds.Select(i => i.ToString()).ToArray()) + "]"; 


            var body = new StringContent(
                listString,
                Encoding.UTF8,
                "application/json");
            var httpTask = client.PostAsync(GatewayApi + $"songs/many", body);

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
                //todo display "song not found" message
                songs = null;

            }

            return songs;
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
