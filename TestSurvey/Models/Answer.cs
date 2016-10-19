namespace TestSurvey
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Question Question { get; set; }
        public SurveyInfo Survey { get; set; }
        public bool IsChecked { get; set; }
    }
}