using System.Collections.Generic;

namespace TestSurvey.Models
{
    public class RespondentInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<SurveyInfo> Surveys { get; set; }
        public ICollection<Response> Responses { get; set; }
    }
}