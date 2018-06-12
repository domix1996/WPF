using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Client.Annotations;
using Client.Model;
using Client.Properties;
using Client.View;
using Client.ViewModel.Commands;

namespace Client.ViewModel
{
    public class LoggingViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly LoggingModel _loggingModel;
        public InGameView InGameView;

        public LoggingViewModel()
        {
            _loggingModel = new LoggingModel()
            {
                MyIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(),
                MyName = "Antriadus",
                HostIp = "127.0.0.1",
                HostPort = "5000"
            };
          


            _connectToServerCommand = new ConnectToServerCommand(this);
        }

        #region Model
        public string HostIp
        {
            get => _loggingModel.HostIp;
            set => _loggingModel.HostIp = value;
        }
        public string HostPort
        {
            get => _loggingModel.HostPort.ToString();
            set => _loggingModel.HostPort = value;
        }
        public string MyName
        {
            get => _loggingModel.MyName;
            set => _loggingModel.MyName = value;
        }
        public string MyIp
        {
            get => _loggingModel.MyIp;
            set => _loggingModel.MyIp = value;
        }

        
        #endregion

        #region Commands
        private ICommand _connectToServerCommand;
        public ICommand ConnectToServer => _connectToServerCommand ?? (_connectToServerCommand = new ConnectToServerCommand(this));

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public string this[string fieldName]
        {
            get
            {
                string result = null;
                if (fieldName == "MyName" || fieldName == "")
                {
                    if (string.IsNullOrEmpty(MyName))
                        result += "\nTwoja nazwa nie może być pusta!";
                }
                if (fieldName == "HostIp" || fieldName == "")
                {
                    if (string.IsNullOrEmpty(HostIp))
                        result += "\nHostIp nie może być puste!";
                    else
                    {
                        Regex regex = new Regex(Resources.IpValidationRegexString);
                        Match match = regex.Match(HostIp);
                        if (!match.Success)
                            result += "\nHost IP ma zły format!";
                    }
                }
                if (fieldName == "HostPort" || fieldName == "")
                {
                    if (string.IsNullOrEmpty(HostPort))
                        result += "\nHostPort nie może być puste!";
                    else
                    {
                        try
                        {
                            if (Int32.Parse(HostPort) < 0 || Int32.Parse(HostPort) > 65535)
                            {
                                result += "\n Host Port wykracza poza zakres 0-65535";
                            }
                        }
                        catch (Exception e)
                        {
                            result += "\n Host musi być liczbą całkowitą z zakresu 0-65535";

                            
                        }
                    }

                   
                }
                return result;
            }
        }

        public virtual string Error
        {
            get
            {
                string result = this[string.Empty];
                if (result != null && result.Trim().Length == 0)
                {
                    result = null;
                }
                return result;
            }
        }
    }
}