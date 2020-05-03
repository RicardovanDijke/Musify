using System;

namespace Musify_Desktop_App.Model
{
    public class Song
    {
        public long SongID { get; set; }

        public string Title { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Album Album { get; set; }

        public int Duration { get; set; }

        public DateTime? DateUploaded { get; set; }

        public string FilePath { get; set; }

    }
}
