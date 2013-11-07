namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDescriptionToSnippets : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Snippets", "Description", c => c.String());
            AlterColumn("dbo.Snippets", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Snippets", "Title", c => c.String());
            DropColumn("dbo.Snippets", "Description");
        }
    }
}
