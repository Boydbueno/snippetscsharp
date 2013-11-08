namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropCategoryTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Snippet_ID", "dbo.Snippets");
            DropIndex("dbo.Categories", new[] { "Snippet_ID" });
            DropTable("dbo.Categories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Description = c.String(),
                        Snippet_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Categories", "Snippet_ID");
            AddForeignKey("dbo.Categories", "Snippet_ID", "dbo.Snippets", "ID");
        }
    }
}
