namespace Snippets.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Snippets.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Snippets.Models.SnippetsDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Snippets.Models.SnippetsDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
                context.Visibilities.AddOrUpdate(v => v.Label,
                  new Visibility { 
                      Label = "Hidden",
                      Description = "Only visible for administrators"
                  },
                  new Visibility
                  {
                      Label = "Protected",
                      Description = "Only visible to logged in uses"
                  },
                  new Visibility
                  {
                      Label = "Public",
                      Description = "Visible to everyone"
                  }
                );
            //
        }
    }
}
