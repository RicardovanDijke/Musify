using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.WindowsAPICodePack.Shell;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.CurrentSong;
using NAudio.Utils;
using NAudio.Wave;

namespace Musify_Desktop_App.Service
{
    //todo add events for song stopped playing 
    class SongPlayer : INotifyPropertyChanged
    {
        //lock object
        private static readonly object padlock = new object();
        private int _duration;
        private int _positionPercentage;
        private int _timePlayed;
        private Song _currentSong;
        private readonly WaveOutEvent _output;
        private Timer songStatusTimer;

        private static SongPlayer _instance;
        public static SongPlayer Instance
        {
            get
            {
                lock (padlock)
                {
                    return _instance ??= new SongPlayer();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Song> History { get; } = new List<Song>();

        /// <summary>
        /// 
        /// </summary>
        public Song CurrentSong
        {
            get => _currentSong;
            set
            {
                _currentSong = value;
                OnPropertyChanged(nameof(CurrentSong));
                CurrentSongViewModel.Instance.SongPlaying = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<Song> Queue { get; } = new List<Song>();

        /// <summary>
        /// 
        /// </summary>
        public int TimePlayed
        {
            get => _timePlayed;
            private set
            {
                _timePlayed = value;
                CurrentSongViewModel.Instance.SongProgress = value;
                OnPropertyChanged(nameof(TimePlayed));
            }
        }


        /// <summary>
        /// 
        /// </summary>
        public int PositionPercentage
        {
            get => _positionPercentage;
            private set
            {
                _positionPercentage = value;
                CurrentSongViewModel.Instance.UpdateFromBackend = true;
                CurrentSongViewModel.Instance.SongProgressPercentage = value;

                OnPropertyChanged(nameof(PositionPercentage));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Duration
        {
            get => _duration;
            private set
            {
                _duration = value;
                CurrentSongViewModel.Instance.SongDuration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int VolumePercentage
        {
            get => (int)(_output.Volume * 100);
            set
            {
                _output.Volume = value / 100.000f;
                OnPropertyChanged(nameof(VolumePercentage));
            }
        }


        // private IWaveProvider songFileReader;
        private Mp3FileReader songFileReader;

        private readonly DirectoryInfo tempFolder = new DirectoryInfo(@"C:\users\ricar\Desktop\Musify\temp");
        private readonly DirectoryInfo downloadFolder = new DirectoryInfo(@"C:\users\ricar\Desktop\Musify\downloaded");

        private bool switchingSong;


        private SongPlayer()
        {
            _instance = this;
            _output = new WaveOutEvent();
            _output.PlaybackStopped += OnPlaybackStopped;
            //create new storage folders to save songs in
            //todo maybe move to somewhere else
            if (!Directory.Exists(tempFolder.FullName))
            {
                Directory.CreateDirectory(tempFolder.FullName);
            }

            if (!Directory.Exists(downloadFolder.FullName))
            {
                Directory.CreateDirectory(downloadFolder.FullName);
            }

            songStatusTimer = new Timer(ManagePlayback, null, 0, 1000);
        }

        private void OnPlaybackStopped(object? sender, StoppedEventArgs e)
        {
            PlayNextSongInQueue();
        }

        private string GetSongPath(Song song)
        {
            //check if song exists in Downloaded folder
            if (File.Exists(Path.Combine(downloadFolder.FullName, $"{song.SongID}.mp3")))
            {
                return Path.Combine(downloadFolder.FullName, $"{song.SongID}.mp3");
            }
            //else search in temp folder
            return Path.Combine(tempFolder.FullName, $"{song.SongID}.mp3");
        }

        public void PlaySong(Song song, int percentage = 0)
        {
            var mp3Path = GetSongPath(song);

            _output.Stop();

            /*todo fix crash on Boulevard of Broken Dreams
             System.InvalidOperationException: 
             'Got a frame at sample rate 48000, in an MP3 with sample rate 44100. 
             Mp3FileReader does not support sample rate changes.'

            fix: https://stackoverflow.com/questions/31453107/got-a-frame-at-sample-rate-44100-in-an-mp3-with-sample-rate-48000-mp3filereade
            use determine sample rate, use WaveFileReader or Mp3FileReader
             */

            // songFileReader.

            songFileReader = new Mp3FileReader(mp3Path);


            var so = ShellFile.FromFilePath(mp3Path);
            double.TryParse(so.Properties.System.Media.Duration.Value.ToString(), out var nanoseconds);
            if (nanoseconds > 0)
            {
                Duration = (int)(Convert100NanosecondsToMilliseconds(nanoseconds) / 1000);
            }

            var relativeDuration = Convert.ToInt32(Duration * (percentage / 100.00));

            TimePlayed = relativeDuration;

            songFileReader.CurrentTime = songFileReader.CurrentTime.Add(new TimeSpan(0, 0, 0, relativeDuration, 0));

            //songFileReader.Position = 10L;
            _output.Init(songFileReader);
            //output.GetPosition()
            _output.Play();

            CurrentSong = song;
        }

        public static double Convert100NanosecondsToMilliseconds(double nanoseconds)
        {
            // One million nanoseconds in 1 millisecond, 
            // but we are passing in 100ns units...
            return nanoseconds * 0.0001;
        }

        //todo add SongPercentage checking, update CurrentSongControl
        private void ManagePlayback(object state)
        {
            if (CurrentSong != null)
            {
                TimePlayed++;
                var percentage = (double)TimePlayed / (double)Duration * 100.0;
                PositionPercentage = (int)percentage;
            }
        }

        public void PlayNextSongInQueue()
        {
            if (Queue.Count > 0)
            {
                switchingSong = true;
                PlaySong(Queue[0]);
                Queue.RemoveAt(0);

                switchingSong = false;
            }
        }

        public void AddSongToQueue(Song song)
        {
            Queue.Add(song);
        }

        public void PlayPauseSong()
        {
            var timespan = _output.GetPosition();
            var length = songFileReader.Length;
            var pos = songFileReader.Position;
            // var duration =
            var seconds = 0.0;
            //// double.TryParse(so.Properties.System.Media.Duration.Value.ToString(), out var nanoseconds);
            // if (nanoseconds > 0)
            // {
            //     seconds = Convert100NanosecondsToMilliseconds(nanoseconds) / 1000;
            // }

        }



        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
