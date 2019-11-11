using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using ServerToolsIdrac.Redfish.Actions;
using ServerToolsIdrac.Redfish.Enums;
using ServerToolsIdrac.Redfish.Util;
using ServerToolsUI.Model;
using ServerToolsUI.Util;
using ServerToolsUI.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
            OpenServerListCommand = new RelayCommand(OpenServerList);
            BackCommand = new RelayCommand(Back);
            CancelCommand = new RelayCommand(Cancel);
            Servers = new ObservableCollection<string>();
        }

        private bool Validate()
        {
            validationErrors.Clear();

            if (string.IsNullOrEmpty(FirmwarePath))
            {
                List<string> errors = new List<string>()
                {
                    "Informe o Firmware a ser utilizado"
                };
                validationErrors["FirmwarePath"] = errors;               
            }

            if (Servers.Count == 0)
            {
                List<string> errors = new List<string>()
                {
                    "Adicione ao menos 1 servidor para Update"
                };
                validationErrors["Server"] = errors;
            }

            RaiseErrorsChanged("Server");
            RaiseErrorsChanged("FirmwarePath");

            return validationErrors.Count == 0;
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

        private bool cancellationRequested = false;
        public bool CancellationRequested
        {
            get => cancellationRequested;
            set
            {
                if(value != cancellationRequested)
                {
                    cancellationRequested = value;
                    NotifyPropertyChanged("CancellationRequested");
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
        public RelayCommand OpenServerListCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private void Cancel(object parameter)
        {
            CancellationRequested = true;
        }
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

        private void Back(object parameter)
        {
            NavigationUtil.NotifyColleagues("Home", null);
        }

        private async void OpenServerList(object parameter)
        {
            var view = new ServersListView()
            {
                DataContext = new ServersListViewModel()
            };
            try
            {
                var serverList = (List<string>)await DialogHost.Show(view, "MainHost");

                if (!serverList.Any())
                    return;

                HasServers = true;
                NoServerCardVisible = false;

                foreach (string item in serverList)
                {
                    if (!string.IsNullOrEmpty(item))
                        Servers.Add(item);
                }
            }
            catch { } // Dialog return an empty List
        }

        private async void UpdateFirmware(object parameter)
        {
            if (!Validate())
                return;

            var view = new CredentialsView()
            {
                DataContext = new CredentialsViewModel()
            };

            NetworkCredential credentials = (NetworkCredential)await DialogHost.Show(view, "MainHost");

            if (credentials == null)
                return;

            HasJobs = true;
            NoJobCardVisible = false;
            Monitor = new JobMonitor(credentials, JobRefreshTime);
            CancellationRequested = false;

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
                if (CancellationRequested)
                {
                    CancellationRequested = false;
                    break;
                }
            }
            Monitor.Start();
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
