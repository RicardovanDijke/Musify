using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using JetBrains.Annotations;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Panels.PlaylistList;
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

        public ProfilePageViewModel() { }
        public ProfilePageViewModel(IPlaylistService playlistService, User user)
        {
            _playlistService = playlistService;

            if (user.UserId == Session.User.UserId)
            {
                OnOwnProfile = true;
            }

            PublicPlaylistsViewModel = new PlaylistListViewModel(_playlistService, _playlistService.GetPublicCreatedPlaylistsByUserId(user.UserId), "Public Playlists");

            FollowUserCommand = new RelayCommand(DoFollowUnfollowUser);
        }

        private void DoFollowUnfollowUser()
        {
            //todo follow/unfollow user
        }
    }
}

