using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using SerwerConsola;
using SerwerConsola.Properties;


namespace SerwerKonsola
{

    class Program
    {
        //static readonly object _lock = new object();
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
        static Dictionary<int, Player> PlayersList = new Dictionary<int, Player>();
        static int answerCount = 0;
        private static int NumberOfPlayers { get; set; } = 2;

        public static QuestionManager _questionManager { get; set; }
        static void Main(string[] args)
        {
            _questionManager = new QuestionManager();
            _questionManager.GetQuestionsFromFile();
            int count = 0;
            TcpListener ServerSocket = new TcpListener(IPAddress.Any, 5000);
            ServerSocket.Start();

            while (true)
            {
                TcpClient client = ServerSocket.AcceptTcpClient();

                //lock (_lock) list_clients.Add(count, client);
                //lock (_lock) PlayersList.Add(count, new Player(count, client));                
                list_clients.Add(count, client);
                PlayersList.Add(count, new Player(count, client));
                //Console.WriteLine("Someone connected!!");
                Thread t = new Thread(HandleClient);
                t.Start(count);
                Console.WriteLine(count);
                count++;
                if (count == NumberOfPlayers)
                {
                    SendNextQuestionToPlayers();
                }
            }
        }

        public static void HandleClient(object id)
        {
            int ID = (int)id;

            Player player = PlayersList[ID];

            while (true)
            {
                NetworkStream stream = player.PlayerTcpClient.GetStream();
                byte[] received = new byte[1024];
                int byte_count = stream.Read(received, 0, received.Length);


                if (byte_count == 0) break;

                Process(received, player);
            }

            list_clients.Remove(ID);
            // lock (_lock) list_clients.Remove(ID);
            player.PlayerTcpClient.Client.Shutdown(SocketShutdown.Both);
            player.PlayerTcpClient.Close();
        }

        //public static void DataBroadcast(string data, Player playerExcluded)
        //{
        //    byte[] buffer = Encoding.UTF8.GetBytes(data + Environment.NewLine);

        //    lock (_lock)
        //    {
        //        for (int i = 0; i < PlayersList.Count; i++)
        //        {
        //            if (PlayersList[i] != playerExcluded)
        //                SentDataToPlayer(data, PlayersList[i]);
        //        }
        //    }
        //}




        public static void Process(byte[] receivedBytes, Player player)
        {
            string data = System.Text.Encoding.UTF8.GetString(receivedBytes).Replace("\0", "");
            string command = data.Split()[0];
            string commandValue = data.Substring(data.IndexOf(' ') >= 0 ? data.IndexOf(' ') : 0);

            switch (command) //rozpoznajemy rzÄ…danie od gracza
            {
                case "MyNameIs": //gracz zmienia swoją nazwę
                    {
                        Console.WriteLine($"{commandValue} dołączył do gry");

                        player.PlayerName = commandValue;
                        break;
                    }

                case "MyAnswerIs": //gracz udziela odpowiedzi
                    {
                        player.LastAnswer = commandValue[1];
                        Console.WriteLine($"\n\t{player.PlayerName}: Moja odpowiedz to {player.LastAnswer}");
                        answerCount++;
                        if (answerCount == NumberOfPlayers)
                        {
                            answerCount = 0;
                            AnswerProceed();
                            Thread.Sleep(7000);
                            SendNextQuestionToPlayers();
                        }
                    }
                    break;
                case "NextQuestion": //gracz udziela odpowiedzi
                    {
                        SendNextQuestionToPlayers();
                    }
                    break;
            }
        }

        static public void SendNextQuestionToPlayers()
        {
            _questionManager.NextQuestion();
            Question question = _questionManager.GetCurrentQuestion();
            if (question != null)
            {
                SendQuestionToPlayers(question);
            }
            else
            {
                Console.WriteLine("KoniecPytań");
            }

        }
        public static void DataBroadcast(byte[]  utf8Data)
        {
            for (int i = 0; i < PlayersList.Count; i++)
            {
                SentDataToPlayer(utf8Data, PlayersList[i]);
            }
        }
        public static void DataBroadcast(string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);

            for (int i = 0; i < PlayersList.Count; i++)
            {
                SentDataToPlayer(buffer, PlayersList[i]);
            }
        }
        public static void SentDataToPlayer(string data, Player player)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data);
            NetworkStream stream = player.PlayerTcpClient.GetStream();
            stream.Write(buffer, 0, buffer.Length);
        }
        public static void SentDataToPlayer(byte[] utf8Data, Player player)
        {
            NetworkStream stream = player.PlayerTcpClient.GetStream();
            stream.Write(utf8Data, 0, utf8Data.Length);
        }

        static public void SendQuestionToPlayers(Question question)
        {
            Console.WriteLine("----------------------------------------\n");
            Console.WriteLine($"Wysyłam pytanie  {question.QuestionNumber}/{question.QuestionNumberTotal}");
            StringBuilder sb = new StringBuilder();
            sb.Append("ThisIsNewQuestion+=+");
            sb.Append($"CA:{question.CorrectAnswer}+=+");
            sb.Append($"Q:{question.QuestionText}+=+");
            sb.Append($"A:{question.AnswerA}+=+");
            sb.Append($"B:{question.AnswerB}+=+");
            sb.Append($"C:{question.AnswerC}+=+");
            sb.Append($"D:{question.AnswerD}+=+");
            sb.Append($"N:{question.QuestionNumber}+=+");
            sb.Append($"T:{question.QuestionNumberTotal}");
            DataBroadcast(sb.ToString());
        }

        static public void AnswerProceed()
        {

            for (int i = 0; i < NumberOfPlayers; i++)
            {
                Player player = PlayersList[i];
                if (player.LastAnswer == _questionManager.GetCurrentQuestion().CorrectAnswer)
                {
                    SentDataToPlayer(Resources.GoodAnswerString, player);
                    player.Points++;
                }
                else
                {
                    SentDataToPlayer(Resources.BadAnswerString, player);
                }
                SentDataToPlayer($"YourPoints+=+{player.Points.ToString()}", player);

            }
            
        }


    }


}