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

       // private Dictionary<int, TcpClient> _listOfClients;// = new Dictionary<int, TcpClient>();
        private Dictionary<int, Player> _listOfPlayers;// = new Dictionary<int, Player>();
        private QuestionManager _questionManager;

        private int _numberOfPlayers;
        private int _answerCount;
        private int _playersCount;

        public int Port;

        void Main(string[] args)
        {
            _questionManager=new QuestionManager();
            _playersCount = 0;
            Port = 5000;

            // _questionManager = new QuestionManager();
            // _questionManager.GetQuestionsFromFile();
            TcpListener ServerSocket = new TcpListener(IPAddress.Any, Port);
            ServerSocket.Start();
            Console.WriteLine($"Uruchomiono serwe na adresie {Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString()} i porcie {Port}");
            Console.WriteLine("\n Wprowadź liczbę graczy: ");
            _numberOfPlayers = Int32.Parse(Console.ReadLine());
            while (true)
            {
                TcpClient client = ServerSocket.AcceptTcpClient();

                //_listOfClients.Add(_playersCount, client);
                _listOfPlayers.Add(_playersCount, new Player(_playersCount, client));
                Thread t = new Thread(HandleClient);
                t.Start(_playersCount);
                Console.WriteLine(_playersCount);
                _playersCount++;
                if (_playersCount == _numberOfPlayers)
                {
                    SendNextQuestionToPlayers();
                }
            }
        }



        #region StaticMethods

        public static void HandleClient(object id)
        {
            int ID = (int)id;

            Player player = _listOfPlayers[ID];

            while (true)
            {
                NetworkStream stream = player.PlayerTcpClient.GetStream();
                byte[] received = new byte[1024];
                int byte_count = stream.Read(received, 0, received.Length);


                if (byte_count == 0) break;

                Process(received, player);
            }

            _listOfClients.Remove(ID);
            // lock (_lock) _listOfClients.Remove(ID);
            player.PlayerTcpClient.Client.Shutdown(SocketShutdown.Both);
            player.PlayerTcpClient.Close();
        }
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
                        _answerCount++;
                        if (_answerCount == _numberOfPlayers)
                        {
                            _answerCount = 0;
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
        public static void SendNextQuestionToPlayers()
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
        public static void DataBroadcast(byte[] utf8Data)
        {
            for (int i = 0; i < _listOfPlayers.Count; i++)
            {
                SentDataToPlayer(utf8Data, _listOfPlayers[i]);
            }
        }
        public static void DataBroadcast(string data)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data + Environment.NewLine);

            for (int i = 0; i < _listOfPlayers.Count; i++)
            {
                SentDataToPlayer(buffer, _listOfPlayers[i]);
            }
        }
        public static void SentDataToPlayer(string data, Player player)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(data + Environment.NewLine);
            NetworkStream stream = player.PlayerTcpClient.GetStream();
            stream.Write(buffer, 0, buffer.Length);
        }
        public static void SentDataToPlayer(byte[] utf8Data, Player player)
        {
            NetworkStream stream = player.PlayerTcpClient.GetStream();
            stream.Write(utf8Data, 0, utf8Data.Length);
        }
        public static void SendQuestionToPlayers(Question question)
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
            sb.Append($"T:{question.QuestionNumberTotal}\n\n");
            DataBroadcast(sb.ToString());
        }
        public static void AnswerProceed()
        {

            for (int i = 0; i < _numberOfPlayers; i++)
            {
                Player player = _listOfPlayers[i];
                if (player.LastAnswer == _questionManager.GetCurrentQuestion().CorrectAnswer)
                {
                    SentDataToPlayer(Resources.GoodAnswerString, player);
                    player.Points++;
                }
                else
                {
                    SentDataToPlayer(Resources.BadAnswerString, player);
                }
                SentDataToPlayer($"YourPoints+=+{player.Points}+=+", player);

            }

        }

        #endregion


    }


}