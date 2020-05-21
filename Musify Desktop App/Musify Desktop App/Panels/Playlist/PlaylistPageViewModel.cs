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

namespace Musify_Desktop_App.Panels.Playlist
{
    internal class PlaylistPageViewModel : BasePanelNavigation
    {
        private readonly SongService _songService;
        private readonly IPlaylistService _playlistService;

        private ObservableCollection<Song> _songs = new ObservableCollection<Song>();

        public ObservableCollection<Song> Songs
        {
            get => _songs;
            set
            {
                _songs = value;
                RaisePropertyChanged(nameof(Songs));
            }
        }

        public List<MenuItem> SubMenuPlaylistItems { get; } = new List<MenuItem>();

        public string ListName { get; set; }

        public Song SelectedSong { get; set; }

        public IList SelectedSongs { get; set; }

        public RelayCommand SongSelectedCommand { get; set; }
        public RelayCommand PlaySongCommand { get; set; }
        public RelayCommand AddSongsToQueueCommand { get; set; }
        public RelayCommand OpenAlbumPageCommand { get; set; }
        public RelayCommand<Model.Playlist> AddSongToPlaylistCommand { get; set; }

        public PlaylistPageViewModel()
        {
            SongSelectedCommand = new RelayCommand(DoSongSelected);
            PlaySongCommand = new RelayCommand(DoPlaySong);
            AddSongsToQueueCommand = new RelayCommand(DoAddSelectedSongsToQueue);
            OpenAlbumPageCommand = new RelayCommand(DoOpenAlbumPage);
            AddSongToPlaylistCommand = new RelayCommand<Model.Playlist>(AddSelectedSongsToPlaylist);
        }

        public PlaylistPageViewModel(SongService songService, IPlaylistService playlistService, List<Song> songList,
            string listName) : this()
        {
            _songService = songService;
            _playlistService = playlistService;

            Songs = new ObservableCollection<Song>(songList);
            ListName = listName;

            CreateAddToPlaylistSubMenu();
        }
        public PlaylistPageViewModel(SongService songService, IPlaylistService playlistService, ObservableCollection<Song> songList,
            string listName) : this()
        {
            _songService = songService;
            _playlistService = playlistService;

            Songs = songList;
            ListName = listName;


            CreateAddToPlaylistSubMenu();
        }

        private void DoOpenAlbumPage()
        {
            OnSongListPageRequested(SelectedSong.Album);
        }

        private void CreateAddToPlaylistSubMenu()
        {
            //create submenu items for the songList contextmenu
            //todo create call for GetEditablePlaylistsByUserId
            var editablePlaylists = _playlistService.GetFollowedPlaylistsByUserId(Session.User.UserId);
            foreach (var playlist in editablePlaylists)
            {
                SubMenuPlaylistItems.Add(new MenuItem
                {
                    Header = playlist.Name,
                    Command = AddSongToPlaylistCommand,
                    CommandParameter = playlist
                });
            }
        }

        private void DoAddSelectedSongsToQueue()
        {
            var songs = SelectedSongs.Cast<Song>().ToList();

            SongPlayer.Instance.AddSongsToQueue(songs);
        }

        private void AddSelectedSongsToPlaylist(Model.Playlist playlist)
        {
            var songs = SelectedSongs.Cast<Song>().ToList();
            //check whether songs already exist in the playlist
            if (_playlistService.CheckSongsInPlaylist(playlist, songs))
            {
                var dialogvm = new DialogViewModel("Duplicate songs", "Some of these songs are already in this playlist");
                new BooleanDialog { DataContext = dialogvm }.ShowDialog();

                switch (dialogvm.Result)
                {
                    case MessageBoxResult.OK:
                        {
                            _playlistService.AddSongsToPlaylist(playlist, songs);
                            break;
                        }
                    case MessageBoxResult.Cancel:
                        {
                            break;
                        }
                }
            }
        }

        private void DoPlaySong()
        {
            _songService.RequestSocket(SelectedSong.SongId);
            SongSocket.NewSongSocket(SelectedSong.SongId);

            SongPlayer.Instance.PlaySong(SelectedSong);
        }

        private void DoSongSelected()
        {
            //todo
        }
    }
}
