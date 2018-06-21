using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
namespace Client.ViewModel.Commands
{
    public class SendDataToSerwer : ICommand
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
            string data = String.Empty;

            if (parameter.ToString().Split()[0] == "MyNameIs")
            {
                 data = parameter.ToString();
            }

            else if (parameter.ToString().Split()[0] != "MyNameIs")
            {
                if (!string.IsNullOrEmpty(_vm.MyAnswer)) return;
                _vm.MyAnswer = parameter.ToString();
                data = $"MyAnswerIs {parameter.ToString()}";
            }
            
            if (!string.IsNullOrEmpty(data))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                _vm.MyClient.GetStream().Write(buffer, 0, buffer.Length);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}

