using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using ServerToolsIdrac.Redfish.Common;
using ServerToolsIdrac.Redfish.Firmware;
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
        public FirmwareUpdateViewModel()
        {
            AddServerCommand = new RelayCommand(AddServer);
            UpdateFirmwareCommand = new RelayCommand(UpdateFirmware);
            RemoveServerCommand = new RelayCommand(RemoveServer);
            OpenFolderCommand = new RelayCommand(OpenFolder);
            ClearJobsCommand = new RelayCommand(ClearJobs);

            jobs = new ObservableCollection<JobsDataGridInfo>();
            servers = new ObservableCollection<string>();
        }

        private ObservableCollection<JobsDataGridInfo> jobs;
        public ObservableCollection<JobsDataGridInfo> Jobs
        {
            get =>  jobs;
            set
            {
                if (value != jobs)
                {
                    jobs = value;
                    NotifyPropertyChanged("Jobs");
                }
            }
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

            foreach (string server in Servers)
            {
                JobsDataGridInfo job = new JobsDataGridInfo()
                {
                    Server = server,
                    JobStatus = "Requested",
                };

                Jobs.Add(job);
                HasJobs = true;
                NoJobCardVisible = false;
                FirmwareAction firmware = new FirmwareAction(server, credentials);

                try
                {
                    job.JobId = await firmware.UpdateFirmwareAsync(FirmwarePath, ((FirmwareUpdateMode)SelectedMode).ToString());
                }
                catch (RedfishException rex)
                {
                    job.JobMessage = string.Format("Erro na requisição Redfish: {0}", rex.Message);
                }
                catch (Exception ex)
                {
                    job.JobMessage = string.Format("Erro na requisição: {0}", ex.Message);
                }
            }
        }

        private void RemoveServer(object parameter)
        {
            Servers.Remove((string)parameter);
            HasServers = servers.Any();
            NoServerCardVisible = !HasServers;
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
            Jobs.Clear();
            HasJobs = false;
            NoJobCardVisible = true;
        }
    }
}
