﻿using System;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.CurrentSong
{
    internal class CurrentSongViewModel : BasePanelNavigation
    {
        private static readonly object Padlock = new object();
        private static CurrentSongViewModel _instance;
        public static CurrentSongViewModel Instance()
        {
            lock (Padlock)
            {
                return _instance ??= new CurrentSongViewModel();
            }
        }

        private int _songProgressPercentage;

        private Song _songPlaying;
        private int _songDuration;
        private int _songProgress;

        public Song SongPlaying
        {
            get => _songPlaying;
            set
            {
                _songPlaying = value;
                RaisePropertyChanged(nameof(SongPlaying));
            }
        }

        public int SongProgress
        {
            get => _songProgress;
            set
            {
                _songProgress = value;

                RaisePropertyChanged(nameof(SongProgress));
                RaisePropertyChanged(nameof(SongProgressString));
            }
        }


        public string SongProgressString
        {
            get
            {
                var t = TimeSpan.FromSeconds(SongProgress);
                return $"{t.Minutes:D2}:{t.Seconds:D2}";
            }
        }


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

        //todo rly ugly way
        public bool UpdateFromBackend;

        public int SongProgressPercentage
        {
            get => _songProgressPercentage;
            set
            {
                _songProgressPercentage = value;
                if (SongPlaying != null && !UpdateFromBackend)
                {
                    SongPlayer.Instance.PlaySong(SongPlaying, value);
                }
                RaisePropertyChanged(nameof(SongProgress));
                RaisePropertyChanged(nameof(SongProgressString));
                RaisePropertyChanged(nameof(SongProgressPercentage));

                UpdateFromBackend = false;
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


        public RelayCommand PlayPauseSongCommand { get; }
        public RelayCommand PlayNextSongInQueueCommand { get; }

        public RelayCommand OpenQueuePageCommand { get; }

        private CurrentSongViewModel()
        {
            SongProgressPercentage = 0;


            PlayPauseSongCommand = new RelayCommand(DoPlayPauseSong);
            PlayNextSongInQueueCommand = new RelayCommand(DoPlayNextSongInQueue);


            OpenQueuePageCommand = new RelayCommand(DoOpenQueuePage);
        }


        private void DoOpenQueuePage()
        {

            OnQueuePageRequested();
        }

        private void DoPlayNextSongInQueue()
        {
            SongPlayer.Instance.PlayNextSongInQueue();
        }

        private void DoPlayPauseSong()
        {
            SongPlayer.Instance.PlayPauseSong();
        }
    }
}
