using System;

namespace Core.Model
{
    public class PlaylistSong
    {
        public Playlist Playlist { get; set; }
        public long PlaylistId { get; set; }
        public Song Song { get; set; }
        public long SongId { get; set; }
        public int Number { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
