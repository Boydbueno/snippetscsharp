using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Snippets.Models
{
    public class Tag
    {

        public virtual int ID { get; set; }
        [DisplayName("Tag")]
        public virtual string Label { get; set; }

        public virtual ICollection<Snippet> Snippets { get; set; }

    }
}