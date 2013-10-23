using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snippets.Models
{
    public class SnippetCategory
    {
        public virtual int ID { get; set; }
        public virtual string Label { get; set; }
        public virtual string Description { get; set; }
    }
}