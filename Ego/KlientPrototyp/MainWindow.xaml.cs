using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

namespace KlientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public class ViewModel : INotifyPropertyChanged

    {
        public string HostIp { get; set; }
        public int Port { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public string Chat { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            HostIp = "127.0.0.1";
            Port = 5000;
        }
    }

    public partial class MainWindow : Window
    {
        public TcpClient client;
        private Thread threadReceive;
        private Thread threadSend;
        private NetworkStream ns;

        public MainWindow()
        {
            InitializeComponent();
            DisconnectButton.IsEnabled = false;
            SendButton.IsEnabled = false;

        }

        private void Connect_OnClick(object sender, RoutedEventArgs e)
        {

            ViewModel vm = this.Resources["VM"] as ViewModel;
            IPAddress ip = IPAddress.Parse(vm.HostIp);
            int port = vm.Port;
            try
            {
                client = new TcpClient(vm.HostIp, port);
                vm.Error = $"Connected to{ip} on port {port}";
                ns = client.GetStream();

                DisconnectButton.IsEnabled = true;
                SendButton.IsEnabled = true;
                ConnectButton.IsEnabled = false;
                threadReceive = new Thread(o => ReceiveData((TcpClient)o));
                threadReceive.Start(client);
                threadSend = new Thread(o => SendData((TcpClient)o));
                threadSend.Start(client);
            }
            catch (Exception ex)
            {
                vm.Error = $"Błąd Nie udało się nawiązać połączenia!";
                MessageBox.Show(ex.ToString());
            }

        }

        private void SendData(TcpClient client)
        {
            ViewModel vm = this.Resources["VM"] as ViewModel;

            while (!string.IsNullOrEmpty((vm.Message)))
            {
                byte[] buffer = Encoding.ASCII.GetBytes(vm.Message);
                ns.Write(buffer, 0, buffer.Length);
                vm.Chat += "\n>>" + (vm.Message);

                vm.Message = String.Empty;
            }
        }

        private void ReceiveData(TcpClient client)
        {
            ViewModel vm = this.Resources["VM"] as ViewModel;
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                Process(receivedBytes);
            }
        }

        private void SendMessage_OnClick(object sender, RoutedEventArgs e)
        {
            SendData(client);
        }

        private void DisconnectButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel vm = this.Resources["VM"] as ViewModel;
            client.Client.Shutdown(SocketShutdown.Send);
            threadSend.Join();
            threadReceive.Join();
            ns.Close();
            client.Close();
            vm.Error += $"\n Disconnected from {vm.HostIp} port {vm.Port}";
            DisconnectButton.IsEnabled = false;
            SendButton.IsEnabled = false;
            ConnectButton.IsEnabled = true;
        }

        public void Process(byte[] receivedBytes)
        {
            ViewModel vm = this.Resources["VM"] as ViewModel;
            string data = System.Text.Encoding.UTF8.GetString(TransformFromCezar(receivedBytes, 10));
            vm.Chat += $"\n{data}";
        }
        static public byte[] TransformToCezar(byte[] data, byte i)
        {
            byte[] result = new byte[data.Length];
            for (int j = 0;
            j < data.Length; j++)
            {
                result[j] = data[j];
                result[j] += i;
            }

            return result;
        }
        static public byte[] TransformFromCezar(byte[] data, byte i)
        {
            byte[] result = new byte[data.Length];
            for (int j = 0; j < data.Length; j++)
            {
                result[j] = data[j];
                result[j] -= i;
            }

            return result;
        }
    }

}