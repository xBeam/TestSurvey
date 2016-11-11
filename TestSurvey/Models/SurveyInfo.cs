using System.Collections.Generic;

namespace TestSurvey.Models
{
    public class SurveyInfo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ICollection<RespondentInfo> Respondents { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}