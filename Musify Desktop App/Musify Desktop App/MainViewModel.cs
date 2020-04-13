using GalaSoft.MvvmLight;
using Musify_Desktop_App.Panels.CurrentSong;
using Musify_Desktop_App.Panels.Home;

namespace Musify_Desktop_App
{
    class MainViewModel : ViewModelBase
    {
        public HomePageViewModel HomePageView { get; set; }
        public CurrentSongViewModel CurrentSongView { get; set; }
        public FriendsActivityViewModel FriendsActivityView { get; set; }

        public MainViewModel()
        {
            HomePageView = new HomePageViewModel();
            CurrentSongView = new CurrentSongViewModel(10);
            FriendsActivityView = new FriendsActivityViewModel();
        }
    }
}
