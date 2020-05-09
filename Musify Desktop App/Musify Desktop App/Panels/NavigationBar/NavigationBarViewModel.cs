using System.Collections.Generic;
using System.Windows.Navigation;
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

        public List<Model.Playlist> UserPlaylists => _playlistService.GetFollowedPlaylistsByUserId(Session.User.UserId);

        // ReSharper disable once UnusedMember.Global
        public NavigationBarViewModel() { }

        public NavigationBarViewModel(SongService songService, PlaylistService playlistService)
        {
            _songService = songService;
            _playlistService = playlistService;

            GotoHomePageCommand = new RelayCommand(OnHomePageRequested);

            RaisePropertyChanged(nameof(UserPlaylists));
        }

        private void OpenPlaylistPage()
        {
            var playlist = _songService.GetSongsInPlaylist(SelectedPlaylist);
            OnPlaylistPageRequested(playlist);
        }
    }
}
