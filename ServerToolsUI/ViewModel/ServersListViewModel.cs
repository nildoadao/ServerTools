using MaterialDesignThemes.Wpf;
using ServerToolsUI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.ViewModel
{
    public class ServersListViewModel : ViewModelBase
    {
        public ServersListViewModel()
        {
            CloseCommand = new RelayCommand(Close);
            CancelCommand = new RelayCommand(Cancel);
        }

        private string servers;
        public string Servers
        {
            get => servers;
            set
            {
                if(value != servers)
                {
                    servers = value;
                    NotifyPropertyChanged("Servers");
                }
            }
        }
        public RelayCommand CloseCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private void Close(object parameter)
        {
            if(string.IsNullOrEmpty(Servers))
                DialogHost.CloseDialogCommand.Execute(null, null);

            else
            {
                List<string> serverList = new List<string>();

                foreach (string item in Servers.Split('\n'))
                    serverList.Add(item.Trim());

                DialogHost.CloseDialogCommand.Execute(serverList, null);
            }

        }

        private void Cancel(object parameter)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
