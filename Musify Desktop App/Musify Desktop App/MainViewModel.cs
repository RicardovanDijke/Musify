using System.Printing.IndexedProperties;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Panels.CurrentSong;
using Musify_Desktop_App.Panels.Home;
using Musify_Desktop_App.Panels.SongQueue;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App
{
    class MainViewModel : ViewModelBase
    {
        public ViewModelBase MainView { get; set; }
        public HomePageViewModel HomePageView { get; set; }
        public CurrentSongViewModel CurrentSongView { get; set; }
        public FriendsActivityViewModel FriendsActivityView { get; set; }

        public SongQueueViewModel SongQueueViewModel { get; set; }


        private SongService _songService;
        public MainViewModel()
        {
            _songService = new SongService();

            HomePageView = new HomePageViewModel(_songService);
            CurrentSongView = CurrentSongViewModel.Instance();
            FriendsActivityView = new FriendsActivityViewModel(_songService);


            MainView = HomePageView;
        }
    }
}
