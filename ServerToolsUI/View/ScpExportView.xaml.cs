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
    /// Interação lógica para ScpExportPage.xam
    /// </summary>
    public partial class ScpExportView : Page
    {
        public ScpExportView()
        {
            InitializeComponent();
        }

        private async void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            var progressDialog = new ProgressView();
            progressDialog.MessageTextBox.Text = "Exportando...";
            await DialogHost.Show(progressDialog, "MainHost");
        }
    }
}
