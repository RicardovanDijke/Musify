using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.SongQueue
{
    class SongQueueViewModel : BasePanelNavigation
    {
        private readonly object _lock = new object();
        private static SongQueueViewModel _instance;
        public SongQueueViewModel Instance(SongService songService)
        {
            lock (_lock)
            {
                return _instance ??= new SongQueueViewModel(songService);
            }

        }


        public SongListViewModel SongQueueListViewModel { get; set; }

        public SongQueueViewModel() { }
        public SongQueueViewModel(SongService songService)
        {

            SongQueueListViewModel = new SongListViewModel(songService, () => SongPlayer.Instance.Queue, "Play Queue");
        }
    }
}
