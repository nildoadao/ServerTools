using MaterialDesignThemes.Wpf;
using ServerToolsIdrac.Racadm.Actions;
using ServerToolsIdrac.Racadm.Model;
using ServerToolsUI.Model;
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
    class GetProcessorAndCoreViewModel : ViewModelBase
    {
        public GetProcessorAndCoreViewModel()
        {
            Servers = new ObservableCollection<string>();
            Jobs = new ObservableCollection<JobsDataGridInfo>();
            AddServerCommand = new RelayCommand(AddServer);
            RemoveServerCommand = new RelayCommand(RemoveServer);
            BackCommand = new RelayCommand(Back);
            OpenServerListCommand = new RelayCommand(OpenServerList);
            RunScriptCommand = new RelayCommand(RunScript);
            ClearJobsCommand = new RelayCommand(ClearJobs);
            CancelCommand = new RelayCommand(Cancel);
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

        private int selectedManufacturer;

        public int SelectedManufacturer
        {
            get => selectedManufacturer;
            set
            {
                if(value != selectedManufacturer)
                {
                    selectedManufacturer = value;
                    NotifyPropertyChanged("SelectedManufacturer");
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
                    NoServerCardVisible = !hasServers;
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
                    NoJobCardVisible = !hasJobs;
                    NotifyPropertyChanged("HasJobs");
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
        private bool cancellationRequested;
        public bool CancellationRequested
        {
            get => cancellationRequested;
            set
            {
                if (value != cancellationRequested)
                {
                    cancellationRequested = value;
                    NotifyPropertyChanged("CancellationRequested");
                }
            }
        }

        public RelayCommand AddServerCommand { get; private set; }
        public RelayCommand RemoveServerCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }
        public RelayCommand OpenFolderCommand { get; private set; }
        public RelayCommand OpenServerListCommand { get; private set; }
        public RelayCommand RunScriptCommand { get; private set; }
        public RelayCommand ClearJobsCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private void Cancel(object parameter)
        {
            CancellationRequested = true;
        }

        private bool Validate()
        {
            validationErrors.Clear();

            if (Servers.Count == 0)
            {
                List<string> errors = new List<string>()
                {
                    "Add at least 1 server to run the script"
                };
                validationErrors["Server"] = errors;
            }

            RaiseErrorsChanged("Server");
            RaiseErrorsChanged("FilePath");

            return validationErrors.Count == 0;
        }

        private void AddServer(object paramenter)
        {
            if (!string.IsNullOrEmpty(Server))
            {
                Servers.Add(Server);
                HasServers = true;
            }
            Server = string.Empty;
        }

        private void ClearJobs(object parameter)
        {
            Jobs.Clear();
            HasJobs = false;
        }

        private void RemoveServer(object parameter)
        {
            if (!HasJobs)
            {
                Servers.Remove((string)parameter);
                HasServers = servers.Any();
            }
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

                foreach (string item in serverList)
                {
                    if (!string.IsNullOrEmpty(item))
                        Servers.Add(item);
                }
            }
            catch { } // Dialog return an empty List
        }

        private string GetDellProcessor(string sysInfo)
        {
            string processorName = "Não encontrado";
            var lines = sysInfo.Split('\n');
            bool cpuBlock = false;

            foreach(var line in lines)
            {
                if (line.Contains("InstanceID: CPU.Socket"))
                    cpuBlock = true;
                if(line.Contains("Model") && cpuBlock)
                {
                    processorName = line.Split('=')[1].Trim();
                    break;
                }
            }
            return processorName;
        }

        private string GetDellCoreNumber(string sysInfo)
        {
            string coreNumber = "Não encontrado";
            var lines = sysInfo.Split('\n');
            bool cpuBlock = false;

            foreach (var line in lines)
            {
                if (line.Contains("InstanceID: CPU.Socket"))
                    cpuBlock = true;

                if (line.Contains("NumberOfProcessorCores") && cpuBlock)
                {
                    coreNumber = line.Split('=')[1].Trim();
                    break;
                }
            }
            return coreNumber;
        }

        private string GetHpProcessor(string sysInfo)
        {
            string processorName = "Não encontrado";
            var lines = sysInfo.Split('\n');

            foreach(var line in lines)
            {
                if (line.Contains("name"))
                {
                    processorName = line.Split('\n')[1].Trim();
                    break;
                }
            }
            return processorName;
        }

        private string GetHpCoreNumber(string sysInfo)
        {
            string coreNumber = "Não encontrado";
            var lines = sysInfo.Split('\n');

            foreach (var line in lines)
            {
                if (line.Contains("number_cores"))
                {
                    coreNumber = line.Split('\n')[1].Trim();
                    break;
                }
            }
            return coreNumber;
        }

        private async void RunScript(object parameter)
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

            string command;

            if (SelectedManufacturer == 1)
                command = "racadm hwinventory";
            else
                command = "show /system1/cpu1";

            HasJobs = true;
            CancellationRequested = false;

            foreach (var item in Servers)
            {
                JobsDataGridInfo job = new JobsDataGridInfo() { Server = item, JobStatus = "Running" };
                Jobs.Add(job);
                SshAction action = new SshAction(item, credentials);
                try
                {
                    SshResponse response = await action.RunCommandAsync(command);

                    if (SelectedManufacturer == 1)
                        job.JobMessage = string.Format("Processador: {0}, Cores: {1}",
                            GetDellProcessor(response.Message), GetDellCoreNumber(response.Message));
                    else
                        job.JobMessage = string.Format("Processador: {0}, Cores: {1}",
                                GetHpProcessor(response.Message), GetHpCoreNumber(response.Message));

                    if (response.StatusCode != 0)
                        job.JobStatus = "Failed";
                    else
                        job.JobStatus = "Success";
                }
                catch (Exception e)
                {
                    job.JobMessage = e.Message;
                    job.JobStatus = "Failed";
                }

                if (CancellationRequested)
                {
                    CancellationRequested = false;
                    break;
                }
            }
        }
    }
}

