﻿namespace TestSurvey
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Question QuestionId { get; set; }
        public SurveyInfo SurveyId { get; set; }
    }
}