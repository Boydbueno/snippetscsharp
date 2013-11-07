namespace Snippets.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;
    using System.Web.Security;
    using Snippets.Models;
    using WebMatrix.WebData;

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

            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
            }

            context.Snippets.AddOrUpdate(s => s.Title,
                new Snippet
                {
                    Title = "JavaScript Properties",
                    Description = "Ipv methods/properties gewoon aan de prototype te hangen kan dat ook via defineProperties",
                    Body = @"Object.defineProperties(o.prototype, {
  property:{
    value: ""Waarde""
  }
  property: { 
     set: function(){
        // Functie voor set 
     },
     get: function(){
        return // stuff
     }
});
",
                    Categories = new List<Category>(),
                    Tags = new List<Tag>(),
                    VisibilityId = context.Visibilities.Single(s => s.Label == "Public").ID
                },
                new Snippet
                {
                    Title = "Compass Spriting",
                    Description = "Generating sprites with compass",
                    Body = @"@import ""my-icons/*.png"";
@include all-my-icons-sprites;
",
                    Categories = new List<Category>(),
                    Tags = new List<Tag>(),
                    VisibilityId = context.Visibilities.Single(s => s.Label == "Protected").ID
                },
                new Snippet
                {
                    Title = "sprintf",
                    Description = "String formatten. Not sure when I’d use this. Maar wel handig om te onthouden.",
                    Body = @"$name = ""Boyd Bueno de Mesquita"";
$age = ""23"";
$job = ""Student"";
$string = sprintf(""My name is %s and I’m $d years old. I’m currently a $s"", $name, $age, $job);
",
                    Categories = new List<Category>(),
                    Tags = new List<Tag>(),
                    VisibilityId = context.Visibilities.Single(s => s.Label == "Public").ID
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
                    Label = "Compass"
                },
                new Tag
                {
                    Label = "Tricks"
                }
            );

            context.SaveChanges();

            AddOrUpdateTag(context, "JavaScript Properties", "JavaScript");
            AddOrUpdateTag(context, "JavaScript Properties", "Tricks");
            AddOrUpdateTag(context, "Compass Spriting", "Compass");
            AddOrUpdateTag(context, "sprintf", "PHP");


            AddRoles(new string[] { "Standard", "Admin" } );

            AddUser("Admin", "Welkom01", "Admin");
            AddUser("Boyd", "Welkom01", "Standard");

            context.SaveChanges();

        }

        // Add tags to the snippets
        private void AddOrUpdateTag(SnippetsDBContext context, string snippetTitle, string tagLabel) {
            Snippet snippet = context.Snippets.SingleOrDefault(s => s.Title == snippetTitle);

            Tag tag = snippet.Tags.SingleOrDefault(t => t.Label == tagLabel);

            if(tag == null) {
                snippet.Tags.Add(context.Tags.Single(t => t.Label == tagLabel));
            }
        }

        private void AddRoles(string[] roles)
        {
            foreach (string role in roles)
            {
                if (!Roles.RoleExists(role))
                {
                    Roles.CreateRole(role);
                }
            }
        }

        private void AddUser(string userName, string password, string role)
        {
            if (!WebSecurity.UserExists(userName))
            {
                WebSecurity.CreateUserAndAccount(userName, password);
                Roles.AddUserToRole(userName, role);
            }
        }

    }
}
