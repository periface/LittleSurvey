namespace LittleSurvey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestionOrderMoved : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveyQuestions", "Order", c => c.Int(nullable: false));
            DropColumn("dbo.Questions", "Order");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Questions", "Order", c => c.Int(nullable: false));
            DropColumn("dbo.SurveyQuestions", "Order");
        }
    }
}
