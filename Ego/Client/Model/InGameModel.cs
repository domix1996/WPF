using System.Net.Sockets;

namespace Client.Model
{
    public class InGameModel
    {
        public TcpClient MyClient { get; set; }
        public NetworkStream MyNetworkStream { get; set; }
        public string MyName { get; set; }
        public int MyNumber { get; set; }

    }
}