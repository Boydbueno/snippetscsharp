namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeZipCodeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Snippets", "ZipCode", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Snippets", "ZipCode", c => c.String(nullable: false));
        }
    }
}
