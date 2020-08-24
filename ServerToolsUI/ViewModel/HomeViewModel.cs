using MaterialDesignThemes.Wpf;
using ServerToolsUI.Util;
using ServerToolsUI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace ServerToolsUI.ViewModel
{
    public class HomeViewModel : ViewModelBase
    {
        public HomeViewModel()
        {
            ScpExportPageCommand = new RelayCommand(ScpExportPage);
            ScpImportPageCommand = new RelayCommand(ScpImportPage);
            FirmwareUpdatePageCommand = new RelayCommand(FirmwareUpdatePage);
            CustomScriptPageCommand = new RelayCommand(CustomScriptPage);
            GetProcessorAndCoreCommand = new RelayCommand(GetProcessorAndCorePage);
            GetProcessorInternetCommand = new RelayCommand(GetProcessorInternetPage);
            SearchIdracPageCommand = new RelayCommand(SearchIdracPage);
        }
        public RelayCommand ScpImportPageCommand { get; private set; }
        public RelayCommand ScpExportPageCommand { get; private set; }
        public RelayCommand FirmwareUpdatePageCommand { get; private set; }
        public RelayCommand CustomScriptPageCommand { get; private set; }
        public RelayCommand GetProcessorAndCoreCommand { get; private set; }
        public RelayCommand GetProcessorInternetCommand { get; private set; }
        public RelayCommand SearchIdracPageCommand { get; private set; }

        private void ScpImportPage(object parameter)
        {
            NavigationUtil.NotifyColleagues("ScpImport", null);
        }

        private void ScpExportPage(object parameter)
        {
            NavigationUtil.NotifyColleagues("ScpExport", null);
        }

        private void FirmwareUpdatePage(object parameter)
        {
            NavigationUtil.NotifyColleagues("FirmwareUpdate", null);
        }

        private void CustomScriptPage(object parameter)
        {
            NavigationUtil.NotifyColleagues("CustomScript", null);
        }

        private void GetProcessorAndCorePage(object parameter)
        {
            NavigationUtil.NotifyColleagues("GetProcessorAndCore", null);
        }

        private void GetProcessorInternetPage(object parameter)
        {
            NavigationUtil.NotifyColleagues("GetProcessorInternet", null);
        }

        private void SearchIdracPage(object parameter)
        {
            NavigationUtil.NotifyColleagues("SearchIdrac", null);
        }
    }
}
