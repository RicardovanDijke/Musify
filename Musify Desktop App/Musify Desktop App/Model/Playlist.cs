using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musify_Desktop_App.Model
{

    public class Playlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlaylistId { get; set; }
        public string Name { get; set; }
        public bool Private { get; set; }
        public User Creator { get; set; }
        public List<PlaylistSong> Songs { get; set; }

    }
}
