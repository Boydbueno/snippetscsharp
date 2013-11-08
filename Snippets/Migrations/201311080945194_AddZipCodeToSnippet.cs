namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddZipCodeToSnippet : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Snippets", "ZipCode", c => c.String(nullable: false));
            AlterColumn("dbo.Snippets", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Snippets", "Description", c => c.String());
            DropColumn("dbo.Snippets", "ZipCode");
        }
    }
}
