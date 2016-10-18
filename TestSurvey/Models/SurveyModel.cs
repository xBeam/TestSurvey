namespace TestSurvey
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SurveyModel : DbContext
    {
        // Your context has been configured to use a 'SurveyModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'TestSurvey.SurveyModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'SurveyModel' 
        // connection string in the application configuration file.
        public SurveyModel()
            : base("name=SurveyModel")
        {
        }

        public virtual DbSet<RespondentInfo> RespondentInfos { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<SurveyInfo> SurveyInfos { get; set; }
    }
}