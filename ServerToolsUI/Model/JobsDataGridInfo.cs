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
        private string server;
        private string serialNumber;
        private string jobId;
        private string jobName;
        private string jobStatus;
        private int jobPercentComplete;
        private string jobMessage;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

        public string JobId
        {
            get => jobId;
            set
            {
                if(value != JobId)
                {
                    jobId = value;
                    NotifyPropertyChanged("JobId");
                }
            } 
        }

        public string JobName
        {
            get => jobName;
            set
            {
                if(value != jobName)
                {
                    jobName = value;
                    NotifyPropertyChanged("JobName");
                }
            }
        }

        public string JobStatus
        {
            get => jobStatus;
            set
            {
                if(value != jobStatus)
                {
                    jobStatus = value;
                    NotifyPropertyChanged("JobStatus");
                }
            }
        }

        public int JobPercentComplete
        {
            get => jobPercentComplete;
            set
            {
                if(value != jobPercentComplete)
                {
                    jobPercentComplete = value;
                    NotifyPropertyChanged("JobPercentComplete");
                }
            }
        }

        public string JobMessage
        {
            get => jobMessage;
            set
            {
                if(value != jobMessage)
                {
                    jobMessage = value;
                    NotifyPropertyChanged("JobMessage");
                }
            }
        }
    }
}
