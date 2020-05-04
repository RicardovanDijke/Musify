using System;

namespace Musify_Desktop_App.Model
{
    public class PlaylistSong
    {
        public virtual Playlist Playlist { get; set; }
        public Song Song { get; set; }
        public long SongId { get; set; }
        public int Number { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
