using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

using Playlist = Musify_Desktop_App.Model.Playlist;

namespace Musify_Desktop_App.Panels.NavigationBar
{
    internal class NavigationBarViewModel : BasePanelNavigation
    {
        public RelayCommand GotoHomePageCommand { get; set; }
        
        
        public List<Model.Playlist> UserPlaylists

        public NavigationBarViewModel()
        {

            GotoHomePageCommand = new RelayCommand(OnHomePageButtonPressed);
        }
    }
}
