using System.Collections.Generic;
namespace TestSurvey
{
    public class RespondentInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public SurveyInfo Survey { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}