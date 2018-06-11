using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using CezarCode;
namespace Client.ViewModel.Commands
{
    public class SendDataToSerwer:ICommand
    {
        private InGameViewModel _vm;

        public SendDataToSerwer(InGameViewModel viewModel)
        {
            _vm = viewModel;
        }
        public bool CanExecute(object parameter)
        {
            if (_vm.MyNetworkStream is null) return false;
            if (string.IsNullOrEmpty(parameter.ToString())) return false;
            return true;
        }

        public void Execute(object parameter)
        {
            string data = parameter.ToString();
            if (!string.IsNullOrEmpty(data))
            {
                byte[] buffer = CezarCode.CezarCode.TransformToCezar(Encoding.ASCII.GetBytes(data), 10);
                _vm.MyClient.GetStream().Write(buffer, 0, buffer.Length);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}

