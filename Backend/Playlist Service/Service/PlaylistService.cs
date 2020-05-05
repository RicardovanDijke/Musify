using System;
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
        Playlist GetById(long id);
        void Add(Playlist playlist);
        List<Playlist> GetFollowedPlaylistsByUserId(long id);
        void AddSongsToPlaylist(long playlistId, List<long> songIds);
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

        public Playlist GetById(long id)
        {
            return _playlistRepository.Get(id);
        }

        public void Add(Playlist user)
        {
            _playlistRepository.Add(user);
        }

        public List<Playlist> GetFollowedPlaylistsByUserId(long id)
        {
            return _playlistRepository.GetFollowedPlaylistsByUserId(id);
        }

        public void AddSongsToPlaylist(long playlistId, List<long> songIds)
        {
            var playlist = GetById(playlistId);

            var number = playlist.Songs.Select(x => x.Number).Max();

            foreach (var songId in songIds)
            {
                var playlistSong = new PlaylistSong()
                {
                    DateAdded = DateTime.Now,
                    Number = number,
                    SongId = songId,
                    Playlist = playlist,
                    PlaylistId = playlist.PlaylistId
                };
                playlist.Songs.Add(playlistSong);
                number++;
            }
            _playlistRepository.Update(playlist);
        }
    }
}
