using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace Ego.Model
{
    public class HostModel
    {
        private Socket _socket { get; set; }
        public List<PlayerModel> Players { get; set; }

        private void Broadcost(string msg)
        {

            foreach (var player in Players)
            {
                TcpClient broadcastSocket;
                broadcastSocket = (TcpClient)player.PlayerIP;
                NetworkStream broadcastStream = broadcastSocket.GetStream();
                Byte[] broadcastBytes = null;

                if (flag == true)
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(uName + " says : " + msg);
                }
                else
                {
                    broadcastBytes = Encoding.ASCII.GetBytes(msg);
                }

                broadcastStream.Write(broadcastBytes, 0, broadcastBytes.Length);
                broadcastStream.Flush(); 
            }
        }

        public HostModel()
        {

        }
    }
}