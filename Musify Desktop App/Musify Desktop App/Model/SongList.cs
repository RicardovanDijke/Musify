using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Musify_Desktop_App.Model
{
    public abstract class SongList
    {
        public long SongListId { get; set; }
        public string Name { get; set; }
        public SongListType SongListType { get; protected set; }
        public bool Private { get; set; }
        public User Creator { get; set; }
        public List<PlaylistSong> Songs { get; set; }
    }
}
