using ServerToolsUI.Properties;
using ServerToolsUI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ServerToolsUI.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            SetThemeCommand = new RelayCommand(SetTheme);

            if (Settings.Default.Theme == MaterialDesignThemes.Wpf.BaseTheme.Dark)
                Dark = true;
            else
                Dark = false;
        }

        private bool dark;
        public bool Dark
        {
            get => dark;
            set
            {
                if(value != dark)
                {
                    dark = value;
                    NotifyPropertyChanged("Dark");
                }
            }
        }

        public RelayCommand SetThemeCommand { get; private set; }

        private void SetTheme(object parameter)
        {
            if (Dark)
                Settings.Default.Theme = MaterialDesignThemes.Wpf.BaseTheme.Dark;
            else
                Settings.Default.Theme = MaterialDesignThemes.Wpf.BaseTheme.Light;

            Settings.Default.Save();
            ((App)Application.Current).SetTheme(Settings.Default.Theme);
        }
    }
}
