// <auto-generated />
namespace Snippets.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    public sealed partial class RenameSnippetsCategoryToCategory : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(RenameSnippetsCategoryToCategory));
        
        string IMigrationMetadata.Id
        {
            get { return "201310241010007_RenameSnippetsCategoryToCategory"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}