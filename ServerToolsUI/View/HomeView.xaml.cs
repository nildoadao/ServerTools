using ServerToolsUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ServerToolsUI.View
{
    /// <summary>
    /// Interação lógica para HomePage.xam
    /// </summary>
    public partial class HomeView : Page
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void FirmwareUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var view = new FirmwareUpdateView()
            {
                DataContext = new FirmwareUpdateViewModel()
            };
            NavigationService.Navigate(view);
        }

        private void ScpExportButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ScpExportView());
        }
    }
}
