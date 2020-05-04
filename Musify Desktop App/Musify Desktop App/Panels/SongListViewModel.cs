using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;
using Musify_Desktop_App.Socket;

namespace Musify_Desktop_App.Panels
{
    internal class SongListViewModel : ViewModelBase
    {
        private readonly SongService _songService;
        private readonly PlaylistService _playlistService;

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

        public IList SelectedSongs
        {
            get;
            set;
        }

        public RelayCommand SongSelectedCommand { get; set; }
        public RelayCommand PlaySongCommand { get; set; }
        public RelayCommand AddSongsToQueueCommand { get; set; }

        public ICommand AddSongToPlaylistCommand
        {
            get;
            set;
        }

        public SongListViewModel() { }

        public SongListViewModel(SongService songService, PlaylistService playlistService, ObservableCollection<Song> songList, string listName) : base()
        {
            _songService = songService;
            _playlistService = playlistService;

            Songs = songList;
            ListName = listName;

            SongSelectedCommand = new RelayCommand(DoSongSelected);
            PlaySongCommand = new RelayCommand(DoPlaySong);
            AddSongsToQueueCommand = new RelayCommand(AddSelectedSongsToQueue);
            AddSongToPlaylistCommand = new RelayCommand<Model.Playlist>(AddSelectedSongsToPlaylist);


            GetEditablePlaylists();
        }

        public SongListViewModel(SongService songService, PlaylistService playlistService, IEnumerable<Song> songList, string listName)
        {
            _songService = songService;
            _playlistService = playlistService;
            //todo fix crash if Storage service not online

            Songs = new ObservableCollection<Song>(songList);
            ListName = listName;

            SongSelectedCommand = new RelayCommand(DoSongSelected);
            PlaySongCommand = new RelayCommand(DoPlaySong);
            AddSongsToQueueCommand = new RelayCommand(AddSelectedSongsToQueue);
            AddSongToPlaylistCommand = new RelayCommand<Model.Playlist>(AddSelectedSongsToPlaylist);

            GetEditablePlaylists();
        }


        private void GetEditablePlaylists()
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

        private void AddSelectedSongsToQueue()
        {
            var songs = SelectedSongs.Cast<Song>().ToList();

            SongPlayer.Instance.AddSongsToQueue(songs);
        }

        private void AddSelectedSongsToPlaylist(Model.Playlist playlist)
        {
            var songs = SelectedSongs.Cast<Song>().ToList();
            _playlistService.AddSongsToPlaylist(playlist, songs);
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
