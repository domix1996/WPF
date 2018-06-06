using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace PlayerApp.ViewModel
{
    public class ConnectToServer:ICommand
    {
        private readonly LoggingViewModel _viewModel;

        public bool CanExecute(object parameter)
        {
            string msg= string.Empty;
            bool can = true;
            if (_viewModel.PlayerName.Length == 0)
            {
                msg += "Wpisz nazwę gracza\n";
                can = false;
            }
            if (_viewModel.HostAdres is null)
            {
                msg += "Wpisz IP Hosta";
                can = false;
            }

             if(msg.Length>0)MessageBox.Show(msg);
            return can;
        }

        public ConnectToServer(LoggingViewModel viewModel)
        {
            if(viewModel is null) throw new ArgumentNullException("viewModel");
            this._viewModel = viewModel;
        }
        public void Execute(object parameter)
        {
            App.Current.MainWindow.Close();
            _viewModel.inGame.Show();
            //MessageBox.Show("Wysyłam do serwera");
        }

        public event EventHandler CanExecuteChanged;
    }
}