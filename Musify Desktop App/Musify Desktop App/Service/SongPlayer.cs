using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using JetBrains.Annotations;
using Microsoft.WindowsAPICodePack.Shell;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.CurrentSong;
using NAudio.Wave;

namespace Musify_Desktop_App.Service
{
    //todo add events for song stopped playing 
    //todo rewrite song stopping / playing logic, add private variables that call output.stop() and output.play()
    internal class SongPlayer : INotifyPropertyChanged
    {
        //lock object
        private static readonly object Padlock = new object();
        private int _duration;
        private int _positionPercentage;
        private int _timePlayed;
        private Song _currentSong;
        private readonly WaveOutEvent _output;
        private Timer _songStatusTimer;

        private static SongPlayer _instance;
        public static SongPlayer Instance
        {
            get
            {
                lock (Padlock)
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
                CurrentSongViewModel.Instance().SongPlaying = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<Song> Queue { get; } = new ObservableCollection<Song>();

        /// <summary>
        /// 
        /// </summary>
        public int TimePlayed
        {
            get => _timePlayed;
            private set
            {
                _timePlayed = value;
                CurrentSongViewModel.Instance().SongProgress = value;
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
                CurrentSongViewModel.Instance().UpdateFromBackend = true;
                CurrentSongViewModel.Instance().SongProgressPercentage = value;

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
                CurrentSongViewModel.Instance().SongDuration = value;
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
                var volume = (value / 100.000f) - 0.01f;
                if (volume < 0f)
                {
                    _output.Volume = 0;
                }
                else
                {
                    _output.Volume = volume;

                }
                OnPropertyChanged(nameof(VolumePercentage));
            }
        }


        // private IWaveProvider songFileReader;
        private Mp3FileReader _songFileReader;

        private readonly DirectoryInfo _tempFolder = new DirectoryInfo(@"C:\users\ricar\Desktop\Musify\temp");
        private readonly DirectoryInfo _downloadFolder = new DirectoryInfo(@"C:\users\ricar\Desktop\Musify\downloaded");

        private SongPlayer()
        {
            _instance = this;
            _output = new WaveOutEvent
            {
                Volume = 0.5f
            };
            _output.PlaybackStopped += OnPlaybackStopped;
            //create new storage folders to save songs in
            //todo maybe move creating folders to somewhere else
            if (!Directory.Exists(_tempFolder.FullName))
            {
                Directory.CreateDirectory(_tempFolder.FullName);
            }

            if (!Directory.Exists(_downloadFolder.FullName))
            {
                Directory.CreateDirectory(_downloadFolder.FullName);
            }

            _songStatusTimer = new Timer(ManagePlayback, null, 0, 1000);
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs e)
        {
            //if (!_switchingSong)
            //{
            //    PlayNextSongInQueue();
            //}
        }

        private string GetSongPath(Song song)
        {
            var songFileName = $"{song.SongId}.mp3";
            //check if song exists in Downloaded fol$"{song.SongId}.mp3"der
            if (File.Exists(Path.Combine(_downloadFolder.FullName, songFileName)))
            {
                return Path.Combine(_downloadFolder.FullName, songFileName);
            }
            //else search in temp folder
            return Path.Combine(_tempFolder.FullName, songFileName);
            //todo else download the song instead of downloading immediately
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
            determine sample rate, use WaveFileReader or Mp3FileReader
             */

            _songFileReader = new Mp3FileReader(mp3Path);


            var so = ShellFile.FromFilePath(mp3Path);
            double.TryParse(so.Properties.System.Media.Duration.Value.ToString(), out var nanoseconds);
            if (nanoseconds > 0)
            {
                Duration = (int)(Convert100NanosecondsToMilliseconds(nanoseconds) / 1000);
            }

            var relativeDuration = Convert.ToInt32(Duration * (percentage / 100.00));

            TimePlayed = relativeDuration;

            _songFileReader.CurrentTime = _songFileReader.CurrentTime.Add(new TimeSpan(0, 0, 0, relativeDuration, 0));

            //songFileReader.Position = 10L;
            if (_output.PlaybackState != PlaybackState.Stopped)
            {
                //stop again cause it sometimes doesnt work?
                _output.Stop();
            }
            _output.Init(_songFileReader);
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

            //todo combine if statements maybe
            if (_output.PlaybackState == PlaybackState.Stopped)
            {
                PlayNextSongInQueue();
            }
        }

        public void PlayNextSongInQueue()
        {
            if (Queue.Count > 0)
            {
                PlaySong(Queue[0]);
                Application.Current.Dispatcher.Invoke(delegate
                {
                    Queue.RemoveAt(0);
                });
                OnPropertyChanged(nameof(Queue));
            }
        }

        public void AddSongsToQueue(List<Song> songs)
        {
            foreach (var song in songs)
            {
                Queue.Add(song);
            }
            OnPropertyChanged(nameof(Queue));
        }

        public void PlayPauseSong()
        {
            var timespan = _output.GetPosition();
            var length = _songFileReader.Length;
            var pos = _songFileReader.Position;
            // var duration =
            var seconds = 0.0;
            //// double.TryParse(so.Properties.System.Media.Duration.Value.ToString(), out var nanoseconds);
            // if (nanoseconds > 0)
            // {
            //     seconds = Convert100NanosecondsToMilliseconds(nanoseconds) / 1000;
            // }
            if (_output.PlaybackState == PlaybackState.Playing)
            {
                _output.Pause();
            }
            else
            {
                _output.Play();
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
