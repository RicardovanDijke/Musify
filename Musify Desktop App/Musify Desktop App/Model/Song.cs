using System;

namespace Musify_Desktop_App.Model
{
    public class Song
    {
        //[Key]
        //[JsonProperty(PropertyName = "songID")]

        public long SongID { get; set; }


        //[JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Album Album { get; set; }

        public int Duration { get; set; }

        public DateTime? DateUploaded { get; set; }

        public string FilePath { get; set; }

    }
}
