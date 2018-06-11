using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Client.Annotations;
using Client.Model;
using CezarCode;
using Client.Properties;
using Client.ViewModel.Commands;

namespace Client.ViewModel
{
    public class InGameViewModel : INotifyPropertyChanged
    {
        private readonly InGameModel _inGameModel;
        private readonly QuestionModel _questionModel;

        private Thread threadReceive;
        private Thread threadSend;

        public event PropertyChangedEventHandler PropertyChanged;

        public InGameViewModel()
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
        #endregion



        #region Methods

        public void StartTransmision()
        {
            threadReceive = new Thread(o => ReceiveData((NetworkStream)o));
            threadReceive.Start(_inGameModel.MyClient.GetStream());
            SendAnswerToSerwerCommand= new SendDataToSerwer(this);
            SendAnswerToSerwerCommand.Execute($"MyNameIs {MyName}");
            //threadSend = new Thread(o => SendDataToSerwer((NetworkStream)o));
            //  threadSend.Start(_inGameModel.MyClient.GetStream());
        }

        //public void SendDataToSerwer(NetworkStream stream)
        //{
        //     while (!string.IsNullOrEmpty(MyAnswer))
        //    {
        //        byte[] buffer = TransformToCezar(Encoding.ASCII.GetBytes(MyAnswer),10);
        //        stream.Write(buffer, 0, buffer.Length);
        //    }
        //}
        //public void SendDataToSerwer(NetworkStream stream,string data)
        //{
        //   if (!string.IsNullOrEmpty(data))
        //    {
        //        byte[] buffer = TransformToCezar(Encoding.ASCII.GetBytes(data), 10);
        //        stream.Write(buffer, 0, buffer.Length);
        //    }
        //}
        public void ReceiveData(NetworkStream stream)
        {
            byte[] receivedBytes = new byte[1024];
            int byteCount =0;

            while ((byteCount = stream.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                Process(receivedBytes);
            }
        }

        public void Process(byte[] receivedBytes)
        {
            string data = System.Text.Encoding.UTF8.GetString(CezarCode.CezarCode.TransformFromCezar(receivedBytes, 10));
            string command = data.Split()[0];
            string commandValue = data.Substring(data.IndexOf(' ') >= 0 ? data.IndexOf(' ') : 0);
            switch (command)
            {

                case "ThisIsNewQuestion":
                    {
                        QuestionText = commandValue.Split(new string[] { "+=+" }, StringSplitOptions.None)[1].Split()[1];
                        AnswerA = commandValue.Split(new string[] { "+=+" }, StringSplitOptions.None)[2].Split()[3];
                        AnswerB = commandValue.Split(new string[] { "+=+" }, StringSplitOptions.None)[3].Split()[3];
                        AnswerC = commandValue.Split(new string[] { "+=+" }, StringSplitOptions.None)[4].Split()[3];
                        AnswerD = commandValue.Split(new string[] { "+=+" }, StringSplitOptions.None)[5].Split()[3];
                        QuestionNumber = Int32.Parse(commandValue.Split(new string[] { "+=+" }, StringSplitOptions.None)[6].Split()[3]);
                        QuestionNumberTotal = Int32.Parse(commandValue.Split(new string[] { "+=+" }, StringSplitOptions.None)[7].Split()[3]);
                    }
                    break;
                case "GoodAnswer":
                    {
                        MessageBox.Show("Good answer!!");
                    }
                    break;
                case "BadAnswer":
                    {
                        MessageBox.Show("Bad answer!!");
                    }
                    break;

            }
        }

        #endregion

        #region Commands
        private ICommand _sendAnswerToSerwerCommand;
        public ICommand SendAnswerToSerwerCommand { get; set; }//=> _sendAnswerToSerwerCommand ?? (_sendAnswerToSerwerCommand = new SendAnswerToSerwer(this));

        #endregion
    }
}


