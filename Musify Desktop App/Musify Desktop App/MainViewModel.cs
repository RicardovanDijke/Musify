using System;
using System.Linq;
using GalaSoft.MvvmLight;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.CurrentSong;
using Musify_Desktop_App.Panels.FriendsActivity;
using Musify_Desktop_App.Panels.Home;
using Musify_Desktop_App.Panels.NavigationBar;
using Musify_Desktop_App.Panels.Playlist;
using Musify_Desktop_App.Panels.Profile;
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


        private readonly ISongService _songService;
        private readonly IPlaylistService _playlistService;
        private readonly IUserService _userService;

        public MainViewModel()
        {
            _songService = new SongService();
            _playlistService = new PlaylistService();
            _userService = new UserService();

            HomePageView = new HomePageViewModel(_songService, _playlistService);
            CurrentSongView = CurrentSongViewModel.Instance();
            FriendsActivityView = new FriendsActivityViewModel(_songService);
            SongQueueViewModel = SongQueueViewModel.Instance(_songService, _playlistService);
            NavigationBarViewModel = new NavigationBarViewModel(_songService, _playlistService);


            CurrentSongView.QueuePageRequested += GoToQueuePage;
            NavigationBarViewModel.HomePageRequested += GoToHomePage;
            NavigationBarViewModel.SongListPageRequested += GoToSongListPage;
            NavigationBarViewModel.ProfilePageRequested += GoToProfilePage;
            HomePageView.SongListPageRequested += GoToSongListPage;
            MainView = HomePageView;
        }

        private void GoToProfilePage(object sender, EventArgs e)
        {
            var user = (User)sender;


            user.Followers = _userService.GetFollowersByUser(user.UserId);
            user.Following = _userService.GetFollowingByUser(user.UserId);


            var profileViewModel = new ProfilePageViewModel(_playlistService, _userService, user);
            profileViewModel.ProfilePageRequested += GoToProfilePage;
            profileViewModel.SongListPageRequested += GoToSongListPage;
            MainView = profileViewModel;
        }

        private void GoToQueuePage(object sender, EventArgs e)
        {
            MainView = SongQueueViewModel;
        }

        private void GoToHomePage(object sender, EventArgs e)
        {
            MainView = HomePageView;
        }

        private void GoToSongListPage(object sender, EventArgs e)
        {
            var songList = _songService.GetSongsInSongList((SongList)sender);
            
            MainView = new PlaylistPageViewModel(_songService, _playlistService, songList, songList.Name);
        }
    }
}
