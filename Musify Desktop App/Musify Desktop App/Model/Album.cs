using System.Collections.Generic;

namespace Musify_Desktop_App.Model
{
    public class Album
    {
        public long AlbumId { get; set; }

        public string Name { get; set; }

        public Artist Artist { get; set; }

        public List<Song> Songs { get; } = new List<Song>();
     }
}
