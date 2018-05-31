using System.Net;
using HostApp.Model;

namespace HostApp.Model
{
    public class LobbyModel
    {
        public int TotalPlayers { get; set; }
        public IPAddress HostIP { get; set; }
        public PlayerModel[] Players { get; set; }
     
    }
}