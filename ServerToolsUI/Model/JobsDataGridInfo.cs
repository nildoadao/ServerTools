using ServerToolsIdrac.Redfish.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.Model
{
    public class JobsDataGridInfo : INotifyPropertyChanged
    {

        public JobsDataGridInfo()
        {
            Job = new IdracJob();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string server;
        public string Server
        {
            get => server;
            set
            {
                if(value != server)
                {
                    server = value;
                    NotifyPropertyChanged("Server");
                }
            }
        }

        private string serialNumber;
        public string SerialNumber
        {
            get => serialNumber;
            set
            {
                if (value != serialNumber)
                {
                    serialNumber = value;
                    NotifyPropertyChanged("SerialNumber");
                }
            }
        }

        private IdracJob job;
        public IdracJob Job
        {
            get => job;
            set
            {
                if(value != job)
                {
                    job = value;
                    NotifyPropertyChanged("Job");
                }
            }
        }
    }
}
