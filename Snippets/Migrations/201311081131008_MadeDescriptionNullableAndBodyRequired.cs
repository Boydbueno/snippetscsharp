namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeDescriptionNullableAndBodyRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Snippets", "Description", c => c.String());
            AlterColumn("dbo.Snippets", "Body", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Snippets", "Body", c => c.String());
            AlterColumn("dbo.Snippets", "Description", c => c.String(nullable: false));
        }
    }
}
