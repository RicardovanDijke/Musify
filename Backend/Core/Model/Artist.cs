﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Core.Model
{
    public class Artist
    {
        [Key]
        public long ArtistID { get; set; }

        public string Name { get; set; }

        [IgnoreDataMember]
        public virtual List<Song> Songs { get; } = new List<Song>();
        [IgnoreDataMember]
        public virtual List<Album> Albums { get; } = new List<Album>();
    }
}
