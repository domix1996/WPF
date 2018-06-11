using System.Net;
using System.Net.Sockets;

namespace PlayerApp.Model
{
    public class LoggingModel
    {
        public string MyName { get; set; }
        public string HostAddress { get; set; }
        public int HostPort { get; set; }
        public string MyIpAddress { get; set; }

    }
}