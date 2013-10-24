namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameSnippetsCategoryToCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SnippetCategories", "Snippet_ID", "dbo.Snippets");
            DropIndex("dbo.SnippetCategories", new[] { "Snippet_ID" });
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Description = c.String(),
                        Snippet_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Snippets", t => t.Snippet_ID)
                .Index(t => t.Snippet_ID);
            
            DropTable("dbo.SnippetCategories");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SnippetCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Description = c.String(),
                        Snippet_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropIndex("dbo.Categories", new[] { "Snippet_ID" });
            DropForeignKey("dbo.Categories", "Snippet_ID", "dbo.Snippets");
            DropTable("dbo.Categories");
            CreateIndex("dbo.SnippetCategories", "Snippet_ID");
            AddForeignKey("dbo.SnippetCategories", "Snippet_ID", "dbo.Snippets", "ID");
        }
    }
}
