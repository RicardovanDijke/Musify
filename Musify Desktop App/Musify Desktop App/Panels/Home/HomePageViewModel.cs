using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.Home
{
    class HomePageViewModel : ViewModelBase
    {
        private SongService songService;

        public ObservableCollection<Song> Songs { get; set; }


        public HomePageViewModel()
        {
            songService = new SongService();

            Songs = new ObservableCollection<Song>(songService.GetAllSongs().Result);

        }
    }
}
