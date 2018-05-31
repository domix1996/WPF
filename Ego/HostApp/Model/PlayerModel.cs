using System.Net;

namespace HostApp.Model
{
    public class PlayerModel
    {
        public string PlayerName { get; set; }
        public IPAddress PlayerIp { get; set; }
        public int PlayerNumber { get; set; }
    }
}