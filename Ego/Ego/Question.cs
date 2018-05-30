using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Ego
{
    public class Question
    {
        public int QuestionNumber { get; set; }
        private int correct;
        public int Correct
        {
            get => correct;
            set
            {
                if (value > 0 && value < 5)
                    correct = value - 1; //w pliku mam 1,2,3,4 ale tablica zaczyna się od 0 
                else throw new Exception("Correct answer question is diffrent than 1,2,3,4");
            }
        }
        public string QuestionText { get; set; }
        public int Total { get; set; }
        public string[] Answers { get; set; }
    }

    public class QuestionsContainer
    {
        public List<Question> Questions { get; set; } = new List<Question>();

        public List<Question> GetQuestionsFromTxt(string path)
        {
            using (var sr = new StreamReader(path))
            {
                Question temp;
                List<Question> result = new List<Question>();
                int n = Int32.Parse(sr.ReadLine() ?? throw new Exception("Error in first line in file with questions"));
                for (int i = 0; i < n; i++)
                {
                    temp = new Question()
                    {
                        QuestionNumber = i,
                        QuestionText = sr.ReadLine(),
                        Total=n,
                        Correct = Int32.Parse(sr.ReadLine()),
                        Answers = new[]
                        {
                            sr.ReadLine(),
                            sr.ReadLine(),
                            sr.ReadLine(),
                            sr.ReadLine()
                        }
                    };
                    result.Add(temp);
                }
                return result;

            }

        }
    }
}