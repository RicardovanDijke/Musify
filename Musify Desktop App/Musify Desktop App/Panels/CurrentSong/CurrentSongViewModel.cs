using GalaSoft.MvvmLight;
using Musify_Desktop_App.Model;

namespace Musify_Desktop_App.Panels.CurrentSong
{
    class CurrentSongViewModel : ViewModelBase
    {
        public Song SongPlaying { get; set; }

        public int SongProgress { get; set; }
        public int SongProgressPercentage
        {
            get;
            set;
        }
        public int SongDuration { get; set; }


        public CurrentSongViewModel()
        {

        }
        public CurrentSongViewModel(int progress)
        {
            SongProgressPercentage = progress;

            SongPlaying = new Song();
        }
    }
}
