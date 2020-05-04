﻿using System.Linq;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.Playlist
{
    internal class PlaylistPageViewModel : BasePanelNavigation
    {

        public SongListViewModel SongListViewModel { get; set; }

        public PlaylistPageViewModel() { }
        public PlaylistPageViewModel(SongService songService, PlaylistService playlistService, Model.Playlist playlist)
        {
            var songsInPlaylist = playlist.Songs.OrderBy(x => x.Number).Select(playlistItem => playlistItem.Song).ToList();


            SongListViewModel = new SongListViewModel(songService, playlistService, songsInPlaylist, playlist.Name);
            RaisePropertyChanged(nameof(SongListViewModel));
        }
    }
}
