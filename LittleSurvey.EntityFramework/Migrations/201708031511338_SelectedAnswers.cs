namespace LittleSurvey.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class SelectedAnswers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SelectedAnswers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SelectedAnswerId = c.Int(nullable: false),
                        AnswerId = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        DeleterUserId = c.Long(),
                        DeletionTime = c.DateTime(),
                        LastModificationTime = c.DateTime(),
                        LastModifierUserId = c.Long(),
                        CreationTime = c.DateTime(nullable: false),
                        CreatorUserId = c.Long(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SelectedAnswer_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SelectedAnswers",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_SelectedAnswer_SoftDelete", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
        }
    }
}
