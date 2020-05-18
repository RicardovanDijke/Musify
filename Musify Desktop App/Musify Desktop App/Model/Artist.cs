using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Musify_Desktop_App.Model
{
    public class Artist
    {
        public long ArtistId { get; set; }

        public string Name { get; set; }

        [IgnoreDataMember]
        public virtual List<Song> Songs { get; } = new List<Song>();
        [IgnoreDataMember]
        public virtual List<Album> Albums { get; } = new List<Album>();
    }
}
