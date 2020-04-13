using System.Net.Mime;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.CurrentSong
{
    class CurrentSongViewModel : ViewModelBase
    {
        public static CurrentSongViewModel Instance { get; private set; }

        private Song _songPlaying;
        public Song SongPlaying
        {
            get => _songPlaying;
            set
            {
                _songPlaying = value;
                RaisePropertyChanged(nameof(SongPlaying));
            }
        }

        public int SongProgress { get; set; }
        public int SongProgressPercentage
        {
            get;
            set;
        }
        public int SongDuration { get; set; }


        public RelayCommand PlayPauseSongCommand { get; private set; }

        public CurrentSongViewModel() { }
        public CurrentSongViewModel(int progress)
        {
            SongProgressPercentage = progress;

            SongPlaying = new Song();
            Instance = this;

            PlayPauseSongCommand = new RelayCommand(DoPlayPauseSong);
        }

        private void DoPlayPauseSong()
        {
            SongPlayer.Instance.PlayPauseSong();
        }
    }
}
