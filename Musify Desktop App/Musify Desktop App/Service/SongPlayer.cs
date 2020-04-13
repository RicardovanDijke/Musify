using System;
using System.Collections.Generic;
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
            var mp3Reader = new Mp3FileReader("C:\\users\\ricar\\Desktop\\temp\\song.mp3");

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
