using MaterialDesignThemes.Wpf;
using ServerToolsIdrac.Redfish.Common;
using ServerToolsUI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.ViewModel
{
    public class AddServerViewModel : ViewModelBase
    {
        public AddServerViewModel()
        {
            AddServerCommand = new RelayCommand(AddServer);
            server = new ConnectionInfo
            {
                Host = host,
                User = user,
                Password = pass
            };
        }

        private string host;
        public string Host
        {
            get => host;
            set
            {
                if(value != host)
                {
                    host = value;
                    NotifyPropertyChanged("Host");
                }
            }
        }

        private string user;
        public string User
        {
            get => user;
            set
            {
                if (value != user)
                {
                    user = value;
                    NotifyPropertyChanged("User");
                }
            }
        }

        private string pass;
        public string Pass
        {
            get => pass;
            set
            {
                if (value != pass)
                {
                    pass = value;
                    NotifyPropertyChanged("Pass");
                }
            }
        }

        private ConnectionInfo server;
        public ConnectionInfo Server
        {
            get => server;
            private set
            {
                if(value != server)
                {
                    server = value;
                    NotifyPropertyChanged("Server");
                }
            }
        }

        public RelayCommand AddServerCommand { get; private set; }

        private void AddServer(object parameter)
        {
            server = new ConnectionInfo
            {
                Host = host,
                User = user,
                Password = pass
            };

            DialogHost.CloseDialogCommand.Execute(server, null);
        }
    }
}
