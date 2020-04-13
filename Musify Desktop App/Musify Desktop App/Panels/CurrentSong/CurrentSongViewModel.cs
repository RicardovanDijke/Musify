using System.Net.Mime;
using System.Windows;
using GalaSoft.MvvmLight;
using Musify_Desktop_App.Model;

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


        public CurrentSongViewModel()
        {
            Instance = this;

        }
        public CurrentSongViewModel(int progress)
        {
            SongProgressPercentage = progress;

            SongPlaying = new Song();
            Instance = this;
        }
    }
}
