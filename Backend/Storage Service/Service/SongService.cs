﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Http;
using Song_Service.Database;

using TagLibFile = TagLib.File;

namespace Song_Service.Service
{
    public class SongService
    {

        private ISongRepository songManager;
        private IAlbumRepository albumManager;
        private IArtistRepository artistManager;


        public SongService(ISongRepository songManager, IAlbumRepository albumManager, IArtistRepository artistManager)
        {
            this.songManager = songManager;
            this.albumManager = albumManager;
            this.artistManager = artistManager;
        }


        public async Task<bool> AddSong(IFormFile file)
        {
            await using (Stream stream = file.OpenReadStream())
            {
                using (var binaryReader = new BinaryReader(stream))
                {
                    var fileContent = binaryReader.ReadBytes((int)file.Length);
                    var str = Encoding.Default.GetString(fileContent);
                    //await this.UploadFile(file.ContentDisposition);
                    var filePath = "C:\\users\\ricar\\desktop\\testmp3.mp3";

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    TagLibFile.IFileAbstraction iFile = new TagLibFile.LocalFileAbstraction(filePath);

                    var mp3 = TagLibFile.Create(iFile);

                    var artistName = mp3.Tag.Performers[0];
                    var albumName = mp3.Tag.Album;
                    var albumNumber = mp3.Tag.Track;

                    //todo add album art

                    //create new artist / album if it doesnt exist yet
                    var artist = artistManager.FindByName(artistName) ?? new Artist()
                    {
                        Name = artistName
                    };

                    var album = albumManager.FindByName(albumName) ?? new Album()
                    {
                        Artist = artist,
                        Name = albumName
                    };


                    //TODO fix duration
                    var song = new Song(mp3.Tag.Title, artist, album, mp3.Properties.Duration.Seconds, (int)albumNumber);

                    songManager.Add(song);

                    // var extension = Path.GetExtension(file.FileName);
                    //   var folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MusifyStorage", artistName, albumName);
                    //  var fileName = albumNumber + " - " + mp3.Tag.Title;
                    // var fullPath = Path.Combine(folderPath, fileName + extension);

                    var fileInfo = new FileInfo(song.FilePath);
                    if (!Directory.Exists(fileInfo.DirectoryName))
                    {
                        Directory.CreateDirectory(fileInfo.DirectoryName);
                    }

                    using (var fileStream = new FileStream(fileInfo.FullName, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            return true;
        }
    }
}