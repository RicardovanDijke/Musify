using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.SongQueue
{
    internal class SongQueueViewModel : BasePanelNavigation
    {
        private static readonly object _lock = new object();
        private static SongQueueViewModel _instance;
        public static SongQueueViewModel Instance(SongService songService)
        {
            lock (_lock)
            {
                return _instance ??= new SongQueueViewModel(songService);
            }

        }


        public SongListViewModel SongQueueListViewModel { get; set; }

        public SongQueueViewModel() { }
        private SongQueueViewModel(SongService songService)
        {
            SongQueueListViewModel = new SongListViewModel(songService, SongPlayer.Instance.Queue, "Play Queue");
        }
    }
}
