using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Musify_Desktop_App
{
    abstract class BasePanelNavigation : ViewModelBase
    {
        public event EventHandler QueuePageButtonPressed;
        public event EventHandler HomePageButtonPressed;
        
        protected virtual void OnQueuePageButtonPressed()
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler handler = QueuePageButtonPressed;
            if (handler != null)
            {
                handler(this, null);
            }
        }

        protected virtual void OnHomePageButtonPressed()
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler handler = HomePageButtonPressed;
            if (handler != null)
            {
                handler(this, null);
            }
        }
    }
}
