namespace LittleSurvey.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OfferedAnswers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OfferedAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnswerText = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OfferedAnswers");
        }
    }
}
