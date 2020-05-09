using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musify_Desktop_App.Model
{

    public class Playlist : SongList
    {

        public Playlist()
        {
            SongListType = SongListType.Playlist;
        }
    }
}
