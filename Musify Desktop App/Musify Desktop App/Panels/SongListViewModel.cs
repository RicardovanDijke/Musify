using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.CurrentSong;
using Musify_Desktop_App.Service;
using Musify_Desktop_App.Socket;

namespace Musify_Desktop_App.Panels
{
    class SongListViewModel : ViewModelBase
    {
        private SongService songService;
        private Song _selectedSong;
        
        public ObservableCollection<Song> Songs { get; set; }

        public Song SelectedSong
        {
            get => _selectedSong;
            set
            {
                _selectedSong = value;
                // DoSongSelected();
            }
        }

        public RelayCommand SongSelectedCommand { get; set; }
        public RelayCommand PlaySongCommand { get; set; }

        public RelayCommand AddSongToQueueCommand { get; set; }


        public SongListViewModel()
        {
            songService = new SongService();

            Songs = new ObservableCollection<Song>(songService.GetAllSongs().Result);
            
            SongSelectedCommand = new RelayCommand(DoSongSelected);
            PlaySongCommand = new RelayCommand(PlaySong);


            AddSongToQueueCommand = new RelayCommand(AddSongToQueue);
        }

        private void AddSongToQueue()
        {
            SongPlayer.Instance.AddSongToQueue(SelectedSong);
        }

        private void PlaySong()
        {
            songService.RequestSocket(SelectedSong.SongID);
            SongSocket.NewSongSocket(SelectedSong.SongID);
            //CurrentSongViewModel.Instance.SongPlaying = SelectedSong;

            SongPlayer.Instance.PlaySong(SelectedSong);
        }

        private void DoSongSelected()
        {
            //todo
        }
    }
}
