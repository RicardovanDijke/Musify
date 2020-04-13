using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.CurrentSong;
using Musify_Desktop_App.Service;
using Musify_Desktop_App.Socket;

namespace Musify_Desktop_App.Panels.Home
{
    class HomePageViewModel : ViewModelBase
    {
        private SongService songService;
        private Song _selectedSong;

        private SongPlayer songPlayer;
        public ObservableCollection<Song> Songs { get; set; }

        public Song SelectedSong
        {
            get => _selectedSong;
            set
            {
                _selectedSong = value;
                DoSongSelected();
            }
        }

        public RelayCommand SongSelectedCommand
        {
            get;
            set;
        }

        public HomePageViewModel()
        {
            songService = new SongService();

            Songs = new ObservableCollection<Song>(songService.GetAllSongs().Result);

            songPlayer = new SongPlayer();

            SongSelectedCommand = new RelayCommand(DoSongSelected);
        }

        private void DoSongSelected()
        {
            songService.RequestSocket(SelectedSong.SongID);
            SongSocket.NewSongSocket();
            CurrentSongViewModel.Instance.SongPlaying = SelectedSong;

            songPlayer.PlaySong();
        }
    }
}
