namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateManyToManyRelationBetweenTagsAndSnippets : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tags", "Snippet_ID", "dbo.Snippets");
            DropIndex("dbo.Tags", new[] { "Snippet_ID" });
            CreateTable(
                "dbo.TagSnippets",
                c => new
                    {
                        Tag_ID = c.Int(nullable: false),
                        Snippet_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_ID, t.Snippet_ID })
                .ForeignKey("dbo.Tags", t => t.Tag_ID, cascadeDelete: true)
                .ForeignKey("dbo.Snippets", t => t.Snippet_ID, cascadeDelete: true)
                .Index(t => t.Tag_ID)
                .Index(t => t.Snippet_ID);
            
            DropColumn("dbo.Tags", "Snippet_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tags", "Snippet_ID", c => c.Int());
            DropIndex("dbo.TagSnippets", new[] { "Snippet_ID" });
            DropIndex("dbo.TagSnippets", new[] { "Tag_ID" });
            DropForeignKey("dbo.TagSnippets", "Snippet_ID", "dbo.Snippets");
            DropForeignKey("dbo.TagSnippets", "Tag_ID", "dbo.Tags");
            DropTable("dbo.TagSnippets");
            CreateIndex("dbo.Tags", "Snippet_ID");
            AddForeignKey("dbo.Tags", "Snippet_ID", "dbo.Snippets", "ID");
        }
    }
}
