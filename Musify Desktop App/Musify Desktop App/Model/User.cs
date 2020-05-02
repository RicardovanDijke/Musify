using System.Collections.Generic;

namespace Musify_Desktop_App.Model
{
    public class User
    {
        public long UserID { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }


        public List<Playlist> Playlists;
        public Song CurrentlyPlayingSong { get; set; }
    }
}
