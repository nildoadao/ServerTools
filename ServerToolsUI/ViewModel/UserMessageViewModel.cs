using MaterialDesignThemes.Wpf;
using ServerToolsUI.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerToolsUI.ViewModel
{
    public class UserMessageViewModel : ViewModelBase
    {
        public UserMessageViewModel(string message)
        {
            OkCommand = new RelayCommand(Ok);
            CancelCommand = new RelayCommand(Cancel);
            Message = message;
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

        public RelayCommand OkCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        private void Ok(object parameter)
        {
            DialogHost.CloseDialogCommand.Execute(true, null);
        }

        private void Cancel(object parameter)
        {
            DialogHost.CloseDialogCommand.Execute(false, null);
        }

    }
}
