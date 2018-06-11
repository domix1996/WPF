using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Serwer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class ViewModel : INotifyPropertyChanged
    {
        public string HostIp { get; set; }
        public int Port { get; set; }
        public string Error { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public partial class MainWindow : Window
    {
        private TcpListener serwer;
        private TcpClient klient;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel vm = this.Resources["VM"] as ViewModel;
            vm.Error = "Oczekiwanie na połączenie\n";
            IPAddress adresIP;
            try
            {
                adresIP = IPAddress.Parse(vm.HostIp);
            }
            catch
            {
                MessageBox.Show("Błędny format adresu IP!", "Błąd");
                vm.HostIp = String.Empty;
                return;
            }

            int port = vm.Port;
            try
            {
                serwer = new TcpListener(adresIP, port);
                serwer.Start();
                serwer.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), serwer);
            }
            catch (Exception ex)
            {
                vm.Error = "Błąd inicjacji serwera!";
                MessageBox.Show(ex.ToString(), "Błąd");
            }
        }

        private void AcceptTcpClientCallback(IAsyncResult asyncResult)
        {
            ViewModel vm = this.Resources["VM"] as ViewModel;
            serwer= (TcpListener)asyncResult.AsyncState;

            klient = serwer.EndAcceptTcpClient(asyncResult);
            SetListBoxText("Połączenie się powiodło!\n");
           
        }

        private void SetListBoxText(string tekst)
        {
            ViewModel vm = this.Resources["VM"] as ViewModel;
            vm.Error += tekst;
        }

        private void StopButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (serwer is null)
            {
                serwer.Stop();
                klient.Close();
            }

            SetListBoxText("Zakonczono pracę serwera");

        }

        private void RefreshButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (serwer is null)  return;
            NetworkStream ns = klient.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length);
            byte[] formated = new byte[byte_count];
            Array.Copy(receivedBytes, formated, byte_count); //handle  the null characteres in the byte array
            string data = Encoding.ASCII.GetString(formated);
            SetListBoxText(data);
        }
    }
}
