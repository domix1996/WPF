using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using Client.View;

namespace Client.ViewModel.Commands
{
    public class ConnectToServerCommand : ICommand
    {
        private LoggingViewModel _loggingViewModel;

        public ConnectToServerCommand(LoggingViewModel loggingViewModel)
        {
            _loggingViewModel = loggingViewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (String.IsNullOrEmpty(_loggingViewModel.Error)) return true;
            return false;
        }

        public void Execute(object parameter)
        {
            try
            {
                TcpClient client = new TcpClient(_loggingViewModel.HostIp, Int32.Parse(_loggingViewModel.HostPort));
                _loggingViewModel.GameView = new GameView();
                GameViewModel inGameViewModel = _loggingViewModel.GameView.DataContext as GameViewModel;

                inGameViewModel.MyClient = client;
                inGameViewModel.MyNetworkStream = client.GetStream();//łączy do serwera
                inGameViewModel.MyName = _loggingViewModel.MyName;
                inGameViewModel.StartTransmision();

                App.Current.MainWindow.Hide();
                _loggingViewModel.GameView.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        public event EventHandler CanExecuteChanged;

    }
}