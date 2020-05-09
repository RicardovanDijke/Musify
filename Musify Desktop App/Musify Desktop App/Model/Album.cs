using System.Collections.Generic;
using Newtonsoft.Json;

namespace Musify_Desktop_App.Model
{
    public class Album : SongList
    {
        [JsonProperty("artist")]

        public new User Creator { get; set; }

        public Album()
        {
            SongListType = SongListType.Album;
        }
    }
}
