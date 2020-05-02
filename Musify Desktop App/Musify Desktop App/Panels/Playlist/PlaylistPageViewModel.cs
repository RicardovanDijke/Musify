using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.Playlist
{
    internal class PlaylistPageViewModel : BasePanelNavigation
    {

        public SongListViewModel SongQueueListViewModel { get; set; }

        public PlaylistPageViewModel() { }
        public PlaylistPageViewModel(SongService songService, Model.Playlist playlist)
        {

            SongQueueListViewModel = new SongListViewModel(songService, () => playlist.Songs, playlist.Name);
        }
    }
}
