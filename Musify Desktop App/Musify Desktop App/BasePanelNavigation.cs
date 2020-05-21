using System;
using GalaSoft.MvvmLight;
using Musify_Desktop_App.Model;

namespace Musify_Desktop_App
{
    internal abstract class BasePanelNavigation : ViewModelBase
    {
        public event EventHandler QueuePageRequested;
        public event EventHandler HomePageRequested;
        public event EventHandler ProfilePageRequested;
        public event EventHandler SongListPageRequested;

        protected virtual void OnQueuePageRequested()
        {
            QueuePageRequested?.Invoke(this, null);
        }
        protected virtual void OnProfilePageRequested(User user)
        {  
            ProfilePageRequested?.Invoke(user, null);
        }
        protected virtual void OnHomePageRequested()
        {
            HomePageRequested?.Invoke(this, null);
        }
        protected virtual void OnSongListPageRequested(SongList album)
        {
            SongListPageRequested?.Invoke(album, null);
        }
    }
}
