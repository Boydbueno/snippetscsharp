namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Snippets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Body = c.String(),
                        VisibilityId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Visibilities", t => t.VisibilityId, cascadeDelete: true)
                .Index(t => t.VisibilityId);
            
            CreateTable(
                "dbo.SnippetCategories",
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
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Snippet_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Snippets", t => t.Snippet_ID)
                .Index(t => t.Snippet_ID);
            
            CreateTable(
                "dbo.Visibilities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tags", new[] { "Snippet_ID" });
            DropIndex("dbo.SnippetCategories", new[] { "Snippet_ID" });
            DropIndex("dbo.Snippets", new[] { "VisibilityId" });
            DropForeignKey("dbo.Tags", "Snippet_ID", "dbo.Snippets");
            DropForeignKey("dbo.SnippetCategories", "Snippet_ID", "dbo.Snippets");
            DropForeignKey("dbo.Snippets", "VisibilityId", "dbo.Visibilities");
            DropTable("dbo.Visibilities");
            DropTable("dbo.Tags");
            DropTable("dbo.SnippetCategories");
            DropTable("dbo.Snippets");
        }
    }
}
