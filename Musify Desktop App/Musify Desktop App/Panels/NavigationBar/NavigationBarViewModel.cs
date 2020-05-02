using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Service;
using Playlist = Musify_Desktop_App.Model.Playlist;

namespace Musify_Desktop_App.Panels.NavigationBar
{
    internal class NavigationBarViewModel : BasePanelNavigation
    {
        public RelayCommand GotoHomePageCommand { get; set; }


        public List<Model.Playlist> UserPlaylists { get; set; }

        public NavigationBarViewModel() { }

        public NavigationBarViewModel(PlaylistService playlistService)
        {

            GotoHomePageCommand = new RelayCommand(OnHomePageButtonPressed);

            //todo add userid
            UserPlaylists = playlistService.GetFollowedPlaylistsByUserId(1);
            RaisePropertyChanged(nameof(UserPlaylists));
        }
    }
}
