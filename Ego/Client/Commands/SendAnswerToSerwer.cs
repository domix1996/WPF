﻿using System;
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

            if (parameter.ToString().Split()[0] == "MyAnswerIs")
            {
                _vm.MyAnswer = parameter.ToString().Split()[1];
            }

            string data = parameter.ToString();
            if (!string.IsNullOrEmpty(data))
            {
                byte[] buffer = Encoding.ASCII.GetBytes(data);
                _vm.MyClient.GetStream().Write(buffer, 0, buffer.Length);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}

