using System;

namespace Playlist_Service.Entities
{
    public class PlaylistSong
    {
       // public Playlist Playlist { get; set; }
        public long PlaylistID { get; set; }
        //public Song Song { get; set; }
        public long SongID { get; set; }
        public int Number { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
