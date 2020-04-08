using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Musify_Desktop_App.Model
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SongID { get; set; }

        public string Title { get; set; }

        public virtual Artist Artist { get; set; }

        public virtual Album Album { get; set; }

        public int Duration { get; set; }
        
        public DateTime? DateUploaded { get; set; }

        public string FilePath { get; }

    }
}
