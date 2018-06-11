using System.ComponentModel;
using System.Net.Sockets;

namespace SerwerKonsola
{
    public class Player : INotifyPropertyChanged
    {
        public string PlayerName { get; set; }
        public int PlayerId { get; set; }
        public TcpClient PlayerTcpClient { get; set; }
        public char LastAnswer { get;
            set; }
        public Player(int playerID, TcpClient playerTcpClient, string playerName = "Annon")
        {
            PlayerName = playerName;
            PlayerId = playerID;
            PlayerTcpClient = playerTcpClient;
        }
        //public Player(int playerID, string playerIPAdress, int playerPort, string playerName = "Annon")
        //{
        //    PlayerName = playerName;
        //    PlayerId = playerID;
        //    PlayerTcpClient = new TcpClient(playerIPAdress, playerPort);
        //}

        public event PropertyChangedEventHandler PropertyChanged;
    }
}