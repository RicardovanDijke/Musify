using GalaSoft.MvvmLight;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.Home
{
    internal class HomePageViewModel : ViewModelBase
    {

        public SongListViewModel AllSongsListViewModel
        {
            get;
            set;
        }

        public HomePageViewModel() { }
        public HomePageViewModel(SongService songService)
        {
            AllSongsListViewModel = new SongListViewModel(songService, songService.GetAllSongs(), "Recently Added");
            RaisePropertyChanged(nameof(AllSongsListViewModel));
        }
    }
}
