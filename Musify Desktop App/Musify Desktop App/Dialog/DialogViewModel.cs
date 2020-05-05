using System;
using System.Collections.Generic;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Musify_Desktop_App.Dialog
{
    class DialogViewModel : ViewModelBase
    {
        public string DialogTitle { get; }
        public string DialogText { get; }

        public RelayCommand OkCommand { get; }
        public RelayCommand NoCommand { get; }
        public RelayCommand CancelCommand { get; }

        public MessageBoxResult Result;

        public DialogViewModel()
        {
        }

        public DialogViewModel(string dialogTitle, string dialogText)
        {
            DialogTitle = dialogTitle;
            DialogText = dialogText;

            OkCommand = new RelayCommand(() =>
            {
                Result = MessageBoxResult.OK;
                CloseWindow();
            });
            NoCommand = new RelayCommand(() =>
            {
                Result = MessageBoxResult.No;
                CloseWindow();
            });
            CancelCommand = new RelayCommand(() => Result = MessageBoxResult.Cancel);
        }

        private void CloseWindow()
        {
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }
    }
}
