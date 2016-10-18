using System.Collections.Generic;

namespace TestSurvey
{
    public class SurveyInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RespondentInfo> Respondents { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}