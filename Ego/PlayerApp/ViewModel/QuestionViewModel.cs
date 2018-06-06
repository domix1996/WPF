using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using PlayerApp.Model;

namespace PlayerApp.ViewModel
{

    public class QuestionViewModel : INotifyPropertyChanged
    {
        private QuestionModel _model = new QuestionModel()
        {
            Answers = new string[]
            {
                "Odpowiedz to 2","Odpowiedz to 3","Odpowiedz to 4","Odpowiedz to 5"
            },
            Correct = 'a',
            QuestionNumber = 1,
            QuestionText = "Jaki jest wynik równania 2+2"
        };

        #region  ModelPropetries
        public string AnswerA
        {
            get => _model.Answers[0];
            set => _model.Answers[0] = value;

        }
        public string AnswerB
        {
            get => _model.Answers[1];
            set => _model.Answers[1] = value;
        }
        public string AnswerC
        {
            get => _model.Answers[2];
            set => _model.Answers[2] = value;
        }
        public string AnswerD
        {
            get => _model.Answers[3];
            set => _model.Answers[3] = value;
        }
        public int QuestionNumber
        {
            get => _model.QuestionNumber;
            set => _model.QuestionNumber = value;
        }
        public string QuestionText
        {
            get => _model.QuestionText;
            set => _model.QuestionText = value;
        }
        #endregion

       
        public event PropertyChangedEventHandler PropertyChanged;

        

    }
}