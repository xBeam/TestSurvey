using System.Collections.Generic;

namespace TestSurvey.Models
{
    public class Response
    {
        public int Id { get; set; }
        public RespondentInfo Respondent { get; set; }
        public SurveyInfo Survey { get; set; }
        public Question Question { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public string TextValue { get; set; }
    }
}