using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.InteropServices;

namespace Core.Model
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }

        public string Title { get; set; }

        public long ArtistID { get; set; }
        public Artist Artist { get; set; }

        public Album Album { get; set; }

        public int Duration { get; set; }

        public DateTime DateUploaded { get; set; }

        public string FilePath { get; }

        public Song(long ID, string title, Artist artist, int duration, DateTime uploaded)
        {
            this.ID = ID;
            Title = title;
            Artist = artist;
            Duration = duration;
            DateUploaded = uploaded;



            //FilePath = 
        }

        public Song() { }

        private void SetFolderPath()
        {
            var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MusifyStorage", Artist.Name, Album.Name, GetAlbumNumber() + " - " + Title);

        }

        public int GetAlbumNumber()
        {
            return Album.GetTrackNumber(this);
        }
    }
}
