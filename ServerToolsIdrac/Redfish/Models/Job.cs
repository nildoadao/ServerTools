using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsIdrac.Redfish.Models
{
    public class Job : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string id;
        public string Id
        {
            get => id;
            set
            {
                if(value != id)
                {
                    id = value;
                    NotifyPropertyChanged("Id");
                }
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if(value != name)
                {
                    name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private string jobState;
        public string JobState
        {
            get => jobState;
            set
            {
                if(value != jobState)
                {
                    jobState = value;
                    NotifyPropertyChanged("JobState");
                }
            }
        }

        private string message;
        public string Message
        {
            get => message;
            set
            {
                if(value != message)
                {
                    message = value;
                    NotifyPropertyChanged("Message");
                }
            }
        }

        private int percentComplete;
        public int PercentComplete
        {
            get => percentComplete;
            set
            {
                if(value != percentComplete)
                {
                    percentComplete = value;
                    NotifyPropertyChanged("PercentComplete");
                }
            }
        }

    }
}
