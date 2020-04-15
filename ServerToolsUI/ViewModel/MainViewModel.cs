using MaterialDesignThemes.Wpf;
using ServerToolsUI.Util;
using ServerToolsUI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            CurrentView = new HomeViewModel();
            NavigationUtil.Register("Home", GoToHome);
            NavigationUtil.Register("FirmwareUpdate", GoToUpdateFirmware);
            NavigationUtil.Register("ScpExport", GoToScpExport);
            NavigationUtil.Register("ScpImport", GoToScpImport);
            NavigationUtil.Register("CustomScript", GoToCustomScript);
            NavigationUtil.Register("GetProcessorAndCore", GoToGetProcessorAndCore);
            NavigationUtil.Register("GetProcessorInternet", GoToGetProcessorInternet);
            SettingsCommand = new RelayCommand(Settings);
        }

        private object currentView;
        public object CurrentView
        {
            get => currentView;
            set
            {
                if (value != currentView)
                {
                    currentView = value;
                    NotifyPropertyChanged("CurrentView");
                }
            }
        }

        private bool rightDrawerVisible = false;
        public bool RightDrawerVisible
        {
            get => rightDrawerVisible;
            set
            {
                if(value != rightDrawerVisible)
                {
                    rightDrawerVisible = value;
                    NotifyPropertyChanged("RightDrawerVisible");
                }
            }
        }

        private object rightDrawerContent;
        public object RightDrawerContent
        {
            get => rightDrawerContent;
            set
            {
                if(value != rightDrawerContent)
                {
                    rightDrawerContent = value;
                    NotifyPropertyChanged("RightDrawerContent");
                }
            }
        }

        public RelayCommand SettingsCommand { get; private set; }

        private void GoToCustomScript(object parameter)
        {
            CurrentView = new CustomScriptViewModel();
        }

        private void GoToHome(object parameter)
        {
            CurrentView = new HomeViewModel();
        }

        private void GoToUpdateFirmware(object parameter)
        {
            CurrentView = new FirmwareUpdateViewModel();
        }

        private void GoToScpExport(object parameter)
        {
            CurrentView = new ScpExportViewModel();
        }

        private void GoToScpImport(object parameter)
        {
            CurrentView = new ScpImportViewModel();
        }

        private void GoToGetProcessorAndCore(object parameter)
        {
            CurrentView = new GetProcessorAndCoreViewModel();
        }

        private void GoToGetProcessorInternet(object parameter)
        {
            CurrentView = new GetProcessorInternetViewModel();
        }

        private void Settings(object parameter)
        {
            RightDrawerContent = new SettingsView()
            {
                DataContext = new SettingsViewModel()
            };
            RightDrawerVisible = true;
        }
    }
}
