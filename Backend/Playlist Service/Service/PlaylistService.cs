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
        void AddRange(List<Playlist> playlists);
        List<Playlist> GetFollowedPlaylistsByUserId(long id);
        void AddSongsToPlaylist(long playlistId, List<long> songIds);
        List<Playlist> GetPublicCreatedPlaylistsByUserId(long id);
        void UpdateCreatorName(UserDisplayNameUpdate userDisplayNameUpdate);
        void DeletePlaylistsByCreatorId(long userId);
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

        public void AddRange(List<Playlist> playlists)
        {
            _playlistRepository.AddRange(playlists);
        }

        public List<Playlist> GetFollowedPlaylistsByUserId(long id)
        {
            return _playlistRepository.GetFollowedPlaylistsByUserId(id);
        }
        public List<Playlist> GetPublicCreatedPlaylistsByUserId(long id)
        {
            return _playlistRepository.GetPublicCreatedPlaylistsByUserId(id);
        }

        public void UpdateCreatorName(UserDisplayNameUpdate userDisplayNameUpdate)
        {

            var playlists = _playlistRepository.GetAllCreatedPlaylistsByUserId(userDisplayNameUpdate.UserId);

            foreach (var playlist in playlists)
            {
                playlist.CreatorName = userDisplayNameUpdate.DisplayName;
                _playlistRepository.Update(playlist);
            }
        }

        public void DeletePlaylistsByCreatorId(long userId)
        {
            var playlists = _playlistRepository.GetAllCreatedPlaylistsByUserId(userId);

            foreach (var playlist in playlists)
            {
                _playlistRepository.Delete(playlist);
            }
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
