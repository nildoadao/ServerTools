using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.Model
{
    public class JobInfo : INotifyPropertyChanged
    {
        private bool isRunning = false;
        private bool hasFailed = false;
        private bool isSuccessful = false;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsRunning
        {
            get => isRunning;
            set
            {
                if (value != isRunning)
                {
                    isRunning = value;
                    NotifyPropertyChanged("IsRunning");
                }
            }
        }
        public bool HasFailed
        {
            get => hasFailed;
            set
            {
                if (value != hasFailed)
                {
                    hasFailed = value;
                    NotifyPropertyChanged("HasFailed");
                }
            }
        }
        public bool IsSuccessful
        {
            get => isSuccessful;
            set
            {
                if (value != isSuccessful)
                {
                    isSuccessful = value;
                    NotifyPropertyChanged("IsSucessful");
                }
            }
        }
    }
}
