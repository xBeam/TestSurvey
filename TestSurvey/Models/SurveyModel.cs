namespace TestSurvey.Models
{
    using System.Data.Entity;

    public class SurveyModel : DbContext
    {
        public SurveyModel()
            : base("name=SurveyModel")
        {
        }

        public virtual DbSet<RespondentInfo> RespondentInfos { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<SurveyInfo> SurveyInfos { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
    }
}