using System;
using GalaSoft.MvvmLight;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.CurrentSong;
using Musify_Desktop_App.Panels.Home;
using Musify_Desktop_App.Panels.NavigationBar;
using Musify_Desktop_App.Panels.Playlist;
using Musify_Desktop_App.Panels.SongQueue;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App
{
    internal class MainViewModel : ViewModelBase
    {
        public ViewModelBase MainView
        {
            get => _mainView;
            set
            {
                _mainView = value;
                RaisePropertyChanged(nameof(MainView));
            }
        }

        public HomePageViewModel HomePageView { get; set; }
        public NavigationBarViewModel NavigationBarViewModel { get; set; }
        public CurrentSongViewModel CurrentSongView { get; set; }
        public FriendsActivityViewModel FriendsActivityView { get; set; }

        public SongQueueViewModel SongQueueViewModel { get; set; }


        private readonly SongService _songService;
        private readonly PlaylistService _playlistService;
        private ViewModelBase _mainView;

        public MainViewModel()
        {
            _songService = new SongService();
            _playlistService = new PlaylistService();

            HomePageView = new HomePageViewModel(_songService, _playlistService);
            CurrentSongView = CurrentSongViewModel.Instance();
            FriendsActivityView = new FriendsActivityViewModel(_songService);
            SongQueueViewModel = SongQueueViewModel.Instance(_songService, _playlistService);
            NavigationBarViewModel = new NavigationBarViewModel(_songService, _playlistService);


            CurrentSongView.QueuePageButtonPressed += GotoQueuePage;
            NavigationBarViewModel.HomePageButtonPressed += GotoHomePage;
            NavigationBarViewModel.PlaylistSelected += OpenPlaylistPage;

            MainView = HomePageView;
        }

        private void GotoQueuePage(object sender, EventArgs e)
        {
            //MainView = new SongQueueViewModel(_songService);
            MainView = SongQueueViewModel;
        }

        private void GotoHomePage(object sender, EventArgs e)
        {
            MainView = HomePageView;
        }
        private void OpenPlaylistPage(object sender, EventArgs e)
        {
            var playlist = (Playlist)sender;
            MainView = new PlaylistPageViewModel(_songService, _playlistService, playlist);
        }
    }
}
