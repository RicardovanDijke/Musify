﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

        public IList SelectedSongs
        {
            get;
            set;
        }

        public RelayCommand SongSelectedCommand { get; set; }
        public RelayCommand PlaySongCommand { get; set; }
        public RelayCommand AddSongToQueueCommand { get; set; }


        public SongListViewModel() { }

        public SongListViewModel(SongService songService, Func<List<Song>> getSongMethod, string listName)
        {
            _songService = songService;
            //todo fix crash if Storage service not online
            Songs = new ObservableCollection<Song>(getSongMethod.Invoke());
            ListName = listName;

            SongSelectedCommand = new RelayCommand(DoSongSelected);
            PlaySongCommand = new RelayCommand(DoPlaySong);
            AddSongToQueueCommand = new RelayCommand(AddSelectedSongsToQueue);


        }

        private void AddSelectedSongsToQueue()
        {
            var songs = SelectedSongs.Cast<Song>().ToList();

            SongPlayer.Instance.AddSongsToQueue(songs);
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
