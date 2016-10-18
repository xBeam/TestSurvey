using System.Collections.Generic;
namespace TestSurvey
{
    public enum QuestionTypes
    {
        Checkbox,
        Radio,
        Text
    }

    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public SurveyInfo SurveyId { get; set; }
        public QuestionTypes QuestionType { get; set; }
    }
}