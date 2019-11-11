using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using ServerToolsIdrac.Redfish.Actions;
using ServerToolsIdrac.Redfish.Enums;
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
    public class ScpImportViewModel : ViewModelBase
    {
        private const int JobRefreshTime = 5;

        public ScpImportViewModel()
        {
            AddServerCommand = new RelayCommand(AddServer);
            RemoveServerCommand = new RelayCommand(RemoveServer);
            OpenFolderCommand = new RelayCommand(OpenFolder);
            ClearJobsCommand = new RelayCommand(ClearJobs);
            ImportFileCommand = new RelayCommand(ImportFile);
            OpenServerListCommand = new RelayCommand(OpenServerList);
            BackCommand = new RelayCommand(Back);
            Servers = new ObservableCollection<string>();
        }

        private bool Validate()
        {
            validationErrors.Clear();

            if (string.IsNullOrEmpty(FilePath))
            {
                List<string> errors = new List<string>()
                {
                    "Informe o Arquivo SCP a ser utilizado"
                };
                validationErrors["FilePath"] = errors;
            }

            if (Servers.Count == 0)
            {
                List<string> errors = new List<string>()
                {
                    "Adicione ao menos 1 servidor para o Import"
                };
                validationErrors["Server"] = errors;
            }

            RaiseErrorsChanged("Server");
            RaiseErrorsChanged("FilePath");

            return validationErrors.Count == 0;
        }

        private string filePath;
        public string FilePath
        {
            get => filePath;
            set
            {
                if (value != filePath)
                {
                    filePath = value;
                    NotifyPropertyChanged("FiLePath");
                }
            }
        }

        private int selectedTarget;
        public int SelectedTarget
        {
            get => selectedTarget;
            set
            {
                if (value != selectedTarget)
                {
                    selectedTarget = value;
                    NotifyPropertyChanged("SelectedTarget");
                }
            }
        }

        private int selectedShutdown;
        public int SelectedShutdown
        {
            get => selectedShutdown;
            set
            {
                if(value != selectedShutdown)
                {
                    selectedShutdown = value;
                    NotifyPropertyChanged("SelectedShutdown");
                }
            }
        }

        private ObservableCollection<string> servers;
        public ObservableCollection<string> Servers
        {
            get => servers;
            set
            {
                if (value != servers)
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
                if (value != hasServers)
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
                if (value != hasJobs)
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
                if (value != server)
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
                if (value != noServerCardVisible)
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
                if (value != monitor)
                {
                    monitor = value;
                    NotifyPropertyChanged("Monitor");
                }
            }
        }

        public RelayCommand AddServerCommand { get; private set; }
        public RelayCommand RemoveServerCommand { get; private set; }
        public RelayCommand OpenFolderCommand { get; private set; }
        public RelayCommand ClearJobsCommand { get; private set; }
        public RelayCommand ImportFileCommand { get; private set; }
        public RelayCommand OpenServerListCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }
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

        private void RemoveServer(object parameter)
        {
            if (!HasJobs)
            {
                Servers.Remove((string)parameter);
                HasServers = servers.Any();
                NoServerCardVisible = !HasServers;
            }
        }

        private void Back(object parameter)
        {
            NavigationUtil.NotifyColleagues("Home", null);
        }

        private void OpenFolder(object parameter)
        {
            var folderDialog = new OpenFileDialog()
            {
                Filter = "SCP Files (*.xml)| *.xml"
            };
            folderDialog.FileOk += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                OpenFileDialog dialog = (OpenFileDialog)sender;
                FilePath = dialog.FileName;
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

        private async void ImportFile(object parameter)
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

            foreach (string server in Servers)
            {
                ScpFileAction scp = new ScpFileAction(server, credentials);

                try
                {
                    string target = ((ScpFileContent)SelectedTarget).ToString();
                    string shutdownType = ((ShutdownType)SelectedShutdown).ToString();
                    string jobUri = await scp.ImportScpFileAsync(FilePath, target, shutdownType, "On");
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
    }
}
