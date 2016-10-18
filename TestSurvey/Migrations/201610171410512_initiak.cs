namespace TestSurvey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initiak : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        AnswerType = c.Int(nullable: false),
                        QuestionId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId_Id)
                .Index(t => t.QuestionId_Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        SurveyId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyInfoes", t => t.SurveyId_Id)
                .Index(t => t.SurveyId_Id);
            
            CreateTable(
                "dbo.SurveyInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RespondentInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SurveyId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SurveyInfoes", t => t.SurveyId_Id)
                .Index(t => t.SurveyId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RespondentInfoes", "SurveyId_Id", "dbo.SurveyInfoes");
            DropForeignKey("dbo.Questions", "SurveyId_Id", "dbo.SurveyInfoes");
            DropForeignKey("dbo.Answers", "QuestionId_Id", "dbo.Questions");
            DropIndex("dbo.RespondentInfoes", new[] { "SurveyId_Id" });
            DropIndex("dbo.Questions", new[] { "SurveyId_Id" });
            DropIndex("dbo.Answers", new[] { "QuestionId_Id" });
            DropTable("dbo.RespondentInfoes");
            DropTable("dbo.SurveyInfoes");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
        }
    }
}
