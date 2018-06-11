using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Text;
using System.Windows;
namespace Klient
{
    
    public class ViewModel:INotifyPropertyChanged

    {
        public string HostIp { get; set; }
        public int Port { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
    public partial class MainWindow : Window
    {
        public TcpClient klient;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel vm = this.Resources["VM"] as ViewModel;
            string host = vm.HostIp;
            int port = vm.Port;
            try
            {
                klient = new TcpClient(host, port);
                vm.Error=$"Nawiązano połączenie z + {host} +  na porcie: +{ port}\n";
            }
            catch (Exception ex)
            {
                vm.Error = $"Błąd Nie udało się nawiązać połączenia!";
                MessageBox.Show(ex.ToString());
            }
        }

        private void SendMessage_OnClick(object sender, RoutedEventArgs e)
        {
            if(klient is null) return;
            ViewModel vm = this.Resources["VM"] as ViewModel;

            byte[] dane = Encoding.ASCII.GetBytes(vm.Message);

            NetworkStream stream = klient.GetStream();

            stream.Write(dane, 0, dane.Length);
        }

        private void Disconnect_OnClick(object sender, RoutedEventArgs e)
        {
            if (klient is null) return;
            ViewModel vm = this.Resources["VM"] as ViewModel;
            vm.Error += "Rozłączono";
            klient.Close();
        }
    }
}
