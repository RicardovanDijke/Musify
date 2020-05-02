using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Playlist_Service.Database;

namespace Playlist_Service.Service
{
    public interface IPlaylistService
    {
        IEnumerable<Playlist> GetAll();
        Playlist GetById(int id);
        void Add(Playlist playlist);
    }

    public class PlaylistService : IPlaylistService
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistService(IPlaylistRepository userRepository)
        {
            _playlistRepository = userRepository;

        }


        public IEnumerable<Playlist> GetAll()
        {
            return _playlistRepository.GetAll().ToList();
        }

        public Playlist GetById(int id)
        {
            return _playlistRepository.Get(id);
        }

        public void Add(Playlist user)
        {
            _playlistRepository.Add(user);
        }
    }
}
