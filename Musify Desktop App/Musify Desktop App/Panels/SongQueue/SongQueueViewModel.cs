using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.SongQueue
{
    internal class SongQueueViewModel : BasePanelNavigation
    {
        private static readonly object _lock = new object();
        private static SongQueueViewModel _instance;
        public static SongQueueViewModel Instance(SongService songService, IPlaylistService playlistService)
        {
            lock (_lock)
            {
                return _instance ??= new SongQueueViewModel(songService, playlistService);
            }

        }


        public SongListViewModel SongQueueListViewModel { get; set; }

        private SongQueueViewModel() { }
        private SongQueueViewModel(SongService songService, IPlaylistService playlistService)
        {
            SongQueueListViewModel = new SongListViewModel(songService, playlistService, SongPlayer.Instance.Queue, "Play Queue");
        }
    }
}
