using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
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
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");



            var httpTask = client.GetAsync(SongServiceApi + "songs/all");

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
                //todo display LoginUnsuccesfull message
                songs = null;

            }

            return songs;
        }

        public void RequestSocket(long songID)
        {
            WebClient webClient = new WebClient();

           // var reqparm = new System.Collections.Specialized.NameValueCollection();
            //reqparm.Add("ipAdress", "127.0.0.1");
           // reqparm.Add("songID", songID.ToString()); 
            webClient.QueryString.Add("ipAdress", "127.0.0.1");
            webClient.QueryString.Add("songID", songID.ToString());
         //   string result = webClient.UploadValues(SongServiceApi + "songs/stream");
            webClient.UploadValues(SongServiceApi + "songs/stream", webClient.QueryString);

        }

    }
}
