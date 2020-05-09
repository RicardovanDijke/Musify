using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Http;
using Song_Service.Database;
using TagLibFile = TagLib.File;

namespace Song_Service.Service
{
    public class AlbumService
    {
        private readonly ISongRepository _songManager;
        private readonly IAlbumRepository _albumManager;
        private readonly IArtistRepository _artistManager;


        public AlbumService(ISongRepository songManager, IAlbumRepository albumManager, IArtistRepository artistManager)
        {
            _songManager = songManager;
            _albumManager = albumManager;
            _artistManager = artistManager;
        }

        public Album GetBySong(long songId)
        {
            Debug.WriteLine($"{MethodBase.GetCurrentMethod().Name} requested with songId{songId}");

            var song = _songManager.Get(songId);

            return _albumManager.GetBySong(song);
        }
    }
}
