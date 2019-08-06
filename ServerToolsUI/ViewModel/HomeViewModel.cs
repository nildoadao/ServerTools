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
        }
        public RelayCommand ScpImportPageCommand { get; private set; }
        public RelayCommand ScpExportPageCommand { get; private set; }
        public RelayCommand FirmwareUpdatePageCommand { get; private set; }

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
    }
}
