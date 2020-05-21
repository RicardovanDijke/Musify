using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.PlaylistList
{
    internal class PlaylistListViewModel : BasePanelNavigation
    {
        private readonly IPlaylistService _playlistService;

        private ObservableCollection<Model.Playlist> _playlists = new ObservableCollection<Model.Playlist>();

        public ObservableCollection<Model.Playlist> Playlists
        {
            get => _playlists;
            set
            {
                _playlists = value;
                RaisePropertyChanged(nameof(Playlists));
            }
        }

        public Model.Playlist SelectedPlaylist { get; set; }

        public string ListName { get; set; }

        public RelayCommand OpenPlaylistPageCommand
        {
            get;
            set;
        }

        public PlaylistListViewModel()
        {
            OpenPlaylistPageCommand = new RelayCommand(DoOpenPlaylistPage);
        }

        public PlaylistListViewModel(IPlaylistService playlistService, List<Model.Playlist> playlistList,
            string listName) : this()
        {
            _playlistService = playlistService;

            Playlists = new ObservableCollection<Model.Playlist>(playlistList);
            ListName = listName;
        }

        private void DoOpenPlaylistPage()
        {
            OnSongListPageRequested(SelectedPlaylist);
        }
    }
}
