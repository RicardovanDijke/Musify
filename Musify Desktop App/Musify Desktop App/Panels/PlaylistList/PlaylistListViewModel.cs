using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Dialog;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;
using Musify_Desktop_App.Socket;

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
        
        public string ListName { get; set; }

        public RelayCommand OpenAlbumPageCommand
        {
            get;
            set;
        }

        public PlaylistListViewModel()
        {
            OpenAlbumPageCommand = new RelayCommand(DoOpenAlbumPage);
        }

        public PlaylistListViewModel(IPlaylistService playlistService, List<Model.Playlist> playlistList,
            string listName) : this()
        {
            _playlistService = playlistService;

            Playlists = new ObservableCollection<Model.Playlist>(playlistList);
            ListName = listName;
        }
     
        private void DoOpenAlbumPage()
        {
            //OnAlbumPageRequested(SelectedSong.Album);
        }
    }
}
