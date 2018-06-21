using Client.Model;
using Client.ViewModel.Commands;
using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Client.ViewModel
{

    public class GameViewModel : INotifyPropertyChanged
    {
        #region PrivateProperties
        private readonly InGameModel _inGameModel;
        private readonly QuestionModel _questionModel;
        private Thread _threadReceive;
        #endregion

        #region Constructors
        public GameViewModel()
        {
            _inGameModel = new InGameModel();
            _questionModel = new QuestionModel()
            {
                QuestionText = "Oczekiwanie na grę",
                AnswerA = "",
                AnswerB = "",
                AnswerC = "",
                AnswerD = "",
                QuestionNumber = 0,
                QuestionNumberTotal = 1
            };
        }
        #endregion

        #region Model

        public string MyName
        {
            get => _inGameModel.MyName;
            set
            {
                _inGameModel.MyName = value;
                MyNetworkStream = MyClient.GetStream();
            }
        }

        public TcpClient MyClient
        {
            get => _inGameModel.MyClient;
            set => _inGameModel.MyClient = value;
        }

        public NetworkStream MyNetworkStream
        {
            get => _inGameModel.MyNetworkStream;
            set => _inGameModel.MyNetworkStream = value;
        }

        public int MyNumber
        {
            get => _inGameModel.MyNumber;
            set => _inGameModel.MyNumber = value;
        }

        public string QuestionText
        {
            get => _questionModel.QuestionText;
            set => _questionModel.QuestionText = value;
        }

        public string AnswerA
        {
            get => _questionModel.AnswerA;
            set => _questionModel.AnswerA = value;

        }

        public string AnswerB
        {
            get => _questionModel.AnswerB;
            set => _questionModel.AnswerB = value;
        }

        public string AnswerC
        {
            get => _questionModel.AnswerC;
            set => _questionModel.AnswerC = value;
        }

        public string AnswerD
        {
            get => _questionModel.AnswerD;
            set => _questionModel.AnswerD = value;
        }

        public int QuestionNumber
        {
            get => _questionModel.QuestionNumber;
            set => _questionModel.QuestionNumber = value;

        }

        public int QuestionNumberTotal
        {
            get => _questionModel.QuestionNumberTotal;
            set => _questionModel.QuestionNumberTotal = value;
        }
        public string MyAnswer
        {
            get => _questionModel.MyAnswer;
            set => _questionModel.MyAnswer = value;
        }
        public int MyPoints
        {
            get => _inGameModel.MyPoints;
            set => _inGameModel.MyPoints = value;
        }
        public string AnswerInfo
        {
            get => _inGameModel.AnswerInfo;
            set => _inGameModel.AnswerInfo = value;
        }
        #endregion

        #region Methods

        public void StartTransmision()
        {
            _threadReceive = new Thread(o => ReceiveData((NetworkStream)o));
            _threadReceive.Start(_inGameModel.MyClient.GetStream());
            SendAnswerToSerwerCommand = new SendDataToSerwer(this);
            SendAnswerToSerwerCommand.Execute($"MyNameIs {MyName}");

        }
        public void ReceiveData(NetworkStream stream)
        {
            byte[] receivedBytes = new byte[1024];
            int byteCount = 0;

            try
            {
                while ((byteCount = stream.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
                {
                    Process(receivedBytes);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Utracono połączenie z serwere ;( \nAplikacja zostanie wyłączona");
                Console.WriteLine(e);
                Environment.Exit(0);
            }


        }

        public void Process(byte[] receivedBytes)

        {
            string data = System.Text.Encoding.UTF8.GetString(receivedBytes).Replace("\0", "");
            string[] content = data.Split(new string[] { "+=+", "\r", "\n" }, StringSplitOptions.None);
            switch (content[0])
            {
                case "ThisIsNewQuestion":
                    {
                        QuestionText = content[2].Split(':')[1];
                        AnswerA = content[3].Split(':')[1];
                        AnswerB = content[4].Split(':')[1];
                        AnswerC = content[5].Split(':')[1];
                        AnswerD = content[6].Split(':')[1];
                        QuestionNumber = Int32.Parse(content[7].Split(':')[1]);
                        QuestionNumberTotal = Int32.Parse(content[8].Split(':')[1]);
                        AnswerInfo = String.Empty;
                        MyAnswer = String.Empty;
                    }
                    break;
                case "GoodAnswer":
                    {
                        AnswerInfo = "Good answer!!";
                    }
                    break;
                case "BadAnswer":
                    {
                        AnswerInfo = "Bad answer!!";
                    }
                    break;
                case "YourPoints":
                    {
                        MyPoints = Int32.Parse(content[1]);
                    }
                    break;
                case "Time":
                    {
                        AnswerInfo = $"Next round in {content[1]}!!";
                    }
                    break;
            }
        }

        #endregion

        #region Commands
        private ICommand _sendAnswerToSerwerCommand;
        public ICommand SendAnswerToSerwerCommand { get; set; }//=> _sendAnswerToSerwerCommand ?? (_sendAnswerToSerwerCommand = new SendAnswerToSerwer(this));

        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}


