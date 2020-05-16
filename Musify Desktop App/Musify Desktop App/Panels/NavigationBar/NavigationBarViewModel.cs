using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.NavigationBar
{
    internal class NavigationBarViewModel : BasePanelNavigation
    {
        private SongService _songService;
        private PlaylistService _playlistService;

        private Model.Playlist _selectedPlaylist;
        private ObservableCollection<Model.Playlist> _playlists;
        public RelayCommand GotoHomePageCommand { get; set; }

        public Model.Playlist SelectedPlaylist
        {
            get { return _selectedPlaylist; }

            set
            {
                _selectedPlaylist = value;
                OpenPlaylistPage();
            }
        }


        public ObservableCollection<Model.Playlist> UserPlaylists
        {
            get => _playlists;
            set
            {
                _playlists = value;
                RaisePropertyChanged(nameof(UserPlaylists));
            }
        }


        // ReSharper disable once UnusedMember.Global
        public NavigationBarViewModel() { }

        public NavigationBarViewModel(SongService songService, PlaylistService playlistService)
        {
            _songService = songService;
            _playlistService = playlistService;

            GotoHomePageCommand = new RelayCommand(OnHomePageRequested);

            UserPlaylists = new ObservableCollection<Model.Playlist>(_playlistService.GetFollowedPlaylistsByUserId(Session.User.UserId));
        }

        private void OpenPlaylistPage()
        {
            var playlist = _songService.GetSongsInSongList(SelectedPlaylist);
            OnPlaylistPageRequested(playlist);
        }
    }
}
