namespace LittleSurvey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SelectedAnswerNP : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SelectedAnswers", "AnswerId");
            AddForeignKey("dbo.SelectedAnswers", "AnswerId", "dbo.Answers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SelectedAnswers", "AnswerId", "dbo.Answers");
            DropIndex("dbo.SelectedAnswers", new[] { "AnswerId" });
        }
    }
}
