using System;
using System.Diagnostics;
using System.Net.Mime;
using System.Threading;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.CurrentSong
{
    class CurrentSongViewModel : ViewModelBase
    {
        private static readonly object padlock = new object();
        private static CurrentSongViewModel instance;
        public static CurrentSongViewModel Instance
        {
            get
            {
                lock (padlock)
                {
                    return instance ??= new CurrentSongViewModel();
                }
            }
        }

        private int _songProgressPercentage;

        private Song _songPlaying;
        private int _songDuration;
        private string _songDurationString;


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


        public string SongDurationString
        {
            get
            {
                var t = TimeSpan.FromSeconds(SongDuration);
                return $"{t.Minutes:D2}:{t.Seconds:D2}";
            }
        }

        public int SongDuration
        {
            get => _songDuration;
            set
            {
                _songDuration = value;
                RaisePropertyChanged(nameof(SongDuration));
                RaisePropertyChanged(nameof(SongDurationString));
            }
        }


        public RelayCommand PlayPauseSongCommand { get; private set; }
        public RelayCommand PlayNextSongInQueueCommand { get; private set; }

        private CurrentSongViewModel()
        {
            SongProgressPercentage = 0;


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
