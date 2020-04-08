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
        public long SongID { get; set; }

        public string Title { get; set; }

        [ForeignKey("ArtistID")]
        public virtual Artist Artist { get; set; }


        [ForeignKey("AlbumID")]

        public virtual Album Album { get; set; }

        public int Duration { get; set; }


        public DateTime? DateUploaded { get; set; }

        public string FilePath { get; }

        public Song()
        {
            DateUploaded = DateTime.Now;
        }

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
