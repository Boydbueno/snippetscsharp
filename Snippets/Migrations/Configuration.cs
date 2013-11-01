namespace Snippets.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
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

            context.Snippets.AddOrUpdate(s => s.Title,
                new Snippet
                {
                    Title = "This is my first snippet",
                    Body = "This is the body of my snippet, which doesn't look anything like a snippet",
                    Categories = new List<Category>(), 
                    Tags = new List<Tag>(),
                    VisibilityId = context.Visibilities.Single( s => s.Label == "Public").ID
                },
                new Snippet
                {
                    Title = "This is my second snippet",
                    Body = "Just some weird stuff in here to function as a second snippet and yeah, this snippet is protected",
                    Categories = new List<Category>(), 
                    Tags = new List<Tag>(),
                    VisibilityId = context.Visibilities.Single( s => s.Label == "Protected").ID
                }
            );

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

            context.Tags.AddOrUpdate(t => t.Label,
                new Tag
                {
                    Label = "PHP"
                },
                new Tag
                {
                    Label = "JavaScript"
                },
                new Tag
                {
                    Label = "Tricks"
                }
            );

            context.SaveChanges();

            AddOrUpdateTag(context, 1, "PHP");
            AddOrUpdateTag(context, 1, "Tricks");
            AddOrUpdateTag(context, 2, "Tricks");

            context.SaveChanges();

        }

        // Add tags to the snippets
        private void AddOrUpdateTag(SnippetsDBContext context, int snippetId, string tagLabel) {
            Snippet snippet = context.Snippets.SingleOrDefault(s => s.ID == snippetId);

            Tag tag = snippet.Tags.SingleOrDefault(t => t.Label == tagLabel);

            if(tag == null) {
                snippet.Tags.Add(context.Tags.Single(t => t.Label == tagLabel));
            }
        }

    }
}
