using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Song_Service.Entities
{

    public class Playlist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PlaylistId { get; set; }
        public string Name { get; set; }
        public bool Private { get; set; }
        public long CreatorId { get; set; }

    }
}
