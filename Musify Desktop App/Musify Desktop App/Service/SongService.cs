using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Musify_Desktop_App.Model;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Musify_Desktop_App.Service
{
    public class SongService
    {
        private const string SongServiceApi = "https://localhost:44337/api/";

        public async Task<List<Song>> GetAllSongs()
        {
            //todo move to LoginService
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

           

            var httpTask = client.GetAsync(SongServiceApi + "songs/all");

            List<Song> songs;
            try
            {
                var msg =  httpTask.Result;
                var content = await msg.Content.ReadAsStringAsync();
                    Debug.Write(content);

                    songs = JsonConvert.DeserializeObject<List<Song>>(content);

            }
            catch
            {
                //todo display LoginUnsuccesfull message
                songs = null;

            }

            return songs;
        }
    }
}
