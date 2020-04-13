using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAudio.Wave;

namespace Musify_Desktop_App.Service
{
    class SongPlayer
    {
        public static SongPlayer Instance { get; private set; }

        private WaveOutEvent output;

        public SongPlayer()
        {
            Instance = this;
            output = new WaveOutEvent();


        }

        public void PlaySong()
        {

            output.Stop();

            var i = new DirectoryInfo(@"C:\users\ricar\Desktop\temp").GetFiles().Length;

            var mp3 = @"C:\users\ricar\Desktop\temp\song" + (i - 1) + ".mp3";
            /*todo fix crash on Boulevard of Broken Dreams
             System.InvalidOperationException: 
             'Got a frame at sample rate 48000, in an MP3 with sample rate 44100. 
             Mp3FileReader does not support sample rate changes.'
             
            fix: https://stackoverflow.com/questions/31453107/got-a-frame-at-sample-rate-44100-in-an-mp3-with-sample-rate-48000-mp3filereade
            use determine sample rate, use WaveFileReader or Mp3FileReader
             */

            var mp3Reader = new Mp3FileReader(mp3);
            output.Init(mp3Reader);
            output.Play();

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
    }
}
