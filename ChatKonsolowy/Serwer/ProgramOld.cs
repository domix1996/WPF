//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Runtime.Remoting.Messaging;
//using System.Text;
//using System.Threading;

//public class Player
//{
//    public string PlayerName { get; set; }
//    public int PlayerNumber { get; set; }
//    public TcpClient PlayerTcpClient { get; set; }

//    public Player(string playerName, int playerNumber, TcpClient playerTcpClient)
//    {
//        PlayerName = playerName;
//        PlayerNumber = playerNumber;
//        PlayerTcpClient = playerTcpClient;
//    }

//    public Player(string playerName, int playerNumber, string playerIPAdress, int playerPort)
//    {
//        PlayerName = playerName;
//        PlayerNumber = playerNumber;
//        PlayerTcpClient = new TcpClient(playerIPAdress,playerPort);
//    }
//}
//class Program
//{
//    static readonly object _lock = new object();
//    static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
//    static  Dictionary<int, TcpClient> PlayerList = new Dictionary<int, TcpClient>();

//    static void Main(string[] args)
//    {
//        int count = 1;

//        TcpListener ServerSocket = new TcpListener(IPAddress.Any, 5000);
//        ServerSocket.Start();

//        while (true)//czekamy aż dojdzie nowy klient i wtedy go dodajemy 
//        {
//            TcpClient client = ServerSocket.AcceptTcpClient();
//            lock (_lock) list_clients.Add(count, client);
//            Console.WriteLine("Someone connected!!");
//            Console.WriteLine("List of connected users");
//            foreach (var lc in list_clients)
//            {
//                Console.WriteLine(lc.Value.Client.RemoteEndPoint);
//            }
//            Thread t = new Thread(HandleClient);
//            t.Start(count);
//            Console.WriteLine(count);
//            count++;

//        }
//    }

//    public static void HandleClient(object id)
//    {
//        int ID = (int)id;
//        TcpClient client;
//        NetworkStream stream;

//        lock (_lock) client = list_clients[ID];

//        while (true)//czytamy co nam wysyła klient i przetwarzamy
//        {
//            stream = client.GetStream();
//            byte[] buffer = new byte[1024];
//            int byte_count = stream.Read(buffer, 0, buffer.Length);

//            if (byte_count == 0) break;

//            Process(buffer, client);
//        }

//        lock (_lock) list_clients.Remove(ID);
//        client.Client.Shutdown(SocketShutdown.Both);
//        client.Close();
//    }
//    /// <summary>
//    /// nie wysyłaj do clientExcluded,bo od niego dostałeś
//    /// </summary>
//    /// <param name="data"></param>
//    /// <param name="clientExcluded"></param>
//    public static void DataBroadcast(string data, TcpClient clientExcluded)
//    {
//        byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

//        lock (_lock)
//        {
//            foreach (TcpClient c in list_clients.Values)
//            {
//                if (c != clientExcluded)
//                {
//                    SentDataToClient(buffer, c);
//                }
//            }
//        }
//    }
//    public static void SentDataToClient(string data, TcpClient client)
//    {
//        lock (_lock)
//        {
//            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

//            NetworkStream stream = client.GetStream();
//            stream.Write(buffer, 0, buffer.Length);
//        }
//    }
//    public static void SentDataToClient(byte[] data, TcpClient client)
//    {
//        lock (_lock)
//        {
//            NetworkStream stream = client.GetStream();
//            stream.Write(data, 0, data.Length);
//        }
//    }
//    public static void Process(byte[] receivedBytes, TcpClient client)
//    {
//        string data = System.Text.Encoding.UTF8.GetString(receivedBytes).Replace("\0", "");
//        if (data == "A")
//        {
//            DataBroadcast($"Odpowiedz gracza to A", client);
//        }
//        if (data == "B")
//        {
//            DataBroadcast($"Odpowiedz gracza to B", client);
//        }

//    }
//}
