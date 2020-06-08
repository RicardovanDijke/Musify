using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Playlist_Service.Entities
{ public class UserDisplayNameUpdate
    {
        public long UserId { get; set; }
        public string DisplayName { get; set; }
    }
}
