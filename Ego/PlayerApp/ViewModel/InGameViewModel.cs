using System.Net.Sockets;

namespace PlayerApp.ViewModel
{
    public class InGameViewModel
    {
        public TcpClient MyClient { get; set; }
        public string MyName { get; set; }
        public int PlayerNumber { get; set; }
        public string MyIpAddress { get; set; }

    }
}