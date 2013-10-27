using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippets.Models.ViewModels
{
    public class SnippetDetails
    {
        public Snippet Snippet { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}