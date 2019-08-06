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

        public RelayCommand SettingsCommand { get; private set; }

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

        private async void Settings(object parameter)
        {
            var view = new SettingsView()
            {
                DataContext = new SettingsViewModel()
            };
            await DialogHost.Show(view);
        }
    }
}
