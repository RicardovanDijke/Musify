using System.Collections.Generic;

namespace Musify_Desktop_App.Model
{
    public abstract class SongList
    {
        public long SongListId { get; set; }
        public string Name { get; set; }
        public SongListType SongListType { get; protected set; }
        public bool Private { get; set; }
        public string CreatorName { get; set; }
        public List<PlaylistSong> Songs { get; set; }
    }
}
