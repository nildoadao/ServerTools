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

        private string jobId;
        public string JobId
        {
            get => jobId;
            set
            {
                if(value != jobId)
                {
                    jobId = value;
                    NotifyPropertyChanged("JobId");
                }
            }
        }

        private string jobName;
        public string JobName
        {
            get => jobName;
            set
            {
                if (value != jobName)
                {
                    jobName = value;
                    NotifyPropertyChanged("JobName");
                }
            }
        }

        private string jobStatus;
        public string JobStatus
        {
            get => jobStatus;
            set
            {
                if (value != jobStatus)
                {
                    jobStatus = value;
                    NotifyPropertyChanged("JobStatus");
                }
            }
        }

        private int jobPercentComplete;
        public int JobPercentComplete
        {
            get => jobPercentComplete;
            set
            {
                if (value != jobPercentComplete)
                {
                    jobPercentComplete = value;
                    NotifyPropertyChanged("JobPercentComplete");
                }
            }
        }

        private string jobMessage;
        public string JobMessage
        {
            get => jobMessage;
            set
            {
                if (value != jobMessage)
                {
                    jobMessage = value;
                    NotifyPropertyChanged("JobMessage");
                }
            }
        }

        private string jobUri;
        public string JobUri
        {
            get => jobUri;
            set
            {
                if(value != jobUri)
                {
                    jobUri = value;
                    NotifyPropertyChanged("JobUri");
                }
            }
        }
    }
}
