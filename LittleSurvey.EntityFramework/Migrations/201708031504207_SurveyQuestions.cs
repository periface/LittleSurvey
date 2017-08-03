namespace LittleSurvey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SurveyQuestions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyQuestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SurveyId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SurveyQuestions");
        }
    }
}
