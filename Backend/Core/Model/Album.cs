using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Core.Model
{
    public class Album
    {
        [Key]
        public long AlbumId { get; set; }

        public string Name { get; set; }


        [ForeignKey("ArtistID")]
        public virtual Artist Artist { get; set; }

        public virtual List<Song> Songs { get; } = new List<Song>();

        //todo constructor

        internal int GetTrackNumber(Song song)
        {
            if (!Songs.Contains(song))
            {
                throw new Exception($"Song {song.Artist.Name} - {song.Title} is not in Album {Name}");
            }

            var trackNumber = 0;

            foreach (var songInAlbum in Songs)
            {
                if (songInAlbum.Equals(song))
                {
                    return trackNumber;
                }

                trackNumber++;
            }

            return -1;
        }
    }
}
