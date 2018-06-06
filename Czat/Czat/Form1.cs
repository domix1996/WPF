using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Czat
{
    public partial class Form1 : Form
    {
        private Socket listenerSocket;
        private long serverIP;
        private int serverPort;

        public Form1()
        {
            InitializeComponent();
            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 8000);
            Socket newsock = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
            newsock.Bind(localEndPoint);
            newsock.Listen(10);
            Socket client = newsock.Accept();
            bwListener = new BackgroundWorker();
            bwListener.DoWork += new DoWorkEventHandler(StartToListen);
            bwListener.RunWorkerAsync();

        }
        private void StartToListen(object sender, DoWorkEventArgs e)
        {
            this.listenerSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            this.listenerSocket.Bind(
                new IPEndPoint(this.serverIP, this.serverPort));
            this.listenerSocket.Listen(200);
            while (true)
                this.CreateNewClientManager(this.listenerSocket.Accept());
        }
    }
}
