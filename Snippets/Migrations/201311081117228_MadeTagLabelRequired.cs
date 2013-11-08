namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeTagLabelRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tags", "Label", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tags", "Label", c => c.String());
        }
    }
}
