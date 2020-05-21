using System;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.PlaylistList;
using Musify_Desktop_App.Panels.UserList;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.Profile
{
    class ProfilePageViewModel : BasePanelNavigation
    {
        private readonly IPlaylistService _playlistService;
        public User User { get; set; }
        public bool OnOwnProfile { get; set; }


        public RelayCommand FollowUserCommand { get; set; }

        public PlaylistListViewModel PublicPlaylistsViewModel { get; set; }
        public UserListViewModel FollowedUsersViewModel { get; set; }

        public ProfilePageViewModel() { }
        public ProfilePageViewModel(IPlaylistService playlistService, IUserService userService, User user)
        {
            _playlistService = playlistService;
            User = user;
            if (user.UserId == Session.User.UserId)
            {
                OnOwnProfile = true;
            }

            PublicPlaylistsViewModel = new PlaylistListViewModel(_playlistService, _playlistService.GetPublicCreatedPlaylistsByUserId(user.UserId), "Public Playlists");
            PublicPlaylistsViewModel.SongListPageRequested += OnSongListPageRequested;

            FollowedUsersViewModel = new UserListViewModel(userService, user.Followers, "Following");
            FollowedUsersViewModel.ProfilePageRequested += OnProfilePageRequested;

            FollowUserCommand = new RelayCommand(DoFollowUnfollowUser);
        }

        private void OnSongListPageRequested(object? sender, EventArgs e)
        {
            OnSongListPageRequested((SongList) sender);
        }

        private void OnProfilePageRequested(object? sender, EventArgs e)
        {
            OnProfilePageRequested((User)sender);
        }

        private void DoFollowUnfollowUser()
        {
            //todo follow/unfollow user
        }
    }
}

