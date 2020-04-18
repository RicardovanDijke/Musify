using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;
using Microsoft.WindowsAPICodePack.Shell;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.CurrentSong;
using NAudio.Wave;

namespace Musify_Desktop_App.Service
{
    class SongPlayer : INotifyPropertyChanged
    {
        private static readonly object padlock = new object();
        private static SongPlayer instance;
        public static SongPlayer Instance
        {
            get
            {
                lock (padlock)
                {
                    return instance ??= new SongPlayer();
                }
            }
        }

        private readonly WaveOutEvent output;

        public List<Song> History { get; } = new List<Song>();

        private Song _currentSong;
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
        public List<Song> Queue { get; } = new List<Song>();


        public int VolumePercentage
        {
            get => (int)(output.Volume * 100);
            set
            {
                output.Volume = value / 100.000f;
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
            output = new WaveOutEvent();

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

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                while (true)
                {
                    ManagePlayback();
                }
            }).Start();
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

            output.Stop();

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
            var seconds = 0.0;
            double.TryParse(so.Properties.System.Media.Duration.Value.ToString(), out var nanoseconds);
            if (nanoseconds > 0)
            {
                seconds = Convert100NanosecondsToMilliseconds(nanoseconds) / 1000;
                Console.WriteLine(seconds.ToString());
            }


            var relativeDuration = seconds * (percentage / 100.00);
            songFileReader.CurrentTime = songFileReader.CurrentTime.Add(new TimeSpan(0, 0, 0, Convert.ToInt32(relativeDuration), 0));

            //songFileReader.Position = 10L;
            output.Init(songFileReader);
            //output.GetPosition()
            output.Play();

            CurrentSong = song;
        }

        public static double Convert100NanosecondsToMilliseconds(double nanoseconds)
        {
            // One million nanoseconds in 1 millisecond, 
            // but we are passing in 100ns units...
            return nanoseconds * 0.0001;
        }

        //todo add SongPercentage checking, update CurrentSongControl
        private void ManagePlayback()
        {
            if (output.PlaybackState == PlaybackState.Stopped && !switchingSong)
            {
                PlayNextSongInQueue();
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

            if (output.PlaybackState == PlaybackState.Playing)
            {
                output.Pause();
            }
            else
            {
                output.Play();
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
