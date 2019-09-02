using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using ServerToolsIdrac.Racadm.Actions;
using ServerToolsIdrac.Racadm.Model;
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
    public class CustomScriptViewModel : ViewModelBase
    {

        public CustomScriptViewModel()
        {
            Servers = new ObservableCollection<string>();
            Jobs = new ObservableCollection<JobsDataGridInfo>();
            AddServerCommand = new RelayCommand(AddServer);
            RemoveServerCommand = new RelayCommand(RemoveServer);
            BackCommand = new RelayCommand(Back);
            OpenFolderCommand = new RelayCommand(OpenFolder);
            OpenServerListCommand = new RelayCommand(OpenServerList);
            RunScriptCommand = new RelayCommand(RunScript);
            ClearJobsCommand = new RelayCommand(ClearJobs);
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

        private ObservableCollection<JobsDataGridInfo> jobs;
        public ObservableCollection<JobsDataGridInfo> Jobs
        {
            get => jobs;
            set
            {
                if(value != jobs)
                {
                    jobs = value;
                    NotifyPropertyChanged("Jobs");
                }
            }
        }


        private string filePath;
        public string FilePath
        {
            get => filePath;
            set
            {
                if(value != filePath)
                {
                    filePath = value;
                    NotifyPropertyChanged("FilePath");
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
                if(value != hasJobs)
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
                if(value != noJobCardVisible)
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
                if(value != server)
                {
                    server = value;
                    NotifyPropertyChanged("Server");
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

        private bool Validate()
        {
            validationErrors.Clear();

            if (string.IsNullOrEmpty(FilePath))
            {
                List<string> errors = new List<string>()
                {
                    "Informe o Script a ser utilizado"
                };
                validationErrors["FilePath"] = errors;
            }

            if (Servers.Count == 0)
            {
                List<string> errors = new List<string>()
                {
                    "Adicione ao menos 1 servidor para a execução do Script"
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

        private void OpenFolder(object parameter)
        {
            var folderDialog = new OpenFileDialog()
            {
                Filter = "Arquivos de texto (*.txt)| *.txt"
            };
            folderDialog.FileOk += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                OpenFileDialog dialog = (OpenFileDialog)sender;
                FilePath = dialog.FileName;
            };
            folderDialog.ShowDialog();
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

        private async void RunScript(object parameter)
        {
            if (!Validate())
                return;

            var view = new CredentialsView()
            {
                DataContext = new CredentialsViewModel()
            };

            NetworkCredential credentials = (NetworkCredential)await DialogHost.Show(view, "MainHost");
            HasJobs = true;
            string[] script;

            try
            {
                script = File.ReadAllLines(FilePath);
            }
            catch(Exception e)
            {
                var userMessage = new UserMessageView()
                {
                    DataContext = new UserMessageViewModel(string.Format("Falha ao abrir script: {0}", e.Message))
                };
                await DialogHost.Show(userMessage, "MainHost");
                return;
            }

            foreach(var item in Servers)
            {
                JobsDataGridInfo job = new JobsDataGridInfo() { Server = item, JobStatus = "Running" };
                Jobs.Add(job);
                SshAction action = new SshAction(item, credentials);
                try
                {
                    SshResponse response = await action.RunScriptAsync(script);
                    job.JobMessage = response.Message;

                    if (response.StatusCode != 0)
                        job.JobStatus = "Failed";
                    else
                        job.JobStatus = "Success";
                }
                catch(Exception e)
                {
                    job.JobMessage = e.Message;
                    job.JobStatus = "Failed";
                }
            }
        }
    }
}
