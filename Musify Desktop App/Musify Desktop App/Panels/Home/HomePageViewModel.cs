using GalaSoft.MvvmLight;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.Home
{
    class HomePageViewModel : ViewModelBase
    {

        public SongListViewModel AllSongsListViewModel
        {
            get;
            set;
        }

        public HomePageViewModel() { }
        public HomePageViewModel(SongService songService)
        {
            AllSongsListViewModel = new SongListViewModel(songService, songService.GetAllSongs);
            RaisePropertyChanged(nameof(AllSongsListViewModel));
        }
    }
}
