using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Resources;
using SerwerConsola.Properties;

namespace SerwerConsola
{
    public class QuestionManager
    {
        public static List<Question> QuestionList;
        public int Total { get; set; }
        public int _position { get; private set; }

        public QuestionManager()
        {
            QuestionList = GetQuestionsFromFile().ToList();
        }

        public ICollection<Question> GetQuestionsFromFile()
        {

            int n = Int32.Parse(Questions.IlePytań);
            if (n == 0) return null;
            Total = n;
            var result = new List<Question>(Total);
            for (int i = 1; i <= n; i++)
            {
                result.Add(new Question()
                {
                    QuestionNumberTotal = n,
                    QuestionNumber = i - 1,
                    QuestionText = Questions.ResourceManager.GetObject($"Pytanie{i}")?.ToString(),
                    AnswerA = Questions.ResourceManager.GetObject($"A{i}")?.ToString(),
                    AnswerB = Questions.ResourceManager.GetObject($"B{i}")?.ToString(),
                    AnswerC = Questions.ResourceManager.GetObject($"C{i}")?.ToString(),
                    AnswerD = Questions.ResourceManager.GetObject($"D{i}")?.ToString(),
                    CorrectAnswer = Questions.ResourceManager.GetObject($"Odpowiedz{i}").ToString()[0]
                });

            }

            return result;
        }

        public Question GetCurrentQuestion()
        {
            return QuestionList[_position];
        }

        public void NextQuestion()
        {
            if (_position < Total - 1)
            {
                _position++;
            }
            else
            {
                _position = 0;
            }
        }
    }
}

//    public static Question CurrentQuestion;
//    public int Total { get; set; }
//    private int _position { get; set; } 

//    public QuestionManager()
//    {
//        QuestionList = new List<Question>();
//        Total = 0;
//        _position = 0;
//    }

//    public void GetQuestionsFromFile()
//    {

//        int n = Int32.Parse(Questions.IlePytań);
//        if (n == 0) return;
//        Total = n;
//        for (int i = 1; i <= n; i++)
//        {
//            QuestionList.Add(new Question()
//            {
//                QuestionNumberTotal = n,
//                QuestionNumber = i-1,
//                QuestionText = Questions.ResourceManager.GetObject($"Pytanie{i}")?.ToString(),
//                AnswerA = Questions.ResourceManager.GetObject($"A{i}")?.ToString(),
//                AnswerB = Questions.ResourceManager.GetObject($"B{i}")?.ToString(),
//                AnswerC = Questions.ResourceManager.GetObject($"C{i}")?.ToString(),
//                AnswerD = Questions.ResourceManager.GetObject($"D{i}")?.ToString(),
//                CorrectAnswer = Questions.ResourceManager.GetObject($"Odpowiedz{i}").ToString()[0]
//            });

//        }

//        CurrentQuestion = QuestionList[0];
//    }
//    public void ResetPosition()
//    {
//        _position = 0;
//        CurrentQuestion = QuestionList[_position];
//    }

//    public Question GetNextQuestion()
//    {
//        if (_position < Total-1)
//        {
//            _position++;
//            CurrentQuestion= QuestionList[_position];
//            return CurrentQuestion;
//        }
//        else
//        {

//            CurrentQuestion = QuestionList[0];

//            return CurrentQuestion;

//        }

//    }



//}

