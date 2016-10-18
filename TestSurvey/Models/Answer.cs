namespace TestSurvey
{
    public enum AnswerTypes
    {
        Checkbox,
        Radio,
        Text
    }

    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Question QuestionId { get; set; }
        public AnswerTypes AnswerType { get; set; }
    }
}