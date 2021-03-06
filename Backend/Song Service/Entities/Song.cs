﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Runtime.Serialization;

namespace Song_Service.Entities
{
    public class Song
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long SongId { get; set; }

        public string Title { get; set; }

        [ForeignKey("ArtistID")]
        public virtual Artist Artist { get; set; }


        [ForeignKey("AlbumID")]
        public virtual Album Album { get; set; }

        public int Duration { get; set; }


        public DateTime? DateUploaded { get; set; }

        [IgnoreDataMember]
        public string FilePath { get; set; }


        private readonly int _trackNumber;


        public Song()
        {
            DateUploaded = DateTime.Now;
        }

        public Song(string title, Artist artist, Album album, int duration, int trackNumber)
        {
            Title = title;
            Artist = artist;
            Album = album;
            Duration = duration;
            _trackNumber = trackNumber;

            SetFilePath();
        }

        public void SetFilePath()
        {
            FilePath = Path.Combine("songstorage", Artist.Name, Album.Name, _trackNumber + " - " + Title + ".mp3");
        }

        public int GetAlbumNumber()
        {
            return Album.GetTrackNumber(this);
        }
    }
}
