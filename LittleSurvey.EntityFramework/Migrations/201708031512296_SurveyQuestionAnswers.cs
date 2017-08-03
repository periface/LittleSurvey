namespace LittleSurvey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyQuestionAnswers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyQuestionAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        OfferedAnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SurveyQuestionAnswers");
        }
    }
}
