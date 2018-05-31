using System.Net;
using System.Windows.Controls;
using HostApp.Model;

namespace HostApp.ViewModel
{
    public class LobbyViewModel
    {
        private LobbyModel _model = new LobbyModel()
        {
            HostIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1],//ta część zwraca Ci IP twojego komputera
            TotalPlayers = 4,
            Players = new PlayerModel[]
            {
                new PlayerModel()
                {
                    PlayerName = "Antriadus",
                    PlayerNumber = 1,
                    PlayerIp = IPAddress.Parse("192.168.0.108")
                },
                new PlayerModel()
                {
                    PlayerName = "Triss",
                    PlayerNumber = 2,
                    PlayerIp = IPAddress.Parse("192.168.0.106")
                },
                new PlayerModel()
                {
                    PlayerName = "Pervial",
                    PlayerNumber = 3,
                    PlayerIp = IPAddress.Parse("192.168.0.82")
                },
                new PlayerModel()
                {
                    //PlayerName = "ktosSiu",
                    //PlayerNumber = 4,
                    //PlayerIp = IPAddress.Parse("192.168.0.4")
                }
            }

        };

        public IPAddress HostIp
        {
            get => _model.HostIP;
            set => _model.HostIP = value;
        }
        public string PlayerOneName
        {
            get
            {
                if (_model.Players[0] != null)
                    return _model.Players[0].PlayerName;
                else return null;
            }
            private set => _model.Players[0].PlayerName=value;
        }
        public string PlayerTwoName
        {
            get
            {
                if (_model.Players[1] != null)
                    return _model.Players[1].PlayerName;
                else return null;
            }
            private set => _model.Players[1].PlayerName = value;
        }
        public string PlayerThreeName
        {
            get
            {
                if (_model.Players[2] != null)
                    return _model.Players[2].PlayerName;
                else return null;
            }
            private set => _model.Players[2].PlayerName = value;
        }
        public string PlayerFourName
        {
            get
            {
                if (_model.Players[3] != null)
                    return _model.Players[3].PlayerName;
                else return null;
            }
            private set => _model.Players[3].PlayerName = value;
        }

        public IPAddress PlayerOneIp
        {
            get
            {
                if (_model.Players[0] != null)
                    return _model.Players[0].PlayerIp;
                else return null;
            }
            private set => _model.Players[0].PlayerIp = value;
        }
        public IPAddress PlayerTwoIp
        {
            get
            {
                if (_model.Players[1] != null)
                    return _model.Players[1].PlayerIp;
                else return null;
            }
            private set => _model.Players[1].PlayerIp = value;
        }
        public IPAddress PlayerThreeIp
        {
            get
            {
                if (_model.Players[2] != null)
                    return _model.Players[2].PlayerIp;
                else return null;
            }
            private set => _model.Players[2].PlayerIp = value;
        }
        public IPAddress PlayerFourIp
        {
            get
            {
                if (_model.Players[3] != null)
                    return _model.Players[3].PlayerIp;
                else return null;
            }
            private set => _model.Players[3].PlayerIp = value;
        }

    }
}