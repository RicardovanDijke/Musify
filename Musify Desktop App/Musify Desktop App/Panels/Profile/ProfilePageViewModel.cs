using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
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
        private readonly IUserService _userService;

        public User User { get; set; }

        private bool _onOtherProfile;

        public bool OnOtherProfile
        {
            get => _onOtherProfile;
            set
            {
                _onOtherProfile = value;
                RaisePropertyChanged(nameof(OnOtherProfile));
            }
        }


        public RelayCommand FollowUserCommand { get; set; }
        public string FollowButtonText { get; set; }

        public PlaylistListViewModel PublicPlaylistsViewModel { get; set; }
        public UserListViewModel FollowedUsersViewModel { get; set; }
        public UserListViewModel FollowingUsersViewModel { get; set; }

        public ProfilePageViewModel() { }
        public ProfilePageViewModel(IPlaylistService playlistService, IUserService userService, User user)
        {
            _playlistService = playlistService;
            _userService = userService;
            User = user;
            OnOtherProfile = false;
            if (user.UserId != Session.User.UserId)
            {
                OnOtherProfile = true;
                if (user.Followers.Any(follower => follower.UserId == Session.User.UserId))
                {
                    FollowButtonText = "Unfollow";
                    FollowUserCommand = new RelayCommand(DoUnFollowUser);
                }
                else
                {
                    FollowButtonText = "Follow";
                    FollowUserCommand = new RelayCommand(DoFollowUser);
                }
            }


            PublicPlaylistsViewModel = new PlaylistListViewModel(_playlistService, _playlistService.GetPublicCreatedPlaylistsByUserId(user.UserId), "Public Playlists");
            PublicPlaylistsViewModel.SongListPageRequested += OnSongListPageRequested;

            FollowedUsersViewModel = new UserListViewModel(userService, user.Followers, "Followers");
            FollowedUsersViewModel.ProfilePageRequested += OnProfilePageRequested;

            FollowingUsersViewModel = new UserListViewModel(userService, user.Following, "Following");
            FollowingUsersViewModel.ProfilePageRequested += OnProfilePageRequested;
        }

        private void DoUnFollowUser()
        {
            _userService.RemoveFollowing(User.UserId, Session.User.UserId);
            OnProfilePageRequested(User);

        }

        private void DoFollowUser()
        {
            _userService.AddFollowing(User.UserId, Session.User.UserId);
            OnProfilePageRequested(User);
        }

        private void OnSongListPageRequested(object? sender, EventArgs e)
        {
            OnSongListPageRequested((SongList)sender);
        }

        private void OnProfilePageRequested(object? sender, EventArgs e)
        {
            OnProfilePageRequested((User)sender);
        }
    }
}

