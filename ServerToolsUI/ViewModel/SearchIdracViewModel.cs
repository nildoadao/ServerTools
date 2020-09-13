using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using ServerToolsIdrac.Network;
using ServerToolsIdrac.Racadm.Actions;
using ServerToolsIdrac.Racadm.Model;
using ServerToolsIdrac.Redfish.Actions;
using ServerToolsIdrac.Redfish.Enums;
using ServerToolsIdrac.Redfish.Util;
using ServerToolsUI.Model;
using ServerToolsUI.Util;
using ServerToolsUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.ViewModel
{
    public class SearchIdracViewModel : ViewModelBase
    {
        private const int JobRefreshTime = 5;
        public SearchIdracViewModel()
        {
            Idracs = new ObservableCollection<JobsDataGridInfo>();
            IpsDiscovered = new List<string>();
            Searching = false;
            BackCommand = new RelayCommand(Back);
            SearchIdracsCommand = new RelayCommand(SearchIdracs);
            CancelCommand = new RelayCommand(StopSearch);
            OpenScpFileCommand = new RelayCommand(OpenScpFile);
            OpenFirmwareFileCommand = new RelayCommand(OpenFirmwareFile);
            UpdateFirmwareCommand = new RelayCommand(UpdateFirmware);
            ImportScpFileCommand = new RelayCommand(ImportScpFile);
            ClearJobsCommand = new RelayCommand(ClearJobs);
            OpenScriptFileCommand = new RelayCommand(OpenScriptFile);
            RunScriptCommand = new RelayCommand(RunScript);
        }

        private bool ValidateIpAddress()
        {
            validationErrors.Clear();

            if (!NetworkUtil.ValidateIpAddress(NetworkIp))
            {
                List<string> errors = new List<string>()
                {
                    "IP inválido"
                };
                validationErrors["NetworkIp"] = errors;
            }


            if (!NetworkUtil.ValidateIpAddress(NetworkMask))
            {
                List<string> errors = new List<string>()
                {
                    "Mascára inválida"
                };
                validationErrors["NetworkMask"] = errors;
            }

            RaiseErrorsChanged("NetworkIp");
            RaiseErrorsChanged("NetworkMask");

            return validationErrors.Count == 0;
        }

        private bool ValidateFirmwareUpdate()
        {
            validationErrors.Clear();

            if (string.IsNullOrEmpty(FirmwarePath))
            {
                List<string> errors = new List<string>()
                {
                    "Informe um arquivo SCP"
                };
                validationErrors["FirmwarePath"] = errors;
            }

            if (Idracs.Count == 0)
            {
                List<string> errors = new List<string>()
                {
                    "Adicione ao menos um servidor para o Update"
                };
                validationErrors["Idracs"] = errors;
            }

            RaiseErrorsChanged("Idracs");
            RaiseErrorsChanged("FirmwarePath");

            return validationErrors.Count == 0;
        }

        private bool ValidateScpImport()
        {
            validationErrors.Clear();

            if (string.IsNullOrEmpty(ScpFilePath))
            {
                List<string> errors = new List<string>()
                {
                    "Informe um arquivo SCP"
                };
                validationErrors["ScpFilePath"] = errors;
            }

            if (Idracs.Count == 0)
            {
                List<string> errors = new List<string>()
                {
                    "Adicione ao menos 1 servidor para o import"
                };
                validationErrors["Idracs"] = errors;
            }

            RaiseErrorsChanged("Idracs");
            RaiseErrorsChanged("ScpFilePath");

            return validationErrors.Count == 0;
        }

        private bool ValidateScriptRun()
        {
            validationErrors.Clear();

            if (string.IsNullOrEmpty(ScriptFilePath))
            {
                List<string> errors = new List<string>()
                {
                    "Informe um Script"
                };
                validationErrors["ScriptFilePath"] = errors;
            }

            if (Idracs.Count == 0)
            {
                List<string> errors = new List<string>()
                {
                    "Adicione ao menos 1 servidor"
                };
                validationErrors["Idracs"] = errors;
            }

            RaiseErrorsChanged("Idracs");
            RaiseErrorsChanged("ScriptFilePath");

            return validationErrors.Count == 0;
        }

        private string networkIp;
        public string NetworkIp
        {
            get => networkIp;
            set
            {
                if(value != networkIp)
                {
                    networkIp = value;
                    NotifyPropertyChanged("NetworkIp");
                }
            }
        }

        private string networkMask;
        public string NetworkMask
        {
            get => networkMask;
            set
            {
                if (value != networkMask)
                {
                    networkMask = value;
                    NotifyPropertyChanged("NetworkMask");
                }
            }
        }

        private List<string> ipsDiscovered;
        public List<string> IpsDiscovered
        {
            get => ipsDiscovered;
            set
            {
                if(value != ipsDiscovered)
                {
                    ipsDiscovered = value;
                    NotifyPropertyChanged("IpsDiscovered");
                }
            }
        }

        private ObservableCollection<JobsDataGridInfo> idracs;
        public ObservableCollection<JobsDataGridInfo> Idracs
        {
            get => idracs;
            set
            {
                if (value != idracs)
                {
                    idracs = value;
                    NotifyPropertyChanged("Idracs");
                }
            }
        }

        private bool searching;
        public bool Searching
        {
            get => searching;
            set
            {
                if(value != searching)
                {
                    searching = value;
                    NotifyPropertyChanged("Searching");
                }
            }
        }

        private bool cancelToken;
        public bool CancelToken
        {
            get => cancelToken;
            set
            {
                if (value != cancelToken)
                {
                    cancelToken = value;
                    NotifyPropertyChanged("CancelToken");
                }
            }
        }

        private string scpFilePath;
        public string ScpFilePath
        {
            get => scpFilePath;
            set
            {
                if(value != scpFilePath)
                {
                    scpFilePath = value;
                    NotifyPropertyChanged("ScpFilePath");
                }
            }
        }

        private int selectedTarget;
        public int SelectedTarget
        {
            get => selectedTarget;
            set
            {
                if(value != selectedTarget)
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
                if (value != selectedShutdown)
                {
                    selectedShutdown = value;
                    NotifyPropertyChanged("SelectedShutdown");
                }
            }
        }

        private string firmwarePath;
        public string FirmwarePath
        {
            get => firmwarePath;
            set
            {
                if (value != firmwarePath)
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
                if (value != selectedMode)
                {
                    selectedMode = value;
                    NotifyPropertyChanged("SelectedMode");
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

        private string scriptFilePath;
        public string ScriptFilePath
        {
            get => scriptFilePath;
            set
            {
                if (value != scriptFilePath)
                {
                    scriptFilePath = value;
                    NotifyPropertyChanged("ScriptFilePath");
                }
            }
        }

        public RelayCommand BackCommand { get; private set; }
        public RelayCommand SearchIdracsCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand OpenScpFileCommand { get; private set; }
        public RelayCommand OpenFirmwareFileCommand { get; private set; }
        public RelayCommand UpdateFirmwareCommand { get; private set; }
        public RelayCommand ImportScpFileCommand { get; private set; }
        public RelayCommand ClearJobsCommand { get; private set; }
        public RelayCommand OpenScriptFileCommand { get; private set; }
        public RelayCommand RunScriptCommand { get; private set; }
        private void Back(object parameter)
        {
            NavigationUtil.NotifyColleagues("Home", null);
        }

        private async void SearchIdracs(object parameter)
        {

            if (!ValidateIpAddress())
                return;

            var view = new CredentialsView()
            {
                DataContext = new CredentialsViewModel()
            };

            NetworkCredential credentials = (NetworkCredential)await DialogHost.Show(view, "MainHost");

            if (credentials == null)
                return;

            Searching = true;
            
            try
            {
                string currentIp = NetworkUtil.GetNetworkAddress(NetworkIp, NetworkMask);
                while (NetworkUtil.GetNextIpAddress(currentIp, NetworkMask) != null)
                {
                    currentIp = NetworkUtil.GetNextIpAddress(currentIp, NetworkMask);

                    if (await ConnectionUtil.CheckConnectionAsync(currentIp))
                    {
                        try
                        {
                            var action = new ChassisAction(currentIp, credentials);
                            string serviceTag = await action.GetServiceTagAsync();

                            Idracs.Add(new JobsDataGridInfo()
                            {
                                Server = currentIp,
                                SerialNumber = serviceTag
                            });
                        }
                        catch { }
                    }

                    if (CancelToken)
                    {
                        CancelToken = false;
                        break;
                    }
                }
            }
            catch { }

            Searching = false;
        }

        private void StopSearch(object parameter)
        {
            CancelToken = true;
        }

        private void OpenScpFile(object parameter)
        {
            var folderDialog = new OpenFileDialog()
            {
                Filter = "SCP Files (*.xml)| *.xml"
            };
            folderDialog.FileOk += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                OpenFileDialog dialog = (OpenFileDialog)sender;
                ScpFilePath = dialog.FileName;
            };
            folderDialog.ShowDialog();
        }

        private void OpenFirmwareFile(object parameter)
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

        private async void UpdateFirmware(object parameter)
        {
            if (!ValidateFirmwareUpdate())
                return;

            var view = new CredentialsView()
            {
                DataContext = new CredentialsViewModel()
            };

            NetworkCredential credentials = (NetworkCredential)await DialogHost.Show(view, "MainHost");

            if (credentials == null)
                return;

            Monitor = new JobMonitor(credentials, JobRefreshTime);
            CancelToken = false;

            foreach (var item in Idracs)
            {
                FirmwareAction firmware = new FirmwareAction(item.Server, credentials);
                try
                {
                    string jobUri = await firmware.UpdateFirmwareAsync(FirmwarePath, ((FirmwareUpdateMode)SelectedMode).ToString());
                    Monitor.AddJob(item.Server, jobUri);
                }
                catch (Exception)
                {
                    Monitor.AddJob(item.Server, "");
                }
                if (CancelToken)
                {
                    CancelToken = false;
                    break;
                }
            }
            Monitor.Start();
        }

        private async void ImportScpFile(object parameter)
        {
            if (!ValidateScpImport())
                return;

            var view = new CredentialsView()
            {
                DataContext = new CredentialsViewModel()
            };

            NetworkCredential credentials = (NetworkCredential)await DialogHost.Show(view, "MainHost");

            if (credentials == null)
                return;

            Monitor = new JobMonitor(credentials, JobRefreshTime);

            foreach (var item in Idracs)
            {
                ScpFileAction scp = new ScpFileAction(item.Server, credentials);

                try
                {
                    string target = ((ScpFileContent)SelectedTarget).ToString();
                    string shutdownType = ((ShutdownType)SelectedShutdown).ToString();
                    string jobUri = await scp.ImportScpFileAsync(ScpFilePath, target, shutdownType, "On");
                    Monitor.AddJob(item.Server, jobUri);
                }
                catch (Exception)
                {
                    Monitor.AddJob(item.Server, "");
                }

                Monitor.Start();
            }
        }

        private void ClearJobs(object parameter)
        {
            if(Monitor != null)
            {
                Monitor.Jobs.Clear();
                Monitor.Stop();
            }
        }

        private void OpenScriptFile(object parameter)
        {
            var folderDialog = new OpenFileDialog()
            {
                Filter = "Text Files (*.txt)| *.txt"
            };
            folderDialog.FileOk += (object sender, System.ComponentModel.CancelEventArgs e) =>
            {
                OpenFileDialog dialog = (OpenFileDialog)sender;
                ScriptFilePath = dialog.FileName;
            };
            folderDialog.ShowDialog();
        }

        private async void RunScript(object parameter)
        {
            if (!ValidateScriptRun())
                return;

            var view = new CredentialsView()
            {
                DataContext = new CredentialsViewModel()
            };

            NetworkCredential credentials = (NetworkCredential)await DialogHost.Show(view, "MainHost");

            if (credentials == null)
                return;

            string[] script;

            try
            {
                script = File.ReadAllLines(ScriptFilePath);
            }
            catch (Exception e)
            {
                var userMessage = new UserMessageView()
                {
                    DataContext = new UserMessageViewModel(string.Format("Fail to open the script: {0}", e.Message))
                };
                await DialogHost.Show(userMessage, "MainHost");
                return;
            }

            CancelToken = false;
            Monitor = new JobMonitor(credentials, JobRefreshTime);
            foreach (var item in Idracs)
            {
                JobsDataGridInfo job = new JobsDataGridInfo() { Server = item.Server, JobStatus = "Running" };
                Monitor.Jobs.Add(job);
                SshAction action = new SshAction(item.Server, credentials);
                try
                {
                    SshResponse response = await action.RunScriptAsync(script);
                    job.JobMessage = response.Message;

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

                if (CancelToken)
                {
                    CancelToken = false;
                    break;
                }
            }
        }
    }
}
