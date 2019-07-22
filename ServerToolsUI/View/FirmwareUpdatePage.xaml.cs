using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using ServerToolsIdrac.Redfish.Common;
using ServerToolsIdrac.Redfish.Firmware;
using ServerToolsIdrac.Redfish.Util;
using ServerToolsUI.Model;
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
                    ConnectionInfo server = new ConnectionInfo()
                    {
                        Host = dialog.ServerTextBox.Text,
                        User = dialog.UserTextBox.Text,
                        Password = dialog.PasswordTextBox.Password
                    };
                    ServersDataGrid.Items.Add(server);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ServersDataGrid.Items.Remove(((Button)(e.Source)).DataContext);
        }

        private void OpenFolderButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Idrac Firmware (*.exe)(*.d7)(*.pm)| *.exe;*.d7;*.pm"
            };
            dialog.FileOk += Firmware_OK;
            dialog.ShowDialog();
        }

        private void Firmware_OK(object sender, System.ComponentModel.CancelEventArgs e)
        {
            OpenFileDialog dialog = (OpenFileDialog)sender;
            FirmwareTextBox.Text = dialog.FileName;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!await CheckForm())
                return;
            
            foreach(ConnectionInfo connection in ServersDataGrid.Items)
            {
                string mode = ((ComboBoxItem)ModeCombobox.SelectedItem).Tag.ToString();
                string path = FirmwareTextBox.Text;
                JobsDataGridInfo job = new JobsDataGridInfo()
                {
                    Server = connection.Host,
                    JobStatus = "Requested",
                };
                JobsDataGrid.Items.Add(job);
                FirmwareAction firmware = new FirmwareAction(connection);
                try
                {
                    job.JobId = await firmware.UpdateFirmwareAsync(path, mode);                  
                }
                catch(RedfishException rex)
                {
                    job.JobMessage = string.Format("Erro na requisição Redfish: {0}", rex.Message);
                }
                catch(Exception ex)
                {
                    job.JobMessage = string.Format("Erro na requisição: {0}", ex.Message);
                }
            }
        }

        private async Task<bool> CheckForm()
        {
            if (string.IsNullOrEmpty(FirmwareTextBox.Text))
            {
                var messageDialog = new UserMessageDialog();
                messageDialog.MessageTextBox.Text = "Selecione um firmware";
                await DialogHost.Show(messageDialog, "MainHost");
                return false;
            }

            if (ModeCombobox.SelectedIndex == -1)
            {
                var messageDialog = new UserMessageDialog();
                messageDialog.MessageTextBox.Text = "Selecione o modo de instalação";
                await DialogHost.Show(messageDialog, "MainHost");
                return false;
            }

            if (ServersDataGrid.Items.Count == 0)
            {
                var messageDialog = new UserMessageDialog();
                messageDialog.MessageTextBox.Text = "Adicione ao menos 1 servidor para atualização";
                await DialogHost.Show(messageDialog, "MainHost");
                return false;
            }

            return true;
        }
    }
}
