using TestSurvey.Models;

namespace TestSurvey.Context
{
    using System.Data.Entity;

    public class SurveyDbContext : DbContext
    {
        public SurveyDbContext() : base("name=SurveyDb")
        {
        }

        public virtual DbSet<RespondentInfo> RespondentInfos { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<SurveyInfo> SurveyInfos { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
    }
}