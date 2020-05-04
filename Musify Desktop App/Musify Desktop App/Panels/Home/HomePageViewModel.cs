using System;
using GalaSoft.MvvmLight;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.Home
{
    internal class HomePageViewModel : ViewModelBase
    {
        public String WelcomeText { get; }


        private SongListViewModel _allSongsListViewModel;
        public SongListViewModel AllSongsListViewModel
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
            AllSongsListViewModel = new SongListViewModel(songService, playlistService, songService.GetAllSongs(), "Recently Added");
            WelcomeText = $"Welcome, {Session.User.DisplayName}";
        }
    }
}
