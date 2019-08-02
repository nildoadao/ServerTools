using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using ServerToolsIdrac.Redfish.Common;
using ServerToolsIdrac.Redfish.Firmware;
using ServerToolsIdrac.Redfish.Job;
using ServerToolsIdrac.Redfish.Util;
using ServerToolsUI.Model;
using ServerToolsUI.Model.Enums;
using ServerToolsUI.Util;
using ServerToolsUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.ViewModel
{
    public class FirmwareUpdateViewModel : ViewModelBase
    {
        private const int JobRefreshTime = 5;
        public FirmwareUpdateViewModel()
        {
            AddServerCommand = new RelayCommand(AddServer);
            UpdateFirmwareCommand = new RelayCommand(UpdateFirmware);
            RemoveServerCommand = new RelayCommand(RemoveServer);
            OpenFolderCommand = new RelayCommand(OpenFolder);
            ClearJobsCommand = new RelayCommand(ClearJobs);
            Servers = new ObservableCollection<string>();
        }

        private string firmwarePath;
        public string FirmwarePath
        {
            get => firmwarePath;
            set
            {
                if(value != firmwarePath)
                {
                    firmwarePath = value;
                    NotifyPropertyChanged("FirmwarePath");
                }
            }
        }

        private int selectedMode;
        public int SelectedMode
        {
            get => selectedMode;
            set
            {
                if(value != selectedMode)
                {
                    selectedMode = value;
                    NotifyPropertyChanged("SelectedMode");
                }
            }
        }

        private ObservableCollection<string> servers;
        public ObservableCollection<string> Servers
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

        private bool hasServers = false;
        public bool HasServers
        {
            get => hasServers;
            set
            {
                if(value != hasServers)
                {
                    hasServers = value;
                    NotifyPropertyChanged("HasServers");
                }
            }
        }

        private bool hasJobs = false;
        public bool HasJobs
        {
            get => hasJobs;
            set
            {
                if(value != hasJobs)
                {
                    hasJobs = value;
                    NotifyPropertyChanged("HasJobs");
                }
            }
        }

        private string server;
        public string Server
        {
            get => server;
            set
            {
                if(value != server)
                {
                    server = value;
                    NotifyPropertyChanged("Server");
                }
            }
        }

        private bool noServerCardVisible = true;
        public bool NoServerCardVisible
        {
            get => noServerCardVisible;
            set
            {
                if(value != noServerCardVisible)
                {
                    noServerCardVisible = value;
                    NotifyPropertyChanged("NoServerCardVisible");
                }
            }
        }

        private bool noJobCardVisible = true;
        public bool NoJobCardVisible
        {
            get => noJobCardVisible;
            set
            {
                if (value != noJobCardVisible)
                {
                    noJobCardVisible = value;
                    NotifyPropertyChanged("NoJobCardVisible");
                }
            }
        }

        private JobMonitor monitor;
        public JobMonitor Monitor
        {
            get => monitor;
            set
            {
                if(value != monitor)
                {
                    monitor = value;
                    NotifyPropertyChanged("Monitor");
                }
            }
        }
        public RelayCommand AddServerCommand { get; private set; }
        public RelayCommand UpdateFirmwareCommand { get; private set; }
        public RelayCommand RemoveServerCommand { get; private set; }
        public RelayCommand OpenFolderCommand { get; private set; }
        public RelayCommand ClearJobsCommand { get; private set; }
        private void AddServer(object parameter)
        {
            if (!string.IsNullOrEmpty(Server))
            {
                Servers.Add(Server);
                HasServers = true;
                NoServerCardVisible = false;
            }
            Server = string.Empty;
        }

        private async void UpdateFirmware(object parameter)
        {
            var view = new CredentialsView()
            {
                DataContext = new CredentialsViewModel()
            };

            NetworkCredential credentials = (NetworkCredential)await DialogHost.Show(view, "MainHost");
            HasJobs = true;
            NoJobCardVisible = false;
            Monitor = new JobMonitor(credentials, JobRefreshTime);

            foreach (string server in Servers)
            {
                FirmwareAction firmware = new FirmwareAction(server, credentials);

                try
                {
                    string jobUri = await firmware.UpdateFirmwareAsync(FirmwarePath, ((FirmwareUpdateMode)SelectedMode).ToString());
                    Monitor.AddJob(server, jobUri);
                }
                catch (Exception)
                {
                    Monitor.AddJob(server, "");
                }

                Monitor.Start();
            }
            HasJobs = Monitor.Jobs.Any();
            NoJobCardVisible = !HasJobs;
        }

        private void RemoveServer(object parameter)
        {
            if (!HasJobs)
            {
                Servers.Remove((string)parameter);
                HasServers = servers.Any();
                NoServerCardVisible = !HasServers;
            }
        }

        private void OpenFolder(object parameter)
        {
            var folderDialog = new OpenFileDialog()
            {
                Filter = "Idrac Firmware (*.exe)(*.d7)(*.pm)| *.exe;*.d7;*.pm"                
            };
            folderDialog.FileOk += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                OpenFileDialog dialog = (OpenFileDialog)sender;
                FirmwarePath = dialog.FileName;
            };
            folderDialog.ShowDialog();
        }

        private void ClearJobs(object parameter)
        {
            Monitor.Jobs.Clear();
            Monitor.Stop();
            HasJobs = false;
            NoJobCardVisible = true;
        }
    }
}
