using System;
using System.Diagnostics;
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

        private int _songProgressPercentage;

        public Song SongPlaying => SongPlayer.Instance.CurrentSong;

        public int SongProgress { get; set; }

        public int VolumePercentage
        {
            get => SongPlayer.Instance.VolumePercentage;
            set
            {
                if (SongPlayer.Instance.VolumePercentage != value)
                {
                    SongPlayer.Instance.VolumePercentage = value;
                    RaisePropertyChanged(nameof(VolumePercentage));
                }
            }
        }

        public int SongProgressPercentage
        {
            get => _songProgressPercentage;
            set
            {
                _songProgressPercentage = value;
                Debug.WriteLine(_songProgressPercentage);
                if (SongPlaying != null)
                {
                    SongPlayer.Instance.PlaySong(SongPlaying, value);
                }
            }
        }

        public int SongDuration { get; set; }


        public RelayCommand PlayPauseSongCommand { get; private set; }
        public RelayCommand PlayNextSongInQueueCommand { get; private set; }

        public CurrentSongViewModel()
        {
            SongProgressPercentage = 0;

            Instance = this;


            PlayPauseSongCommand = new RelayCommand(DoPlayPauseSong);
            PlayNextSongInQueueCommand = new RelayCommand(DoPlayNextSongInQueueComamand);

        }

        private void DoPlayNextSongInQueueComamand()
        {
            SongPlayer.Instance.PlayNextSongInQueue();
        }

        private void DoPlayPauseSong()
        {
            SongPlayer.Instance.PlayPauseSong();
        }
    }
}
