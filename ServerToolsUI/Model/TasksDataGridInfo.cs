using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.Model
{
    public class TasksDataGridInfo : INotifyPropertyChanged
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

        private Task scriptTask;
        public Task ScriptTask
        {
            get => scriptTask;
            set
            {
                if(value != scriptTask)
                {
                    scriptTask = value;
                    NotifyPropertyChanged("ScriptTask");
                }
            }
        }
    }
}
