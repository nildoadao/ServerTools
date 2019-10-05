using MaterialDesignThemes.Wpf;
using ServerToolsUI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.ViewModel
{
    public class CredentialsViewModel : ViewModelBase
    {

        public CredentialsViewModel()
        {
            OkCommand = new RelayCommand(Ok);
        }
        private string user;
        public string User
        {
            get => user;
            set
            {
                if(value != user)
                {
                    user = value;
                    NotifyPropertyChanged("User");
                }
            }
        }

        private SecureString securePassword;
        public SecureString SecurePassword
        {
            get => securePassword;
            set
            {
                if(value != securePassword)
                {
                    securePassword = value;
                    NotifyPropertyChanged("SecurePassword");
                }
            }
        }

        public RelayCommand OkCommand { get; private set; }

        private void Ok(object parameter)
        {
            if (SecurePassword == null || SecurePassword.Length == 0)
                return;

            NetworkCredential credential = new NetworkCredential(User, SecurePassword);
            DialogHost.CloseDialogCommand.Execute(credential, null);
        }
    }
}
