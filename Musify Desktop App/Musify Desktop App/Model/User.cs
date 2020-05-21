using System.Collections.Generic;

namespace Musify_Desktop_App.Model
{
    public class User
    {
        public long UserId { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }


        public virtual List<User> Following { get; set; } = new List<User>();
        public virtual List<User> Followers { get; set; } = new List<User>();


        public List<Playlist> Playlists { get; set; }
        public Song CurrentlyPlayingSong { get; set; }
    }
}
