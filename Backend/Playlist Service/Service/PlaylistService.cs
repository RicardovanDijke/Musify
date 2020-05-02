using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Playlist_Service.Database;
using Playlist_Service.Entities;

namespace Playlist_Service.Service
{
    public interface IPlaylistService
    {
        IEnumerable<Playlist> GetAll();
        Playlist GetById(int id);
        void Add(Playlist playlist);
        List<Playlist> GetFollowedPlaylistsByUserId(int id);
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

        public List<Playlist> GetFollowedPlaylistsByUserId(int id)
        {
            return _playlistRepository.GetFollowedPlaylistsByUserId(id);
        }
    }
}
