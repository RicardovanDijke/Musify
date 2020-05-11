using Musify_Desktop_App.Panels.Playlist;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.SongQueue
{
    internal class SongQueueViewModel : BasePanelNavigation
    {
        private static readonly object Lock = new object();
        private static SongQueueViewModel _instance;
        public static SongQueueViewModel Instance(SongService songService, IPlaylistService playlistService)
        {
            lock (Lock)
            {
                return _instance ??= new SongQueueViewModel(songService, playlistService);
            }

        }


        public PlaylistPageViewModel SongQueueListViewModel { get; set; }

        private SongQueueViewModel() { }
        private SongQueueViewModel(SongService songService, IPlaylistService playlistService)
        {
            SongQueueListViewModel = new PlaylistPageViewModel(songService, playlistService, SongPlayer.Instance.Queue, "Play Queue");
        }
    }
}
