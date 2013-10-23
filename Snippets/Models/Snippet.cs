using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippets.Models
{
    public class Snippet
    {

        public virtual int ID { get; set; }
        public virtual string Title { get; set; }
        public virtual string Body { get; set; }

        public virtual List<SnippetCategory> SnippetCategories { get; set; }
        public virtual List<Tag> Tags { get; set; }

        public virtual int VisibilityId { get; set; }
        public virtual Visibility Visibility { get; set; }
    }
}