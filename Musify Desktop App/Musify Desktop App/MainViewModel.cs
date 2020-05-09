using System;
using System.Linq;
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

        private BasePanelNavigation _mainView;
        public BasePanelNavigation MainView
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

        public MainViewModel()
        {
            _songService = new SongService();
            _playlistService = new PlaylistService();

            HomePageView = new HomePageViewModel(_songService, _playlistService);
            CurrentSongView = CurrentSongViewModel.Instance();
            FriendsActivityView = new FriendsActivityViewModel(_songService);
            SongQueueViewModel = SongQueueViewModel.Instance(_songService, _playlistService);
            NavigationBarViewModel = new NavigationBarViewModel(_songService, _playlistService);


            CurrentSongView.QueuePageRequested += GoToQueuePage;
            NavigationBarViewModel.HomePageRequested += GoToHomePage;
            NavigationBarViewModel.PlaylistPageRequested += GoToPlaylistPage;
            HomePageView.AlbumPageRequested += GoToAlbumPage;
            MainView = HomePageView;
        }


        private void GoToQueuePage(object sender, EventArgs e)
        {
            MainView = SongQueueViewModel;
        }

        private void GoToHomePage(object sender, EventArgs e)
        {
            MainView = HomePageView;
        }

        private void GoToAlbumPage(object sender, EventArgs e)
        {
            var album = (Album)sender;
            album = _songService.GetSongsInAlbum(album);
            var songsInAlbum = album.Songs.OrderBy(x => x.Number).Select(albumSong => albumSong.Song).ToList();

            MainView = new PlaylistPageViewModel(_songService, _playlistService, songsInAlbum, album.Name);
        }
        private void GoToPlaylistPage(object sender, EventArgs e)
        {
            var playlist = (Playlist)sender;
            var songsInPlaylist = playlist.Songs.OrderBy(x => x.Number).Select(playlistItem => playlistItem.Song).ToList();

            MainView = new PlaylistPageViewModel(_songService, _playlistService, songsInPlaylist, playlist.Name);
        }
    }
}
