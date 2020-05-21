using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Command;
using Musify_Desktop_App.Model;
using Musify_Desktop_App.Service;

namespace Musify_Desktop_App.Panels.UserList
{
    internal class UserListViewModel : BasePanelNavigation
    {
        private readonly IUserService _userService;

        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                RaisePropertyChanged(nameof(Users));
            }
        }
        
        public User SelectedUser { get; set; }

        public string ListName { get; set; }

        public RelayCommand GoToProfilePageCommand { get; set; }


        public UserListViewModel()
        {
            GoToProfilePageCommand = new RelayCommand(OnProfilePageRequested);
        }

        public UserListViewModel(IUserService userService, List<User> userList,
            string listName) : this()
        {
            _userService = userService;

            Users = new ObservableCollection<User>(userList);
            ListName = listName;
        }

        private void OnProfilePageRequested()
        {
            OnProfilePageRequested(SelectedUser);
        }
    }
}
