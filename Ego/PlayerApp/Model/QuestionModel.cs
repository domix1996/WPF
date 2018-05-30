using System;

namespace PlayerApp.Model
{
    public class QuestionModel
    {
        public int QuestionNumber { get; set; }
        private char _correctAnswer;
        public char Correct
        {
            get => _correctAnswer;
            set
            {
                if (value >= 'a' && value <= 'd')
                    _correctAnswer = value;
                else throw new Exception("Correct answer out of range ");
            }
        }
        public string QuestionText { get; set; }
        public string[] Answers { get; set; }
    }
}