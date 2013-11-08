namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedMaximumLengthToTagLabel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "Label", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Label", c => c.String(nullable: false));
        }
    }
}
