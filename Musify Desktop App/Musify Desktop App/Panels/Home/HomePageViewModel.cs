using System;
using System.Collections.ObjectModel;
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
        public HomePageViewModel(ISongService songService, IPlaylistService playlistService)
        {
            AllSongsListViewModel = new PlaylistPageViewModel(songService, playlistService, new ObservableCollection<Song>(songService.GetAllSongs()), "Recently Added");

            AllSongsListViewModel.SongListPageRequested += OnSongListPageRequested;
            WelcomeText = $"Welcome, {Session.User.DisplayName}";
        }

        protected virtual void OnSongListPageRequested(object sender, EventArgs e)
        {
            OnSongListPageRequested((Album)sender);
        }
    }
}
