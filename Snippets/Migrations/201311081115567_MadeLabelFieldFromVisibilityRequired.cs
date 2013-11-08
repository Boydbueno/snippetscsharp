namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeLabelFieldFromVisibilityRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Visibilities", "Label", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Visibilities", "Label", c => c.String());
        }
    }
}
