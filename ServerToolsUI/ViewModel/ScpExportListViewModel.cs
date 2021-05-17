using MaterialDesignThemes.Wpf;
using ServerToolsIdrac.Redfish.Actions;
using ServerToolsIdrac.Redfish.Enums;
using ServerToolsUI.Model;
using ServerToolsUI.Util;
using ServerToolsUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.ViewModel
{
    class ScpExportListViewModel : ViewModelBase
    {
        private const int JobRefreshTime = 5;

        public ScpExportListViewModel()
        {
            AddServerCommand = new RelayCommand(AddServer);
            RemoveServerCommand = new RelayCommand(RemoveServer);
            ClearJobsCommand = new RelayCommand(ClearJobs);
            ExportFileCommand = new RelayCommand(ExportFile);
            OpenServerListCommand = new RelayCommand(OpenServerList);
            BackCommand = new RelayCommand(Back);
            Servers = new ObservableCollection<string>();
            Jobs = new ObservableCollection<JobsDataGridInfo>();
        }

        private bool Validate()
        {
            validationErrors.Clear();

            if (Servers.Count == 0)
            {
                List<string> errors = new List<string>()
                {
                    "Add at least 1 server to perform an import"
                };
                validationErrors["Server"] = errors;
            }

            RaiseErrorsChanged("Server");

            return validationErrors.Count == 0;
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

        private int exportMode;

        public int ExportMode
        {
            get => exportMode;
            set
            {
                if (value != exportMode)
                {
                    exportMode = value;
                    NotifyPropertyChanged("ExportMode");
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

        private ObservableCollection<JobsDataGridInfo> jobs;
        public ObservableCollection<JobsDataGridInfo> Jobs
        {
            get => jobs;
            set
            {
                if (value != jobs)
                {
                    jobs = value;
                    NotifyPropertyChanged("Jobs");
                }
            }
        }

        public RelayCommand AddServerCommand { get; private set; }
        public RelayCommand RemoveServerCommand { get; private set; }
        public RelayCommand OpenFolderCommand { get; private set; }
        public RelayCommand ClearJobsCommand { get; private set; }
        public RelayCommand ExportFileCommand { get; private set; }
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

        private void ClearJobs(object parameter)
        {
            Jobs.Clear();
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

        private async void ExportFile(object parameter)
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

            foreach (string server in Servers)
            {
                ScpFileAction action = new ScpFileAction(server, credentials);
                string target = ((ScpFileContent)SelectedTarget).ToString();
                string mode = ((ScpExportMode)ExportMode).ToString();
                JobsDataGridInfo job = new JobsDataGridInfo() { Server = server, JobStatus = "Running" };
                Jobs.Add(job);
                try
                {

                    string fileData = await action.ExportScpFileAsync(target, mode);
                    string downloadsFolder = Syroot.Windows.IO.KnownFolders.Downloads.Path;

                    string fileName = string.Format("SCP_{0}{1}{2}{3}.xml", server,
                        DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                    File.WriteAllText(Path.Combine(downloadsFolder, fileName), fileData);
                    job.JobStatus = "Success";
                    job.JobMessage = string.Format("Save in {0}", Path.Combine(downloadsFolder, fileName));
                }
                catch (Exception ex)
                {
                    job.JobStatus = "Failed";
                    job.JobMessage = ex.Message;
                }
            }
            HasJobs = Jobs.Any();
            NoJobCardVisible = !HasJobs;
        }
    }
}
