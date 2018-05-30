using System.Net;
using System.Xml.Serialization;

namespace Ego.Model
{
    public class PlayerModel
    {
        public IPAddress PlayerIP;
        public string Name { get; set; }
        public int Points { get; set; } = 0;

        public PlayerModel(string playerName,IPAddress playerIP)
        {
            Name = playerName;
            PlayerIP = playerIP;
        }

        private void AddPoint()
        {
            Points++;
        }

        private void ResetPoints()
        {
            Points = 0;
        }

    }
}