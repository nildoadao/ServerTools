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

            jobs = new ObservableCollection<JobsDataGridInfo>();
            servers = new ObservableCollection<ConnectionInfo>();
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

        private ObservableCollection<ConnectionInfo> servers;
        public ObservableCollection<ConnectionInfo> Servers
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

        public RelayCommand AddServerCommand { get; private set; }
        public RelayCommand UpdateFirmwareCommand { get; private set; }
        public RelayCommand RemoveServerCommand { get; private set; }
        public RelayCommand OpenFolderCommand { get; private set; }
        private async void AddServer(object parameter)
        {
            var view = new AddServerView
            {
                DataContext = new AddServerViewModel()
            };
            try
            {
                ConnectionInfo result = (ConnectionInfo)await DialogHost.Show(view);
                servers.Add(result);
            }
            catch { } // Cancel hit
        }

        private async void UpdateFirmware(object parameter)
        {
            foreach (ConnectionInfo connection in Servers)
            {
                JobsDataGridInfo job = new JobsDataGridInfo()
                {
                    Server = connection.Host,
                    JobStatus = "Requested",
                };

                Jobs.Add(job);
                FirmwareAction firmware = new FirmwareAction(connection);

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
            servers.Remove((ConnectionInfo)parameter);
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
    }
}
