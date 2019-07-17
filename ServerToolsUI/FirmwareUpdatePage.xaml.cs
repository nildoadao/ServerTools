using MaterialDesignThemes.Wpf;
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

namespace ServerToolsUI
{
    /// <summary>
    /// Interação lógica para FirmwareUpdatePage.xam
    /// </summary>
    public partial class FirmwareUpdatePage : Page
    {
        public FirmwareUpdatePage()
        {
            InitializeComponent();
        }

        private async void AddServerButton_Click(object sender, RoutedEventArgs e)
        {
            var serverDialog = new AddServerDialog();
            await DialogHost.Show(serverDialog, "MainHost");
            ServersListBox.Items.Add(serverDialog.ServerTextBox.Text);
        }
    }
}
