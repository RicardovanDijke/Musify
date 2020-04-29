﻿using System;
using System.Printing.IndexedProperties;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.CurrentSong;
using Musify_Desktop_App.Panels.Home;
using Musify_Desktop_App.Panels.NavigationBar;
using Musify_Desktop_App.Panels.SongQueue;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App
{
    class MainViewModel : ViewModelBase
    {
        public ViewModelBase MainView
        {
            get => _mainView;
            set
            {
                _mainView = value;
                RaisePropertyChanged(nameof(MainView));
            }
        }

        public HomePageViewModel HomePageView { get; set; }
        public NavigationBarViewModel NavigationBarViewModel { get; set; }
        public CurrentSongViewModel CurrentSongView { get; set; }
        public FriendsActivityViewModel FriendsActivityView { get; set; }

        public SongQueueViewModel SongQueueViewModel { get; set; }


        private SongService _songService;
        private ViewModelBase _mainView;

        //todo use user
        public MainViewModel(User user)
        {
            _songService = new SongService();

            HomePageView = new HomePageViewModel(_songService);
            CurrentSongView = CurrentSongViewModel.Instance();
            FriendsActivityView = new FriendsActivityViewModel(_songService);
            SongQueueViewModel = new SongQueueViewModel(_songService);
            NavigationBarViewModel = new NavigationBarViewModel();


            CurrentSongView.QueuePageButtonPressed += GotoQueuePage;
            NavigationBarViewModel.HomePageButtonPressed += GotoHomePage;

            MainView = HomePageView;
        }


        private void GotoQueuePage(object sender, EventArgs e)
        {
            MainView = new SongQueueViewModel(_songService);
        }

        private void GotoHomePage(object sender, EventArgs e)
        {
            MainView = HomePageView;
        }
    }
}
