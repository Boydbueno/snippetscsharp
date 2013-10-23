using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippets.Models
{
    public class Snippet
    {

        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public List<SnippetCategory> SnippetCategories { get; set; }
        public List<Tag> Tags { get; set; }

        public int VisibilityId { get; set; }
        public Visibility Visibility { get; set; }
    }
}