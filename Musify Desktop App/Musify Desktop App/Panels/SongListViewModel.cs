using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;
using Musify_Desktop_App.Socket;

namespace Musify_Desktop_App.Panels
{
    class SongListViewModel : ViewModelBase
    {
        private readonly SongService _songService;

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

        public string ListName { get; set; }

        public Song SelectedSong { get; set; }

        public RelayCommand SongSelectedCommand { get; set; }
        public RelayCommand PlaySongCommand { get; set; }
        public RelayCommand AddSongToQueueCommand { get; set; }


        public SongListViewModel() { }

        public SongListViewModel(SongService songService, Func<List<Song>> getSongMethod, string listName)
        {
            _songService = songService;
            Songs = new ObservableCollection<Song>(getSongMethod.Invoke());
            ListName = listName;

            SongSelectedCommand = new RelayCommand(DoSongSelected);
            PlaySongCommand = new RelayCommand(DoPlaySong);
            AddSongToQueueCommand = new RelayCommand(AddSongToQueue);


        }

        private void AddSongToQueue()
        {
            SongPlayer.Instance.AddSongToQueue(SelectedSong);
        }

        private void DoPlaySong()
        {
            _songService.RequestSocket(SelectedSong.SongID);
            SongSocket.NewSongSocket(SelectedSong.SongID);

            SongPlayer.Instance.PlaySong(SelectedSong);
        }

        private void DoSongSelected()
        {
            //todo
        }
    }
}
