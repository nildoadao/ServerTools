using MaterialDesignThemes.Wpf;
using ServerToolsIdrac.Redfish.Actions;
using ServerToolsIdrac.Redfish.Enums;
using ServerToolsUI.Util;
using ServerToolsUI.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.ViewModel
{
    public class ScpExportViewModel : ViewModelBase
    {
        public ScpExportViewModel()
        {
            ExportCommand = new RelayCommand(Export);
            BackCommand = new RelayCommand(Back);
        }

        private bool Validate()
        {
            validationErrors.Clear();

            if (string.IsNullOrEmpty(Host))
            {
                List<string> errors = new List<string>()
                {
                    "Inform an host to export"
                };
                validationErrors["Host"] = errors;
            }

            RaiseErrorsChanged("Host");

            return validationErrors.Count == 0;
        }

        private bool noExportCardVisible = true;
        public bool NoExportCardVisible
        {
            get => noExportCardVisible;
            set
            {
                if(value != noExportCardVisible)
                {
                    noExportCardVisible = value;
                    NotifyPropertyChanged("NoExportCardVisible");
                }
            }
        }

        private bool exportRunning = false;
        public bool ExportRunning
        {
            get => exportRunning;
            set
            {
                if(value != exportRunning)
                {
                    exportRunning = value;
                    NotifyPropertyChanged("ExportRunning");
                }
            }
        }

        private bool exportFail = false;
        public bool ExportFail
        {
            get => exportFail;
            set
            {
                if(value != exportFail)
                {
                    exportFail = value;
                    NotifyPropertyChanged("ExportFail");
                }
            }
        }

        private bool exportFinished = false;
        public bool ExportFinished
        {
            get => exportFinished;
            set
            {
                if(value != exportFinished)
                {
                    exportFinished = value;
                    NotifyPropertyChanged("ExportFinished");
                }
            }
        }

        private string host;
        public string Host
        {
            get => host;
            set
            {
                if(value != host)
                {
                    host = value;
                    NotifyPropertyChanged("Host");
                }
            }
        }

        private int fileContent;
        public int FileContent
        {
            get => fileContent;
            set
            {
                if(value != fileContent)
                {
                    fileContent = value;
                    NotifyPropertyChanged("FileContent");
                }
            }
        }

        private int exportMode;
        public int ExportMode
        {
            get => exportMode;
            set
            {
                if(value != exportMode)
                {
                    exportMode = value;
                    NotifyPropertyChanged("ExportMode");
                }
            }
        }

        private string exportMessage;
        public string ExportMessage
        {
            get => exportMessage;
            set
            {
                if(value != exportMessage)
                {
                    exportMessage = value;
                    NotifyPropertyChanged("ExportMessage");
                }
            }
        }

        public RelayCommand ExportCommand { get; private set; }
        public RelayCommand BackCommand { get; private set; }
        private void Back(object parameter)
        {
            NavigationUtil.NotifyColleagues("Home", null);
        }
        private async void Export(object parameter)
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

            ScpFileAction action = new ScpFileAction(Host, credentials);

            ExportMessage = "";
            ExportFail = false;
            ExportFinished = false;
            ExportRunning = true;
            NoExportCardVisible = false;

            string target = ((ScpFileContent)FileContent).ToString();
            string mode = ((ScpExportMode)ExportMode).ToString();
            try
            {
                string fileData = await action.ExportScpFileAsync(target, mode);
                string downloadsFolder = Syroot.Windows.IO.KnownFolders.Downloads.Path;

                string fileName = string.Format("SCP_{0}{1}{2}.xml", 
                    DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

                File.WriteAllText(Path.Combine(downloadsFolder, fileName), fileData);

                ExportRunning = false;
                ExportFinished = true;
                NoExportCardVisible = true;
                ExportFail = false;
                ExportMessage = string.Format("Export completed, save in {0}",
                    Path.Combine(downloadsFolder, fileName));
            }
            catch(Exception ex)
            {
                ExportRunning = false;
                ExportFinished = true;
                NoExportCardVisible = true;
                ExportFail = true;
                ExportMessage = string.Format("Fail to export: {0}", ex.Message);
            }           
        }
    }
}
