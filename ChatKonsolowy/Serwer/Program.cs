using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using Serwer.Annotations;

public class Player : INotifyPropertyChanged
{
    public string PlayerName { get; set; }
    public int PlayerId { get; set; }
    public TcpClient PlayerTcpClient { get; set; }
    public char LastAnswet { get; set; }
    public Player(int playerID, TcpClient playerTcpClient, string playerName = "Annon")
    {
        PlayerName = playerName;
        PlayerId = playerID;
        PlayerTcpClient = playerTcpClient;
    }
    public Player(int playerID, string playerIPAdress, int playerPort, string playerName = "Annon")
    {
        PlayerName = playerName;
        PlayerId = playerID;
        PlayerTcpClient = new TcpClient(playerIPAdress, playerPort);
    }

    public event PropertyChangedEventHandler PropertyChanged;

}
class Program
{
    static readonly object _lock = new object();
    static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
    static Dictionary<int, Player> PlayersList = new Dictionary<int, Player>();

    static void Main(string[] args)
    {
        int count = 0;

        TcpListener ServerSocket = new TcpListener(IPAddress.Any, 5000);
        ServerSocket.Start();

        while (true)//czekamy aż dojdzie nowy klient i wtedy go dodajemy 
        {
            TcpClient client = ServerSocket.AcceptTcpClient();
            lock (_lock) list_clients.Add(count, client);
            lock (_lock) PlayersList.Add(count, new Player(count, client));
            Console.WriteLine("Someone connected!!");
            //Console.WriteLine("List of connected users");
            //foreach (var lc in list_clients)
            //{
            //    Console.WriteLine(lc.Value.Client.RemoteEndPoint);
            //}
            Thread t = new Thread(HandleClient);
            t.Start(count);
            Console.WriteLine(count);
            count++;

        }
    }

    public static void HandleClient(object id)
    {
        int ID = (int)id;

        Player player = PlayersList[ID];


        while (true)//czytamy co nam wysyła klient i przetwarzamy
        {
            NetworkStream stream = player.PlayerTcpClient.GetStream();
            byte[] received = new byte[1024];
            int byte_count = stream.Read(received, 0, received.Length);

            if (byte_count == 0) break;

            Process(received, player);
        }

        //lock (_lock) list_clients.Remove(ID);
        //client.Client.Shutdown(SocketShutdown.Both);
        //client.Close();
    }
    /// <summary>
    /// nie wysyłaj do clientExcluded,bo od niego dostałeś
    /// </summary>
    /// <param name="data"></param>
    /// <param name="clientExcluded"></param>
    public static void DataBroadcast(string data, Player playerExcluded)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

        lock (_lock)
        {
            for (int i = 0; i < PlayersList.Count; i++)
            {
                if (PlayersList[i] != playerExcluded)
                    SentDataToPlayer(data, PlayersList[i]);
            }
        }
    }
    public static void DataBroadcast(string data)
    {
        byte[] buffer = TransformToCezar(Encoding.ASCII.GetBytes(data + Environment.NewLine),10);
       
        lock (_lock)
        {
            for (int i = 0; i < PlayersList.Count; i++)
            {
                SentDataToPlayer(buffer, PlayersList[i]);
            }
        }
    }
    public static void SentDataToPlayer(string data, Player player)
    {
        lock (_lock)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data + Environment.NewLine);

            NetworkStream stream = player.PlayerTcpClient.GetStream();
            stream.Write(buffer, 0, buffer.Length);
        }
    }
    public static void SentDataToPlayer(byte[] data, Player player)
    {
        lock (_lock)
        {
            NetworkStream stream = player.PlayerTcpClient.GetStream();
            stream.Write(data, 0, data.Length);
        }
    }
    //public static void Process(byte[] receivedBytes, TcpClient client)
    //{
    //    string data = System.Text.Encoding.UTF8.GetString(receivedBytes).Replace("\0", "");
    //    if (data == "A")
    //    {
    //        DataBroadcast($"Odpowiedz gracza to A", client);
    //    }
    //    if (data == "B")
    //    {
    //        DataBroadcast($"Odpowiedz gracza to B", client);
    //    }
    //}
    public static void Process(byte[] receivedBytes, Player player)
    {
        string data = System.Text.Encoding.UTF8.GetString(receivedBytes).Replace("\0", "");
        string command = data.Split()[0];
        string commandValue = data.Substring(data.IndexOf(' ') >= 0 ? data.IndexOf(' ') : 0);
        switch (command)//rozpoznajemy rządanie od gracza
        {
            case "MyNameIs": //gracz zmienia swoją nazwę
                {
                    DataBroadcast(
                        $"\nGracz {player.PlayerName} o IP {player.PlayerTcpClient.Client.RemoteEndPoint} zmienił nazwę na {commandValue}");
                    player.PlayerName = commandValue;
                    break;
                }

            case "MyAnswerIs": //gracz udziela odpowiedzi
                {
                    player.LastAnswet = commandValue[0];
                    DataBroadcast($"\nOdpowiedz gracza {player.PlayerName} o IP {player.PlayerTcpClient.Client.RemoteEndPoint} to {commandValue}");
                }
                break;
            case "NextQuestion": //gracz udziela odpowiedzi
            {
                SendNextQuestionToPlayers();
            }
                break;
            default:
            {
                    DataBroadcast(data);
            }
                break;
        }
    }
    static public void SendNextQuestionToPlayers()
    {
        string question = "Ile to jest 2+2?";
        string correctAnswer = "A";
        string answerA = "2";
        string answerB = "3";
        string answerC = "4";
        string answerD = "5";
        StringBuilder sb = new StringBuilder();
        sb.Append("ThisIsNewQuestion ");
        sb.Append($"CorrectAnswer {correctAnswer}+=+");
        sb.Append($"Question {question}+=+");
        sb.Append($"AnswerA wynik to {answerA}+=+");
        sb.Append($"AnswerB wynik to {answerB}+=+");
        sb.Append($"AnswerC wynik to {answerC}+=+");
        sb.Append($"AnswerD wynik to {answerD}+=+");
        DataBroadcast(sb.ToString());
    }
     
    static public byte[] TransformToCezar(byte[]data,byte i)
    {
        byte[] result =new byte[data.Length];
        for (int j = 0; j < data.Length; j++)
        {
            result[j] = data[j];
            result[j] += i;
        }

        return result;
    }
    static  public byte[] TransformFromCezar(byte[] data, byte i)
    {
        byte[] result = new byte[data.Length];
        for (int j = 0; j < data.Length; j++)
        {
            result[j] = data[j];
            result[j] -= i;
        }

        return result;
    }
}
