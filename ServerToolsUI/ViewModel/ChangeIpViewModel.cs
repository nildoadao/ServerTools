using MaterialDesignThemes.Wpf;
using ServerToolsIdrac.Network;
using ServerToolsIdrac.Racadm.Actions;
using ServerToolsIdrac.Racadm.Model;
using ServerToolsIdrac.Redfish.Actions;
using ServerToolsIdrac.Redfish.Models;
using ServerToolsIdrac.Redfish.Util;
using ServerToolsUI.Model;
using ServerToolsUI.Util;
using ServerToolsUI.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.ViewModel
{
    public class ChangeIpViewModel : ViewModelBase
    {
        public class IdracInfo
        {
            public string Ip { get; set; }
            public string SerialNumber { get; set; }
            public string NewIp { get; set; }
            public string NewMask { get; set; }
            public string NewGateway { get; set; }
        }

        private const int JobRefreshTime = 5;
        public ChangeIpViewModel()
        {
            Jobs = new ObservableCollection<JobsDataGridInfo>();
            Idracs = new ObservableCollection<IdracInfo>();
            IpsDiscovered = new List<string>();
            Searching = false;
            BackCommand = new RelayCommand(Back);
            SearchIdracsCommand = new RelayCommand(SearchIdracs);
            CancelCommand = new RelayCommand(StopSearch);
            ClearJobsCommand = new RelayCommand(ClearJobs);
            ChangeIpCommand = new RelayCommand(ChangeIp);
            CancelExecutionCommand = new RelayCommand(CancelExecution);
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

        private bool ValidateNewIpAdrreses()
        {
            validationErrors.Clear();

            if (Idracs.Count == 0)
            {
                List<string> errors = new List<string>()
                {
                    "Adicione ao menos 1 Idrac"
                };
                validationErrors["Idracs"] = errors;
            }

            foreach (var idrac in Idracs)
            {
                if (!NetworkUtil.ValidateIpAddress(idrac.NewIp))
                {
                    List<string> errors = new List<string>()
                    {
                        "IP inválido"
                    };
                    validationErrors["Idracs"] = errors;
                }
                if (!NetworkUtil.ValidateIpAddress(idrac.NewMask))
                {
                    List<string> errors = new List<string>()
                    {
                        "Mascara inválida"
                    };
                    validationErrors["Idracs"] = errors;
                }
                if (!NetworkUtil.ValidateIpAddress(idrac.NewGateway))
                {
                    List<string> errors = new List<string>()
                    {
                        "Gateway inválido"
                    };
                    validationErrors["Idracs"] = errors;
                }
            }

            RaiseErrorsChanged("Idracs");

            return validationErrors.Count == 0;
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

        private string networkIp;
        public string NetworkIp
        {
            get => networkIp;
            set
            {
                if (value != networkIp)
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
                if (value != ipsDiscovered)
                {
                    ipsDiscovered = value;
                    NotifyPropertyChanged("IpsDiscovered");
                }
            }
        }

        private ObservableCollection<IdracInfo> idracs;
        public ObservableCollection<IdracInfo> Idracs
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
                if (value != searching)
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

        public RelayCommand BackCommand { get; private set; }
        public RelayCommand SearchIdracsCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand CancelExecutionCommand { get; private set; }
        public RelayCommand ClearJobsCommand { get; private set; }
        public RelayCommand ChangeIpCommand { get; private set; }

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

                            Idracs.Add(new IdracInfo()
                            {
                                Ip = currentIp,
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

        private void CancelExecution(object parameter)
        {
            CancellationRequested = true;
        }

        private void ClearJobs(object parameter)
        {
            Jobs.Clear();
        }

        private async void ChangeIp(object parameter)
        {
            if (!ValidateNewIpAdrreses())
                return;

            var view = new CredentialsView()
            {
                DataContext = new CredentialsViewModel()
            };

            NetworkCredential credentials = (NetworkCredential)await DialogHost.Show(view, "MainHost");

            if (credentials == null)
                return;

            foreach (var idrac in Idracs)
            {
                JobsDataGridInfo job = new JobsDataGridInfo() { Server = idrac.SerialNumber, JobStatus = "Running" };
                Jobs.Add(job);
                SshAction action = new SshAction(idrac.Ip, credentials);
                try
                {
                    SshResponse response = await action.RunCommandAsync(string.Format("racadm setniccfg -s {0} {1} {2}",
                    idrac.NewIp, idrac.NewMask, idrac.NewGateway));
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

                if (CancellationRequested)
                {
                    CancellationRequested = false;
                    break;
                }
            }
        }

    }
}
