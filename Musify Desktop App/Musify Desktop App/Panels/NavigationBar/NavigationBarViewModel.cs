using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.NavigationBar
{
    internal class NavigationBarViewModel : BasePanelNavigation
    {
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

        public List<Model.Playlist> UserPlaylists { get; set; }

        // ReSharper disable once UnusedMember.Global
        public NavigationBarViewModel() { }

        public NavigationBarViewModel(PlaylistService playlistService)
        {

            GotoHomePageCommand = new RelayCommand(OnHomePageButtonPressed);

            //todo add userid
            //todo retrieve song info here from SongService based on songIDs
            UserPlaylists = playlistService.GetFollowedPlaylistsByUserId(1);
            RaisePropertyChanged(nameof(UserPlaylists));
        }

        private void OpenPlaylistPage()
        {
            OnPlaylistSelected(SelectedPlaylist);
        }
    }
}
