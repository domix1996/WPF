using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PlayerApp.View;

namespace PlayerApp.ViewModel
{
    public class LoggingViewModel:INotifyPropertyChanged
    {
        public InGame inGame = new InGame(); 
        private PlayerApp.Model.LoggingModel _model = new PlayerApp.Model.LoggingModel()
        {
            MyIPAddress  = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1],
            HostAddress = new IPAddress(0x2414188f),
            PlayerName = "Antriadus",
            PlayerNumber = 2

        };

        public IPAddress HostAdres
        {
            get => _model.HostAddress;
            set => _model.HostAddress = value;
        }

        public string PlayerName
        {
            get => _model.PlayerName;
            set => _model.PlayerName = value;
        }

        public int PlayerNumber
        {
            get => _model.PlayerNumber;
            set => _model.PlayerNumber = value;
        }

        public IPAddress MyIPAddress
        {
            get => _model.MyIPAddress;
            set => _model.MyIPAddress = value;
        }
        #region Methods

        private ICommand _connectToServerCommand;

        public ICommand ConnectToServer => _connectToServerCommand ?? (_connectToServerCommand = new ConnectToServer(this));

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
