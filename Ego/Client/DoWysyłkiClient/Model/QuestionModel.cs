namespace Client.Model
{
    public class QuestionModel
    {
        public string QuestionText { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string AnswerC { get; set; }
        public string AnswerD { get; set; }
        public int QuestionNumber { get; set; }
        public int QuestionNumberTotal { get; set; }
        public string MyAnswer { get; set; }
    }
}