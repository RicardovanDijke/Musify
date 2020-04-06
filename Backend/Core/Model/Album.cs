using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model
{
    public class Album
    {
        public long ID { get; set; }

        public string Name { get; set; }

        public Artist artist { get; set; }

        public List<Song> Songs { get; }

        //todo constructor
        
        internal int GetTrackNumber(Song song)
        {
            if (Songs.Contains(song))
            {
                throw new Exception($"Song {song.Artist.Name} - {song.Title} is not in Album {Name}");
            }

            var trackNumber = 0;

            foreach(var songInAlbum in Songs)
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
