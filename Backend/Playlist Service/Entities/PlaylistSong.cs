using System;

namespace Playlist_Service.Entities
{
    public class PlaylistSong
    {
        public virtual Playlist Playlist { get; set; }
        public long PlaylistId { get; set; }
        public long SongId { get; set; }
        public int Number { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
