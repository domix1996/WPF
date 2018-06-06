using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace HostApp.ViewModel
{
    public class TestCommand:ICommand
    {
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            string res = parameter as string;
            MessageBox.Show("Działa"+res);
        }

        public event EventHandler CanExecuteChanged;
    }
}