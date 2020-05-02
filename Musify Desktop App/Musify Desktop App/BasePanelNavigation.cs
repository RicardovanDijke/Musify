using System;
using GalaSoft.MvvmLight;
using Musify_Desktop_App.Model;

namespace Musify_Desktop_App
{
    internal abstract class BasePanelNavigation : ViewModelBase
    {
        public event EventHandler QueuePageButtonPressed;
        public event EventHandler HomePageButtonPressed;
        public event EventHandler PlaylistSelected;

        protected virtual void OnQueuePageButtonPressed()
        {
            QueuePageButtonPressed?.Invoke(this, null);
        }

        protected virtual void OnHomePageButtonPressed()
        {
            HomePageButtonPressed?.Invoke(this, null);
        }
        protected virtual void OnPlaylistSelected(Playlist playlist)
        {
            PlaylistSelected?.Invoke(playlist, null);
        }
    }
}
