using System;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.Playlist;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.Home
{
    internal class HomePageViewModel : BasePanelNavigation
    {
        public String WelcomeText { get; }


        private PlaylistPageViewModel _allSongsListViewModel;
        public PlaylistPageViewModel AllSongsListViewModel
        {
            get => _allSongsListViewModel;
            set
            {
                _allSongsListViewModel = value;
                RaisePropertyChanged(nameof(AllSongsListViewModel));
            }
        }

        public HomePageViewModel() { }
        public HomePageViewModel(SongService songService, PlaylistService playlistService)
        {
            AllSongsListViewModel = new PlaylistPageViewModel(songService, playlistService, songService.GetAllSongs(), "Recently Added");

            AllSongsListViewModel.AlbumPageRequested += OnAlbumPageRequested;
            WelcomeText = $"Welcome, {Session.User.DisplayName}";
        }

        protected virtual void OnAlbumPageRequested(object sender, EventArgs e)
        {
            OnAlbumPageRequested((Album)sender);
        }
    }
}
