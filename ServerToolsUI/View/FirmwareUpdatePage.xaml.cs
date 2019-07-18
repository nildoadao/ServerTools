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

namespace ServerToolsUI.View
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
            await DialogHost.Show(serverDialog, "MainHost", DialogClosing);
        }

        private void DialogClosing(object sender, DialogClosingEventArgs e)
        {
            //cancel button was clicked, don't do any of this logic
            if (!(bool)e.Parameter)
                return;

            //This will get called when the dialog is about to close
            if (e.Session.Content is AddServerDialog dialog)
            {
                if (string.IsNullOrEmpty(dialog.ServerTextBox.Text))
                {
                    dialog.ServerTextBox.Focus();
                    e.Cancel();
                }
                else if (string.IsNullOrEmpty(dialog.UserTextBox.Text))
                {
                    dialog.UserTextBox.Focus();
                    e.Cancel();
                }
                else if (string.IsNullOrEmpty(dialog.PasswordTextBox.Password))
                {
                    dialog.PasswordTextBox.Focus();
                    e.Cancel();
                }
                else
                {
                    var server = new ServerListRow();
                    server.Text.Text = dialog.ServerTextBox.Text;
                    ServerListBox.Items.Add(server);
                }
            }

        }
    }
}
