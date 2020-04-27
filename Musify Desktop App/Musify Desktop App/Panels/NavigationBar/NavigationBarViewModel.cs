using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Musify_Desktop_App.Panels.NavigationBar
{
    class NavigationBarViewModel : BasePanelNavigation
    {


        public RelayCommand GotoHomePageCommand { get; set; }



        public NavigationBarViewModel()
        {

            GotoHomePageCommand = new RelayCommand(OnHomePageButtonPressed);
        }
    }
}
