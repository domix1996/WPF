using System.Net;

namespace PlayerApp.Model
{
    public class LoggingModel
    {
        public IPAddress HostAddress { get; set; }
        public IPAddress MyIPAddress { get; set; }
        public string PlayerName { get; set; }
        public int PlayerNumber { get; set; }

    }
}